using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giroscopio : MonoBehaviour
{
    public GameObject VRCamera;

    private float PosInicialY = 0f; 
    private float PosdelGiroY = 0f;
    private float CalibrarPosY = 0f;

    public bool PlayGame; 

    void Start()
    {
        Input.gyro.enabled = true;
        PosInicialY = VRCamera.transform.eulerAngles.y; 
    }

    
    void Update()
    {
        AplicarRotacionGiroscopio();
        AplicarCalibracion(); 

        if(PlayGame == true)
        {
            Invoke("CalibrarenPocisionY", 3f);
            PlayGame = false; 
        }
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Gyro attitude: " + Input.gyro.attitude);
        GUILayout.Label("Rotation rate: " + Input.gyro.rotationRateUnbiased);
        GUILayout.Label("Update Interval: " + Input.gyro.updateInterval);
        GUILayout.Label("User Acceleration: " + Input.gyro.userAcceleration);
        GUILayout.Label("Gravity: " + Input.gyro.gravity);
    }

    void AplicarRotacionGiroscopio()
    {
        VRCamera.transform.rotation = Input.gyro.attitude;
        VRCamera.transform.Rotate(0f, 0f, 180f, Space.Self);
        VRCamera.transform.Rotate(90f, 180f, 0f, Space.World);
        PosdelGiroY = VRCamera.transform.eulerAngles.y; 
    }

    void CalibrarenPocisionY()
    {
        CalibrarPosY = PosdelGiroY - PosInicialY; 
    }

    void AplicarCalibracion()
    {
        VRCamera.transform.Rotate(0, -CalibrarPosY, 0f, Space.World); 
    }
}
