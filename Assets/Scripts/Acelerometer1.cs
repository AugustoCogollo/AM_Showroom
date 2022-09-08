using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerometer1 : MonoBehaviour
{
    Vector3 dirVelocity; // 
    private float speed;
    private float ySalto;
    public float fuerzaSalto;
    private string input = "teasting";
    Rigidbody rb;

    //public float fuerzaGravedad = 9.81f;
    //public bool isGrounded;

    private void Start()
    {
        speed = 2;

        rb = this.gameObject.GetComponent<Rigidbody>();

    }

    private void Update()
    {
        dirVelocity = Input.acceleration;

        HorizontalPhone();
        AcelerationMove();
        Jump();
        //rb.AddForce(dir);

        Debug.DrawRay(transform.position + Vector3.up, dirVelocity, Color.blue);
    }

    private void HorizontalPhone()
    {
        dirVelocity = Quaternion.Euler(90, 0, 0) * dirVelocity;
    }

    private void AcelerationMove()
    {
        rb.velocity = new Vector3(dirVelocity.x * speed, 0, dirVelocity.z * speed);
    }

    private void Jump()
    {
        dirVelocity = Input.acceleration;
        if (dirVelocity.sqrMagnitude >= 5f)
        {
            rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            input = "Salta";
        }
    }
}
