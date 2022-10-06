using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Forward Movement")]
    [Min(0.01f)]
    public float topSpeed = 10f;
    public float acceleration = 5f;


    [Header("Reverse Movement")]
    [Min(0.01f)]
    public float reverseSpeed = 5f;
    public float reverseAcceleration = 5f;

    [Header("Movement Values")]
    public float braking = 10f;
    public float dragSpeed = 4f;
    [Range(0.0f, 1.0f)]
    public float sideGrip = 0.95f;
    public float steer = 5f;
    Vector3 velocity;

    private bool canMove = false;

    [Header("Gravity Movement")]
    public float gravity = Physics.gravity.y;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;
    private bool isGrounded;

    [Header("Vehicle Physics")]
    public Transform centerOfMass;

    Rigidbody rb;
    Vector3 accelerationInput;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChangeDeviceOrientationToHorizontal();
    }

    private void ChangeDeviceOrientationToHorizontal() => accelerationInput = Quaternion.Euler(90, 0, 0) * accelerationInput;

    private void FixedUpdate()
    {
        accelerationInput = Input.acceleration;

        accelerationInput.Normalize();

    }

    public void SetCanMove(bool move) => canMove = move;

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 50;

        GUILayout.Label("Accelerometer X: " + Input.acceleration.x);
        GUILayout.Label("Accelerometer Y: " + Input.acceleration.y);
    }

    private void ResetVerticalVelocityWhenGrounded()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

}
