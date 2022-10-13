using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] float boostDuration = 2.0f;
    [SerializeField] float boostForce = 50.0f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody body = other.GetComponentInParent<Rigidbody>();
        if (!body)
            return;

        ConstantForce boost = body.gameObject.AddComponent<ConstantForce>();
        boost.relativeForce = Vector3.forward * boostForce;

        Destroy(boost, boostDuration);
    }
}
