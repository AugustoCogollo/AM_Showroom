using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeBehaviour : MonoBehaviour
{
    public GameObject editorReference;

    public float attitudeAccordingID;
    public Vector3 axisAccordingID;

    public int platformID;

    bool playerIsInside = false;

    void Start()
    {
        Input.gyro.enabled = true;
        axisAccordingID = Vector3.zero;
    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            DefineAttitudeAccordingToID();

            if (playerIsInside)
                GyroRotateObject();
        }
    }

    private void DefineAttitudeAccordingToID()
    {
        switch (platformID)
        {
            case 1:
                attitudeAccordingID = -Input.gyro.attitude.x;
                axisAccordingID = Vector3.right;
                break;
            case 2:
                attitudeAccordingID = Input.gyro.attitude.y;
                axisAccordingID = Vector3.up;
                break;
            case 3:
                attitudeAccordingID = -Input.gyro.attitude.z;
                axisAccordingID = Vector3.forward;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Player is inside");
            playerIsInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            playerIsInside = false;
        }
    }


    void GyroRotateObject()
    {
        transform.RotateAround(editorReference.transform.position, axisAccordingID, attitudeAccordingID);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Gyro attitude: " + Input.gyro.attitude);
        GUILayout.Label("Is gyroscope enabled? " + Input.gyro.enabled);
        GUILayout.Label("Gravity: " + Input.gyro.gravity);
        GUILayout.Label("Rotation rate: " + Input.gyro.rotationRate);
        GUILayout.Label("Rotation Rate Unbiased" + Input.gyro.rotationRateUnbiased);
        GUILayout.Label("Update Interval: " + Input.gyro.updateInterval);
        GUILayout.Label("User Acceleration: " + Input.gyro.userAcceleration);
    }
}

