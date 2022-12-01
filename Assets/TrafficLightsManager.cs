using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsManager : MonoBehaviour
{
    public GameObject[] lightList;
    float elapsedTime = 0;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 3)
        {
            lightList[0].SetActive(true);
        }
        if(elapsedTime >= 6)
        {
            lightList[1].SetActive(true);
        }
        if(elapsedTime >= 9)
        {
            lightList[2].SetActive(true);
        }

        if (elapsedTime >= 15)
            this.gameObject.SetActive(false);
    }
}
