using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Player : MonoBehaviour
{
    private float Angle;
    private float AtAngle;
    private float RotationSpeed;
    private int SpeedState;
    private float MovementSpeed;
    private bool OnGround;

    public float Rotation_Acceleration;
    public float Min_RotationSpeed;
    public float Max_RotationSpeed;

    public float Max_MovementSpeed;
    public float decelerate;

    public float jumpHeight;

    void Update()
    {
        OnGround = Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 1);


        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && OnGround)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Angle = CalculateAngle(horizontalInput, verticalInput);

            //Acceleration
            RotationSpeed = Mathf.Min(Max_RotationSpeed, RotationSpeed + Rotation_Acceleration);
            //Rotation
            AtAngle = Mathf.MoveTowardsAngle(AtAngle, Angle, RotationSpeed * Time.deltaTime);
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, AtAngle, gameObject.transform.eulerAngles.z);

            //Speed
            MovementSpeed = Max_MovementSpeed;
        }
        else
        {
            RotationSpeed = Min_RotationSpeed;
        }

        //Movement
        if (OnGround)
        {
            MovementSpeed = Mathf.Max(0, MovementSpeed - decelerate);
        }
        gameObject.transform.position = gameObject.transform.position + Time.deltaTime * transform.forward * MovementSpeed;

        //Running/Crouching

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Running
            SpeedState = 1;

            Rotation_Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 300;
            Max_MovementSpeed = 10;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //Crouching
            SpeedState = 2;

            Rotation_Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 300;
            Max_MovementSpeed = 3;
        }
        else
        {
            //Walking
            SpeedState = 0;
        
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


}