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
    public AnimationCurve rotationCurve;


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

        currentTime = Mathf.Clamp(currentTime, 0.0f, timeForMaxAcceleration);
        currentTime += Time.deltaTime;

        motorForceToApply = Mathf.Clamp(motorForceToApply, maxBrakeForce, maxMotorForce);

        if(accelerometerInput.y > 0)
        {
            motorForceToApply = accelerationCurve.Evaluate(currentTime) * accelerometerInput.y * maxMotorForce;
            Debug.Log(accelerationCurve.Evaluate(currentTime));
        }

        else
        {
            motorForceToApply = accelerationCurve.Evaluate(currentTime) * accelerometerInput.y * maxBrakeForce;
            brakeForceToApply = 0;
        }

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                //Right touch
                if (touch.position.x > Screen.width / 2)
                {
                    currentSteeringAngle = maxSteerAngle;

                }

                //Left touch
                else if (touch.position.x < Screen.width / 2)
                {
                    currentSteeringAngle = -maxSteerAngle;
                }
            }

            else
                currentSteeringAngle = 0;
        }

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
