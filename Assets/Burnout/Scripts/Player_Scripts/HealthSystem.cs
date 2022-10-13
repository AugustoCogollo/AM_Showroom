using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float healthPoints = 100.0f;
    [SerializeField] float wallDamage = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            healthPoints -= wallDamage;
        }
    }

    public void RecoverHealth(float healthPoints)
    {
        this.healthPoints += healthPoints;
    }

    public void LoseHealth(float healthPoints)
    {
        this.healthPoints -= healthPoints;
    }
}
