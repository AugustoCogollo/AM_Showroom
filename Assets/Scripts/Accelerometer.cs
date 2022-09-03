using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Este codgo esta pensado para que el telefono este en vertical -> La tarea es hacer que funcione en horizontal
        rb.AddForce(Input.acceleration);
    }
}
