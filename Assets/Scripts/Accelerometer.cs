using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    Rigidbody rb;

    Vector3 tiltSpeed;

    public float speed;
    public float jumpHeight;
    public float shakeDetectionThreshold;

    public float fallScale;
    public float gravity = 2;

    public bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tiltSpeed = Input.acceleration;
        ChangePhoneDirectionToHorizontal();
        MoveObjectWithVelocity();
        CheckForPhoneMovementY();
        Debug.DrawRay(transform.position + Vector3.up, tiltSpeed, Color.cyan);

        Debug.Log("Velocity.y: " + rb.velocity.y.ToString());

        if (!isGrounded)
        {
            rb.AddForce(new Vector3(0, fallScale, 0), ForceMode.Impulse);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void ChangePhoneDirectionToHorizontal()
    {
        tiltSpeed = Quaternion.Euler(90, 0, 0) * tiltSpeed;
    }

    private void MoveObjectWithVelocity()
    {
        rb.velocity = new Vector3(tiltSpeed.x * speed, 0, tiltSpeed.z * speed);
    }

    private void CheckForPhoneMovementY()
    {
        if (tiltSpeed.sqrMagnitude >= shakeDetectionThreshold)
        {
            JumpIfIsGrounded();
            Debug.Log("Jumped");
        }
    }

    void JumpIfIsGrounded()
    {
        if (isGrounded)
        {
            float jumpingVelocity = (gravity * jumpHeight);
            rb.AddForce(new Vector3(0, jumpingVelocity, 0), ForceMode.Impulse);
            isGrounded = false;
        }
        
    }
}
