using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float Angle;
    private float AtAngle;
    private float RotationSpeed;
    public float MovementSpeed;
    private bool OnGround;
    private bool Moving;

    public float Rotation_Acceleration;
    public float Min_RotationSpeed;
    public float Max_RotationSpeed;

    public float Movement_Acceleration;
    public float Movement_decelerate;
    public float Max_MovementSpeed;
    
    public float jumpHeight;

    void Update()
    {
        OnGround = Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 1);
        Moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (Moving && !(gameObject.GetComponent<Player_Climb>().IsClimb))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Angle = CalculateAngle(horizontalInput, verticalInput);

            //Rotation Speed Acceleration
            RotationSpeed = Mathf.Min(Max_RotationSpeed, RotationSpeed + Rotation_Acceleration);
            //Rotation
            AtAngle = Mathf.MoveTowardsAngle(AtAngle, Angle, RotationSpeed * Time.deltaTime);
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, AtAngle, gameObject.transform.eulerAngles.z);

            //Walk while rotating
            if (Mathf.Abs(Mathf.DeltaAngle(AtAngle, Angle)) < 100)
            {
                //Movement Speed Acceleration
                MovementSpeed = Mathf.Min(Max_MovementSpeed, MovementSpeed + Movement_Acceleration);
            }
        }
        else
        {
            //Reset Acceleration
            RotationSpeed = Min_RotationSpeed;
        }

        //decelerate Movement
        if (OnGround && !(Moving))
        {
            MovementSpeed = Mathf.Max(0, MovementSpeed - Movement_decelerate);
        }
        else
        {
            MovementSpeed = Mathf.Max(0, MovementSpeed - Movement_decelerate * 0.1f);
        }
        //Wall detection
        Ray WallCheck = new Ray(transform.position, transform.forward);
        Vector3 Dir = transform.forward;
        bool Wall = Physics.Raycast(WallCheck, out hit, 0.7f);

        if(hit.distance < 0.25f)
        {
            Dir = transform.forward + hit.normal * 1f;
        }
        if (Wall)
        {
            MovementSpeed = 0;
        }
        Debug.Log(MovementSpeed);
        gameObject.transform.position = gameObject.transform.position + Time.deltaTime * Dir * MovementSpeed;

        //Running/Crouching

        if (!OnGround)
        {
            //Jumping

            Rotation_Acceleration = 2;
            Min_RotationSpeed = 50;
            Max_RotationSpeed = 100;
            Max_MovementSpeed = Mathf.Max(1, MovementSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //Crouching

            Rotation_Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 300;
            Max_MovementSpeed = 3;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            //Running

            Rotation_Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 300;
            Max_MovementSpeed = 10;
        }
        else
        {
            //Walking

            Rotation_Acceleration = 15;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 350;
            Max_MovementSpeed = 6;
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            GetComponent<Rigidbody>().velocity += jumpHeight * Vector3.up;
        }

    }


    float CalculateAngle(float horizontal, float vertical)
    {
        float radians = Mathf.Atan2(horizontal, vertical);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    void OnCollisionEnter()
    {
        if(!(OnGround))
        {
            MovementSpeed = 0;
            Max_MovementSpeed = Mathf.Max(1, Max_MovementSpeed - 1);
        }
    }


}