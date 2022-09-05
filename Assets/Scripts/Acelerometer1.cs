using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerometer1 : MonoBehaviour
{
    Vector3 dirVelocity; // 
    private float speed;
    float ySalto;
    float fuerzaSalto;
    Rigidbody rb;

    private void Start()
    {
        speed = 2f;
        fuerzaSalto = 5f;
        rb = this.gameObject.GetComponent<Rigidbody>();

    }

    private void Update()
    {
        dirVelocity = Input.acceleration;

        HorizontalPhone();
        AcelerationMove();

        //rb.AddForce(dir);

        Debug.DrawRay(transform.position + Vector3.up, dirVelocity, Color.blue);

        Jump();

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
            //rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
            rb.velocity = new Vector3(0, fuerzaSalto, 0);
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }


    }

}

