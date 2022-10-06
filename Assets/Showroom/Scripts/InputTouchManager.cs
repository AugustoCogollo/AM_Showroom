using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InputTouchManager : MonoBehaviour
{
    public float touchSensitivityX = 10.0f;
    public float touchSensitivityY = 10.0f;

    CubeMover cubeMover;
    CinemachineFreeLook cinemachine;

    private void Start()
    {
        cubeMover = GetComponent<CubeMover>();
        cinemachine = GetComponent<CinemachineFreeLook>();
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    void Update()
    {
        CheckForFingers(Input.touches);
        CheckAmountOfFingersTouchingTheScreen(Input.touches);
        
    }

    private void LateUpdate()
    {
        CheckIfSystemCancelledTrackingFinger(Input.touches);
    }


    public void CheckForFingers(Touch[] touches)
    {
        foreach(Touch touch in touches)
        {
            CheckForFingersTouchingScreen(touch);
            CheckForFingersMoving(touch);
            CheckForStationaryFingers(touch);
            CheckIfAFingerStoppedTouchingScreen(touch);
        }
    }

    private float HandleAxisInputDelegate(string axisName)
    {
        if (axisName == "MouseX" && Input.touchCount > 0)
            return GetFirstTouchDeltaPositionX();
        else if (axisName == "MouseY" && Input.touchCount > 0)
            return GetFirstTouchDeltaPositionY();
        else
            return Input.GetAxis(axisName);
    }

    private float GetFirstTouchDeltaPositionX()
    {
        return Input.touches[0].deltaPosition.x / touchSensitivityX;
    }

    private float GetFirstTouchDeltaPositionY()
    {
        return Input.touches[0].deltaPosition.y / touchSensitivityY;
    }

    private void CheckAmountOfFingersTouchingTheScreen(Touch[] touches)
    {
        if (Input.touchCount == 0)
            Debug.Log("No fingers are touching the screen");
        else
            Debug.Log(Input.touchCount + " fingers are touching the screen");
    }

    private void CheckForFingersTouchingScreen(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
            PrintFingerActivity(touch, " touched the screen");
    }

    private void CheckForFingersMoving(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
        {

        }
            
    }

    private void CheckForStationaryFingers(Touch touch)
    {
        if (touch.phase == TouchPhase.Stationary)
            PrintFingerActivity(touch, " is not moving");
    }

    private void CheckIfAFingerStoppedTouchingScreen(Touch touch)
    {
        if (touch.phase == TouchPhase.Ended)
            PrintFingerActivity(touch, " stopped touching the screen");
    }
    private void PrintFingerActivity(Touch touch, string log)
    {
        Debug.Log(touch.fingerId + log);
    }

    private void CheckIfSystemCancelledTrackingFinger(Touch[] touches)
    {
        foreach(Touch finger in touches)
            if (finger.phase == TouchPhase.Canceled)
                Debug.Log("The system stopped tracking: " + finger);
    }

}
