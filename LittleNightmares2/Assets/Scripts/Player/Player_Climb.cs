using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Climb : MonoBehaviour
{
    public GameObject test;
    public bool IsClimb;

    private bool CanClimb;
    private RaycastHit Hit;
    private Vector3 HitPos;

    private bool LerpToClimb;
    private Vector3 TargetRotation;
    private Vector3 TargetPosition;
    private float frameCount;
    private Vector3 CurPos;
    private Vector3 CurRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        test.transform.position = transform.position + transform.forward * 1.25f + new Vector3(0, 2, 0);

        Ray ray = new Ray(transform.position + transform.forward * 1.25f + new Vector3(0, 2, 0), Vector3.down);
        CanClimb = Physics.Raycast(ray, out Hit,0.5f);

        if (Input.GetKey(KeyCode.Mouse0) && CanClimb)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            IsClimb = true;
            HitPos = Hit.point;

            //Align with wall
            Ray WallCheck = new Ray(transform.position, transform.forward);
            Physics.Raycast(WallCheck, out RaycastHit hit, 2);

            TargetRotation = -hit.normal;
            TargetPosition = hit.normal * 0.8f  + new Vector3(hit.point.x, 0, 0) + new Vector3(0, Hit.point.y + -1.5f, 0) + new Vector3(0, 0, hit.point.z);
            LerpToClimb = true;
            CurPos = transform.position;
            CurRot = transform.forward;



        }

        if (LerpToClimb)
        {
            frameCount++;
            transform.position = Vector3.Lerp(CurPos, TargetPosition, frameCount / 15f);
            transform.forward = Vector3.Lerp(CurRot, TargetRotation, frameCount / 15f);
            if(frameCount >= 15)
            {
                frameCount = 0;
                LerpToClimb = false;
            }
        }

        if (!(Input.GetKey(KeyCode.Mouse0)))
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            IsClimb = false;
        }

        if (IsClimb && Input.GetKeyDown("space") && !(LerpToClimb))
        {
            Vector3 NewPos = gameObject.transform.position;
            NewPos.y = HitPos.y + 0.64f;
            gameObject.transform.position = NewPos;
        }
    }
}
