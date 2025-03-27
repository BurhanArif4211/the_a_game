using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [Header("Crouch Settings")]
    public float crouchSpeed = 2.5f;  // Speed while crouching
    public float crouchHeight = 0.5f; // Adjusted height while crouching

    private float originalSpeed;
    private float originalHeight;
    private bool isCrouching = false;

    private CapsuleCollider col;
    private Animator anime;
    private Vector3 originalCenter;
    private bool isCrouchWalking =false;
    [Header("References")]
    public PlayerMovement movement;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        anime = GetComponent<Animator>();

        originalCenter = col.center;
        originalHeight = col.height;
        originalSpeed = movement.normalSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        isCrouchWalking = (inputX != 0 || inputZ != 0);
        if (isCrouchWalking)
        {
            anime.SetBool("isCrouchWalking", true);
        }
        else
        {
            anime.SetBool("isCrouchWalking", false);
        }

        //  Exit crouch when sprinting is attempted
        if (isCrouching && Input.GetKey(KeyCode.LeftShift))
        {
            ExitCrouch();
            movement.StartSprint();
        }
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            col.height = crouchHeight;
            col.center = new Vector3(originalCenter.x, 0.32f, originalCenter.z);
            movement.normalSpeed=crouchSpeed; // Slow down movement
            anime.SetBool("isCrouching", true);
        }
        else
        {
            ExitCrouch();
        }
    }

    private void ExitCrouch()
    {
        isCrouching = false;
        col.center = originalCenter;
        col.height = originalHeight;
        movement.SetSpeed(originalSpeed);
        anime.SetBool("isCrouching", false);
    }

    public bool IsCrouching() => isCrouching;
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCrouch : MonoBehaviour
//{
//    public float crouchSpeed = 2.5f; // Speed while crouching
//    public float crouchHeight = 0.5f; // Adjusted height while crouching
//    private float originalSpeed;
//    private float originalHeight;

//    private bool isCrouching = false;
//    private CapsuleCollider col;
//    public PlayerMovement movement;
//    private Animator anime;
//    Vector3 original_center;

//    void Start()
//    {
//        col = GetComponent<CapsuleCollider>(); // Get the capsule collider
//        anime = GetComponent<Animator>(); // Reference to animator
//        original_center = col.center;
//        Debug.Log(original_center);
//        originalHeight = col.height; // Store original height
//        originalSpeed = movement.normalSpeed; // Store original speed
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            ToggleCrouch();
//        }
//    }
//    void ToggleCrouch()
//    {
//        isCrouching = !isCrouching;

//        if (isCrouching)
//        {
//            col.height = crouchHeight; // Reduce player height
//            col.center = new Vector3(original_center.x, 0.32f, original_center.z); // Move collider center down
//            movement.normalSpeed = crouchSpeed; // Slow down movement
//            anime.SetBool("isCrouching", true); // Trigger crouch animation
//        }
//        else
//        {
//            col.center=original_center ;
//            col.height = originalHeight; // Restore player height
//            movement.normalSpeed = originalSpeed; // Restore movement speed
//            anime.SetBool("isCrouching", false);
//        }
//    }
//    public bool GetIsCrouching() => isCrouching;
//}
