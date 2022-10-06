using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDrag : MonoBehaviour
{

    //A camera that emits radiation
    private Camera cam;
    //Objects colliding with rays
    private GameObject go;
    //The name of the object the ray collides with
    public static string btnName;
    private Vector3 screenSpace;
    private Vector3 offset;
    private bool isDrage = false;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Overall initial position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Ray from camera to click coordinate
        RaycastHit hitInfo;
        if (isDrage == false)
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                //The scribed rays can only be seen in the scene view
                Debug.DrawLine(ray.origin, hitInfo.point);
                go = hitInfo.collider.gameObject;
                print(btnName);
                screenSpace = cam.WorldToScreenPoint(go.transform.position);
                offset = go.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                //The name of the object
                btnName = go.name;
                //Name of component
            }
            else
            {
                btnName = null;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) /*+ offset*/;
            if (btnName != null)
            {
                go.transform.position = currentPosition;
            }
            isDrage = true;
        }
        else
        {
            isDrage = false;
        }
    }

}