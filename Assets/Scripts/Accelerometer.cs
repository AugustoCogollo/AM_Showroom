using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    Gyroscope testGyroscope;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

        Screen.orientation = ScreenOrientation.LandscapeLeft;

        testGyroscope = Input.gyro;
        testGyroscope.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Este codgo esta pensado para que el telefono este en vertical -> La tarea es hacer que funcione en horizontal
        rb.AddForce(Input.acceleration);
        Debug.Log("Gyro attitude: " + testGyroscope.attitude.ToString());
        Debug.Log("Gyro rotation rate: " + testGyroscope.rotationRate.ToString());
        Debug.Log("Gyro acceleration: " + testGyroscope.userAcceleration.ToString());
    }
}
