using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [SerializeField] float healthToRecover;

    private void OnTriggerEnter(Collider other)
    {
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (!healthSystem)
            return;

        healthSystem.RecoverHealth(healthToRecover);
    }

}
