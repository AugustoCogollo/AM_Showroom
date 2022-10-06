using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelShip
{
    public WheelCollider collider;

    public bool isPowered;
    public bool isSteerable;
    public bool hasBrakes;
}

public class Vehicle : MonoBehaviour
{
    [Header("Ship values")]
    [SerializeField] WheelShip[] wheelsShip;

    [Header("Animations Curves")]
    public AnimationCurve accelerationCurve;
    public AnimationCurve brakingCurve;



    [Header("Ship constraints")]
    [SerializeField] float maxMotorForce = 1000f;
    [SerializeField] float maxBrakeForce = 2000f;
    [SerializeField] float maxSteerAngle = 45f;

    private Vector3 accelerometerInput;
    [Header("Values")]
    [SerializeField] float motorForceToApply;
    [SerializeField] float brakeForceToApply;
    [SerializeField] float currentSteeringAngle;
    public float timeForMaxAcceleration = 10f;
    public float currentTime;

    void Update()
    {
        accelerometerInput = Input.acceleration;
        accelerometerInput.Normalize();

        currentTime += Time.deltaTime;

        while (currentTime < timeForMaxAcceleration)
        {
            accelerationCurve.Evaluate(currentTime);
        }

        if(accelerometerInput.y > 0)
        {
            motorForceToApply = accelerometerInput.y * maxMotorForce;
        }
        else
        {
            motorForceToApply = accelerometerInput.y * maxBrakeForce;
            brakeForceToApply = 0;
        }

        currentSteeringAngle = accelerometerInput.x * maxSteerAngle;

        for(int i = 0; i < wheelsShip.Length; ++i)
        {
            WheelShip wheel = wheelsShip[i];

            if (wheel.isPowered)
                wheel.collider.motorTorque = motorForceToApply;

            if (wheel.isSteerable)
                wheel.collider.steerAngle = currentSteeringAngle;

            if (wheel.hasBrakes)
                wheel.collider.brakeTorque = brakeForceToApply;
        }

    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 50;

        GUILayout.Label("Accelerometer X: " + Input.acceleration.x);
        GUILayout.Label("Accelerometer Y: " + Input.acceleration.y);
        GUILayout.Label("Accelerometer Z: " + Input.acceleration.z);
    }
}
