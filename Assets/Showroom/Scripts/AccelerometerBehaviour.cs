using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerBehaviour : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement")]
    public float speed = 12f;
    public float gravity = -9.81f;

    [Header("Jump")]
    public float jumpHeight = 3f;

    [Header("Ground Check Properties")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    Vector3 tiltSpeed;
    Vector3 movement;

    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        tiltSpeed = Input.acceleration;



        CheckForGround();
        ResetVerticalVelocityWhenGrounded();
        ChangeDeviceOrientationToHorizontal();

        MoveObject();
        HandleJump();

        HandleGravity();


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, groundDistance);
    }

    private void CheckForGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void ResetVerticalVelocityWhenGrounded()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void ChangeDeviceOrientationToHorizontal()
    {
        tiltSpeed = Quaternion.Euler(90, 0, 0) * tiltSpeed;
    }
    private void MoveObject()
    {
        movement = transform.right * tiltSpeed.x + transform.forward * tiltSpeed.z;
        controller.Move(movement * speed * Time.deltaTime);
    }

    private void HandleJump()
    {
        /*if (tiltSpeed.sqrMagnitude >= 5f && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }*/

        if(tiltSpeed.y >= 5f && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if(Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }
    }

    private void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            isGrounded = true;
        }
        
    }
}
