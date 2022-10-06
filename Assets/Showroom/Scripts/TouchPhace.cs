using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPhace : MonoBehaviour
{
    public bool isActive;

    // Use this for initialization
    void Start()
    {


    }
    void Update()
    {


        if (isActive == false)
        {
            foreach (Touch touch in Input.touches)
            {
                Funciones(touch);
            }

        }
    }
    private void Began(Touch touch)
    {


        //if (Input.touchCount == 1)
        //{
        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("toco una vez");
        }
        //} 


    }
    private void Moved(Touch touch)
    {

        //if (Input.touchCount == 1)
        //{
        if (touch.phase == TouchPhase.Moved)
        {
            Debug.Log("Moved");
        }
        //}
    }
    private void Stationary(Touch touch)
    {

        //if (Input.touchCount == 1)
        //{
        if (touch.phase == TouchPhase.Stationary)
        {
            Debug.Log("no se mueve");
        }
        //}
    }

    private void Ended(Touch touch)
    {

        //if (Input.touchCount == 0)
        //{
        if (touch.phase == TouchPhase.Ended)
        {
            Debug.Log("end");
        }
        //}
    }
    private void Canceled(Touch touch)
    {

        //if (Input.touchCount == 1)
        //{
        if (touch.phase == TouchPhase.Canceled)
        {
            Debug.Log("cancel");
        }
        //}

    }
    private void Funciones(Touch touch)
    {

        Began(touch);
        Moved(touch);
        Stationary(touch);
        Ended(touch);
        Canceled(touch);
    }
}