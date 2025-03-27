using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anime;
    private PlayerMovementAdvance movement;
    private PlayerJump jump;

    void Start()
    {
        movement = GetComponent<PlayerMovementAdvance>();
        jump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (movement != null)
        {
            // Check if the player is actually moving
            bool isWalking = movement.GetInputMagnitude() > 0;
            anime.SetBool("isWalking", isWalking);
        }

        if (jump != null)
        {
            anime.SetBool("isJumping", !jump.GetIsGrounded());
        }
    }
}

