using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Climb : MonoBehaviour
{
    // Climb variables
    private bool CanClimb;
    private RaycastHit Hit;
    private Vector3 HitPos;

    private bool LerpToClimb;
    private float frameCount;
    private Vector3 TargetRotation;
    private Vector3 TargetPosition;
    private Vector3 CurPos;
    private Vector3 CurRot;

    [SerializeField] private float ClimbSpeed;

    public bool IsClimb;


    // Update is called once per frame
    void Update()
    {

        // Raycast to check if climbing is possible
        Ray ray = new Ray(transform.position + transform.forward + new Vector3(0, 2, 0), Vector3.down);
        CanClimb = Physics.Raycast(ray, out Hit, 0.5f);

        // Check for input to initiate climbing
        if (Input.GetKey(KeyCode.Mouse0) && CanClimb && !(IsClimb))
        {
            // Freeze Y position and rotation constraints
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

            // Set climbing flag and freeze regular movement
            IsClimb = true;
            gameObject.GetComponent<PlayerMovement>().MovementSpeed = 0;
            HitPos = Hit.point;

            // Align with the wall
            Ray WallCheck = new Ray(transform.position + new Vector3(0, 1.5f, 0), transform.forward);
            Physics.Raycast(WallCheck, out RaycastHit hit, 2);

            TargetRotation = -hit.normal;
            TargetPosition = hit.normal * 0.9f + new Vector3(hit.point.x, 0, hit.point.z) + new Vector3(0, Hit.point.y + -1.5f, 0);
            LerpToClimb = true;

            CurPos = transform.position;
            CurRot = transform.forward;
        }

        // Lerp towards the climbing position and rotation
        if (LerpToClimb)
        {
            frameCount++;
            transform.position = Vector3.Lerp(CurPos, TargetPosition, frameCount / ClimbSpeed);
            transform.forward = Vector3.Lerp(CurRot, TargetRotation, frameCount / ClimbSpeed);

            // Reset frame count and stop lerping when finished
            if (frameCount >= ClimbSpeed)
            {
                frameCount = 0;
                LerpToClimb = false;
            }
        }

        // Stop Climbing
        if (!(Input.GetKey(KeyCode.Mouse0)) || !(IsClimb))
        {
            // Release Y position and rotation constraints
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            // Reset climbing flag
            IsClimb = false;
        }

        // Climb Up
        if (IsClimb && Input.GetKeyDown("space") && !(LerpToClimb))
        {
            // Move the player up during climbing
            Vector3 NewPos = gameObject.transform.position;
            NewPos.y = HitPos.y + 0.64f;
            gameObject.transform.position = NewPos + transform.forward;
            IsClimb = false;
        }
    }
}
