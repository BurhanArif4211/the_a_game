using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public Rigidbody rb;
    Animator anime;
    private bool isGrounded;

    void Start()
    {
        anime = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (isGrounded)
        {
            anime.SetBool("isJumping", false);
        }
        else
        {
            anime.SetBool("isJumping", true);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) // Ground layer
        {
            isGrounded = true;
        }
    }

    public bool GetIsGrounded() => isGrounded;
}
