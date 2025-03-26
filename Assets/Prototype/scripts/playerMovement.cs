using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform CameraTransform;
    public Rigidbody rb;
    public Animator anime;
    [SerializeField] private float rotationSpeed = 10f;
    private bool isGrounded;
    Vector3 nullVector;
    void Start()
    { 
        rb.freezeRotation = true;
    }
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;
        transform.position += moveAmount;

        // Animation controller
        bool isWalking = moveAmount != nullVector;
        anime.SetBool("isWalking", isWalking);

        RotateTowardsDirection(direction);//vv imp function

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        anime.SetBool("isJumping", !isGrounded);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)//6== ground layer
        {
            isGrounded = true;
        }
    }

    //This function is used to rotate the player in the direction of movement

    public void RotateTowardsDirection(Vector3 direction)
    {
        // Ensure we have a valid direction (magnitude > 0)
        if (direction.magnitude > 0.01f)
        {
            // Project direction onto XZ plane (ignore Y axis)
            direction.y = 0;
            direction.Normalize();

            // Calculate target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards target (Y-axis only)
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
