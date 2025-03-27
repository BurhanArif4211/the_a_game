using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float sprintSpeed = 10f;
    private PlayerMovementAdvance movement;

    void Start()
    {
        movement = GetComponent<PlayerMovementAdvance>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement.SetSpeed(sprintSpeed);
        }
        else
        {
            movement.SetSpeed(normalSpeed);
        }
    }
}

