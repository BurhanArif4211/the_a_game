using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public float crouchSpeed = 2.5f; // Speed while crouching
    public float crouchHeight = 0.5f; // Adjusted height while crouching
    private float originalSpeed;
    private float originalHeight;

    private bool isCrouching = false;
    private CapsuleCollider col;
    private PlayerMovementAdvance movement;
    private Animator anime;

    void Start()
    {
        col = GetComponent<CapsuleCollider>(); // Get the capsule collider
        movement = GetComponent<PlayerMovementAdvance>(); // Reference to movement script
        anime = GetComponent<Animator>(); // Reference to animator

        originalHeight = col.height; // Store original height
        originalSpeed = movement.normalSpeed; // Store original speed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            col.height = crouchHeight; // Reduce player height
            movement.normalSpeed = crouchSpeed; // Slow down movement
            anime.SetBool("isCrouching", true); // Trigger crouch animation
        }
        else
        {
            col.height = originalHeight; // Restore player height
            movement.normalSpeed = originalSpeed; // Restore movement speed
            anime.SetBool("isCrouching", false);
        }
    }
}
