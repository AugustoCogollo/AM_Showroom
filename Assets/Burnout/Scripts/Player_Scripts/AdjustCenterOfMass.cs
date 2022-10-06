using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AdjustCenterOfMass : MonoBehaviour
{
    [SerializeField] Vector3 centerOfMass = Vector3.zero;
    [SerializeField] float COMRadius = 0.125f;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass += centerOfMass;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 currentCenterOfMass = this.GetComponent<Rigidbody>().worldCenterOfMass;
        Gizmos.DrawSphere(currentCenterOfMass + centerOfMass, COMRadius);
    }
}
