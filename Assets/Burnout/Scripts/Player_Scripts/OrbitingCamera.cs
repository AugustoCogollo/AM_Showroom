using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("Camera Sensitivity")]
    [SerializeField] float rotationSpeed = 120f;
    [SerializeField] float elevationSpeed = 120f;

    [Header("Elevation")]
    [SerializeField] float elevationMinLimit = -20f;
    [SerializeField] float elevationMaxLimit = 80f;

    [Header("Target Values")]
    [SerializeField] float distance = 5f;
    [SerializeField] float minDistance = 0.5f;
    [SerializeField] float maxDistance = 10f;

    [Header("Rotation values")]
    [SerializeField] float minRotation = -180f;
    [SerializeField] float maxRoation = 180f;

    float rotationAroundTarget = 0.0f;
    float elevationToTarget = 0.0f;

    Vector3 accelerometerInput;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationAroundTarget = angles.x;
        elevationToTarget = angles.y;

        if (target)
        {
            float currentDistance = (transform.position - target.position).magnitude;
            distance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        }
    }

    private void LateUpdate()
    {
        accelerometerInput = Input.acceleration;
        accelerometerInput.Normalize();


        if (target)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                //Left Touch
                if(touch.position.x < Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Moved )
                    {
                        rotationAroundTarget -= rotationSpeed * distance * 0.02f;
                    }
                }

                //Right Touch
                else if(touch.position.x > Screen.width / 2)
                {
                    if (touch.phase == TouchPhase.Stationary)
                    {
                        rotationAroundTarget += rotationSpeed * distance * 0.02f;
                    }
                }
                
            }

            elevationToTarget += elevationSpeed * 0.02f;

            elevationToTarget = ClampAngle(elevationToTarget, elevationMinLimit, elevationMaxLimit);
            //rotationAroundTarget = ClampAngle(rotationAroundTarget, minRotation, maxRoation);

            Quaternion rotation = Quaternion.Euler(elevationToTarget, rotationAroundTarget, 0);

            transform.rotation = rotation;

            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Vector3 negDistance = new Vector3(0f, 0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.position = position;
        }

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360f)
            angle -= 360f;

        if (angle < -360f)
            angle += 360;

        return Mathf.Clamp(angle, min, max);
    }
}
