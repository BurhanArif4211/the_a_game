using System;
using UnityEngine;
public class PlayerMovementAdvance : MonoBehaviour
{
    
    public float normalSpeed = 5f;
    public float currentSpeed = 0f;
    private static bool isWalking = false;
    public Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 15f;

    private Vector3 nullVector;

    void Update()
    {
        Move();
    }

    void Move()
    {
        isWalking =  ((Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)) ? true :false;
        bool isSprinting= PlayerSprint.GetIsSprinting();
        Vector3 moveDirection = Vector3.zero;
        if (isWalking)
        {
            float inputX = Input.GetAxisRaw("Horizontal"); // A/D
            float inputZ = Input.GetAxisRaw("Vertical");   // W/S
            moveDirection = cameraTransform.forward * inputZ + cameraTransform.right * inputX;
            moveDirection.y = 0;
            moveDirection.Normalize();
            RotateTowardsDirection(moveDirection);
            SetSpeed(normalSpeed);
        }
        else if(GetInputMagnitude()<=0)
        {

            SetSpeed(0f);
        }
        
        SetVelocity(moveDirection);
        Debug.Log("Current speed"+currentSpeed);
    }

    void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    // Method to get movement intensity (0 to 1)
    public float GetMovementIntensity()
    {
        float normalizedSpeed = Mathf.Clamp01((currentSpeed - normalSpeed) / (PlayerSprint.sprintSpeed - normalSpeed));
        return (float)System.Math.Round(normalizedSpeed, 5);
    }

    public static float GetInputMagnitude()
    {
        float inputX = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        float inputZ = Mathf.Abs(Input.GetAxisRaw("Vertical"));
        return inputX + inputZ; // Returns 0 when idle
    }

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }
    public void SetVelocity(Vector3 moveDir)
    {
        Vector3 velocity = moveDir * currentSpeed;
        transform.position += moveDir * currentSpeed * Time.deltaTime;
    }
    public static bool GetIsWalking() => isWalking;

}

