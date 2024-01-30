using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float positionSpeed;

    public GameObject Wall;
    public GameObject player;
    private float PosDis;

    private void Update()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        // Calculate the target position and rotation
        Vector3 targetPosition = UpdatePosition();
        Quaternion targetRotation = UpdateRotation();

        // Move the camera towards the target position if the distance is greater than 3 units
        if (Vector3.Distance(transform.position, targetPosition) > PosDis)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, positionSpeed * Time.deltaTime);
        }

        // Rotate the camera using Slerp if the angle is greater than 3 units
        if (Quaternion.Angle(transform.rotation, targetRotation) > 3f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Method to calculate the target rotation based on player direction
    Quaternion UpdateRotation()
    {
        Vector3 playerDirection = Vector3.Normalize(player.transform.position - transform.position);
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
        return targetRotation;
    }

    // Method to calculate the target position based on player's position
    Vector3 UpdatePosition()
    {
        Vector3 targetPosition = transform.position;

        // Adjust the y position to follow the player vertically with an offset
        targetPosition.y = Mathf.Min(9, player.transform.position.y + 7);

        // Adjust the z position to follow the player on the z-axis with an offset
        targetPosition.z = Mathf.Max(-17, player.transform.position.z - 18);

        // Adjust the x position if the distance between the camera and player on the x-axis is greater than 8 units
        if (Mathf.Abs(transform.position.x - player.transform.position.x) > 8f)
        {
            targetPosition.x = Mathf.Max(-2.8f, player.transform.position.x);
        }
        if(player.transform.position.x > 10)
        {
            positionSpeed = 10;
            targetPosition.x = 15;
            PosDis = 0;
        }
        else if (transform.position.x > 3)
        {
            Wall.SetActive(false);
            positionSpeed = 10;
            targetPosition.x = 3;
            PosDis = 0;
        }
        else
        {
            positionSpeed = 2;
            PosDis = 3;
        }
        if (player.transform.position.x > 6)
        {
            Debug.Log("test");

            targetPosition.z = -9;
            if (player.transform.position.z < -4 && player.transform.position.x > 11.2f)
            {
                targetPosition.z =  player.transform.position.z - 10;
                positionSpeed = 6;
                PosDis = 1;
            }
        }

        if (transform.position.x > 14)
        {
            Wall.SetActive(true);
        }


        return targetPosition;
    }
}
