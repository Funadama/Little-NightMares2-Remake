using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Player : MonoBehaviour
{
    private float Angle;
    private float AtAngle;
    private float UsingRotationSpeed;
    private int SpeedState;

    public float Acceleration;
    public float Min_RotationSpeed;
    public float Max_RotationSpeed;
    public float Speed;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Angle = CalculateAngle(horizontalInput, verticalInput);

            //acceleration
            UsingRotationSpeed = Mathf.Min(Max_RotationSpeed, UsingRotationSpeed + Acceleration);
            //Rotation
            AtAngle = Mathf.MoveTowardsAngle(AtAngle, Angle, UsingRotationSpeed * Time.deltaTime);
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, AtAngle, gameObject.transform.eulerAngles.z);

            //Postion
            gameObject.transform.position = gameObject.transform.position + Time.deltaTime * transform.forward * Speed;
        }
        else
        {
            UsingRotationSpeed = Min_RotationSpeed;
        }

        //Running/Crouching
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Running
            SpeedState = 1;

            Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 300;
            Speed = 10;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //Crouching
            SpeedState = 2;

            Acceleration = 10;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 300;
            Speed = 3;
        }
        else
        {
            //Walking
            SpeedState = 0;
        
            Acceleration = 15;
            Min_RotationSpeed = 200;
            Max_RotationSpeed = 350;
            Speed = 6;
        }


    }


    float CalculateAngle(float horizontal, float vertical)
    {
        float radians = Mathf.Atan2(horizontal, vertical);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }


}