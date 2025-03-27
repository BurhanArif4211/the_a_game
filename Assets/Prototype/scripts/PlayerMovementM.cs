using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAdvance : MonoBehaviour
{
    
    public float normalSpeed = 5f;
    public float sprintSpeed = 10f;
    public Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 nullVector;

    void Update()
    {
        Move();
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal"); // A/D
        float inputZ = Input.GetAxisRaw("Vertical");   // W/S

        Vector3 moveDirection = Vector3.zero;
        if (inputX != 0 || inputZ != 0)
        {
            moveDirection = cameraTransform.forward * inputZ + cameraTransform.right * inputX;
            moveDirection.y = 0;
            moveDirection.Normalize();

            RotateTowardsDirection(moveDirection);
        }

        Vector3 velocity = moveDirection * normalSpeed;
        transform.position += moveDirection * normalSpeed * Time.deltaTime;
    }

    void RotateTowardsDirection(Vector3 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public float GetInputMagnitude()
    {
        float inputX = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        float inputZ = Mathf.Abs(Input.GetAxisRaw("Vertical"));
        return inputX + inputZ; // Returns 0 when idle
    }

    public void SetSpeed(float newSpeed)
    {
        normalSpeed = newSpeed;
    }
}

