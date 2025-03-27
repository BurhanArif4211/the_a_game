using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    public float normalSpeed = 5f;
    public static float sprintSpeed = 10f;
    private static bool isSprinting = false;
    [SerializeField] private float acceleration = 5f; // Adjust for smoother transition
    private PlayerMovementAdvance movement;

    void Start()
    {
        movement = GetComponent<PlayerMovementAdvance>();
    }

    private float currentSpeed;

    void Update()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift) && PlayerMovementAdvance.GetInputMagnitude() > 0f;
        float targetSpeed = isSprinting ? sprintSpeed : normalSpeed;
        if (isSprinting && !PlayerMovementAdvance.GetIsWalking())
        {
            // Smoothly transition between currentSpeed and targetSpeed
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);
            Debug.Log("Trying to change srinting:" + currentSpeed);
            // Apply the smooth speed to movement
            movement.SetSpeed(currentSpeed);

        }
        else if (PlayerMovementAdvance.GetInputMagnitude() <= 0)
        {
            Debug.Log("Trying to stop Sprinting");
            movement.SetSpeed(0f);

        }
    }


    public static bool GetIsSprinting() => isSprinting;
}

