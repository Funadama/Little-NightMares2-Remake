using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Player : MonoBehaviour
{
    public float angle;
    private int frames;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Debug.Log(horizontalInput);
        Debug.Log(verticalInput);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            angle = CalculateAngle(horizontalInput, verticalInput);
        }

        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, angle, gameObject.transform.eulerAngles.z);
    }


    float CalculateAngle(float horizontal, float vertical)
    {
        float radians = Mathf.Atan2(horizontal, vertical);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }


}