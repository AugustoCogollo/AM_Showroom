using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerBehaviour : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    public bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 tiltSpeed = Input.acceleration;

        tiltSpeed = Quaternion.Euler(90, 0, 0) * tiltSpeed;

        Vector3 movement = transform.right * tiltSpeed.x + transform.forward * tiltSpeed.z;

        controller.Move(movement * speed * Time.deltaTime);

        if(tiltSpeed.sqrMagnitude >= 5f && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
