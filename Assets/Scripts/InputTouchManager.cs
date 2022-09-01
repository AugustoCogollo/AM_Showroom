using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchManager : MonoBehaviour
{
    void Update()
    {
        CheckForFingers(Input.touches);
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

    private void CheckForFingersTouchingScreen(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
            PrintFingerActivity(touch, " touched the screen");
    }

    private void CheckForFingersMoving(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
            PrintFingerActivity(touch, " is moving");
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
        Debug.Log(touch + log);
    }

    private void CheckIfSystemCancelledTrackingFinger(Touch[] touches)
    {
        foreach(Touch finger in touches)
            if (finger.phase == TouchPhase.Canceled)
                Debug.Log("The system stopped tracking: " + finger);
    }

}
