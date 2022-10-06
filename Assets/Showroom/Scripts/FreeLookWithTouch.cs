using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookWithTouch : MonoBehaviour
{
    public float TouchSensitivityX = 10.0f;
    public float TouchSensitivityY = 10.0f;

    public CinemachineFreeLook freeLookCamera;

    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = Input.touches[0].deltaPosition.x;
            freeLookCamera.m_YAxis.m_InputAxisValue = Input.touches[1].deltaPosition.y;
        }
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {
            case "MouseX":

                if (Input.touchCount > 0)
                {
                    return Input.touches[0].deltaPosition.x / TouchSensitivityX;
                }
                else
                    return Input.GetAxis(axisName);

            case "MouseY":
                if (Input.touchCount > 0)
                {
                    return Input.touches[0].deltaPosition.y / TouchSensitivityY;
                }
                else
                    return Input.GetAxis(axisName);
        }

        return 0f;
    }
}
