using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Rotation variables
    private float targetAngle;
    private float currentAngle;
    private float RotationSpeed;

    // Movement variables
    private bool OnGround;
    private bool Moving;

    // Rotation acceleration parameters
    private float Rotation_Acceleration;
    private float Min_RotationSpeed;
    private float Max_RotationSpeed;

    // Movement acceleration and deceleration parameters
    private float Max_MovementSpeed;
    [SerializeField] private float Movement_Acceleration;
    [SerializeField] private float Movement_decelerate;
    [SerializeField] private float jumpHeight;

    // Movement variable
    public float MovementSpeed;


    void Update()
    {
        // Ground check
        OnGround = Physics.Raycast(new Ray(transform.position + new Vector3(0,1,0), Vector3.down), out RaycastHit hit, 1);
        if(!(gameObject.GetComponent<Player_Climb>().IsClimb || gameObject.GetComponent<PickUp>().IsPickup))
        {
            Moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        }
        else
        {
            Moving = false;
        }

        if (Moving)
        {
            // Set Rotation Target
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            targetAngle = CalculateAngle(horizontalInput, verticalInput);

            // Walk while rotating
            if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) < 100)
            {
                // Movement Speed Acceleration
                MovementSpeed = Mathf.Min(Max_MovementSpeed, MovementSpeed + Movement_Acceleration);
            }
        }

        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 1)
        {
            // Rotation Speed Acceleration
            RotationSpeed = Mathf.Min(Max_RotationSpeed, RotationSpeed + Rotation_Acceleration);

            // Rotation get angle
            currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, RotationSpeed * Time.deltaTime);

            // Rotation
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, currentAngle, gameObject.transform.eulerAngles.z);
        }
        else
        {
            // Reset Acceleration
            RotationSpeed = Min_RotationSpeed;
        }

        // Decelerate Movement
        if (OnGround && !(Moving))
        {
            MovementSpeed = Mathf.Max(0, MovementSpeed - Movement_decelerate);
        }
        else
        {
            MovementSpeed = Mathf.Max(0, MovementSpeed - Movement_decelerate * 0.1f);
        }

        // Wall detection
        Ray WallCheck = new Ray(transform.position, transform.forward);
        Vector3 Dir = transform.forward;
        bool Wall = Physics.Raycast(WallCheck, out hit, 1f) && hit.collider.tag != "door" && hit.collider.tag != "PickUpObject";

        if (hit.distance < 0.25f)
        {
            //Dir = transform.forward + hit.normal * 1f;
        }

        if (Wall)
        {
            MovementSpeed = 0;
        }


        // Move the player
        gameObject.transform.position = gameObject.transform.position + Time.deltaTime * Dir * MovementSpeed;

        // Running/Crouching
        if (!OnGround)
        {
            // Jumping
            Rotation_Acceleration = 2;
            Min_RotationSpeed = 50;
            Max_RotationSpeed = 100;
            Max_MovementSpeed = Mathf.Max(1, MovementSpeed);
        }
        else if (gameObject.GetComponent<PickUp>().IsMoving)
        {
            // IsPickup
            Rotation_Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 350;
            Max_MovementSpeed = 3;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            // Crouching
            Rotation_Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 350;
            Max_MovementSpeed = 3;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            // Running
            Rotation_Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 350;
            Max_MovementSpeed = 10;
        }
        else
        {
            // Walking
            Rotation_Acceleration = 15;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 400;
            Max_MovementSpeed = 6;
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && OnGround && !(gameObject.GetComponent<PickUp>().IsMoving))
        {
            GetComponent<Rigidbody>().velocity += jumpHeight * Vector3.up;
        }
    }

    // Helper method to calculate the angle
    float CalculateAngle(float horizontal, float vertical)
    {
        float radians = Mathf.Atan2(horizontal, vertical);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    // Collision handling
    void OnCollisionEnter()
    {
        if (!(OnGround))
        {
            // Stop movement on collision with non-ground object
            MovementSpeed = 0;
            Max_MovementSpeed = Mathf.Max(1, Max_MovementSpeed - 1);
        }
    }
}
