using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] Laptracker laptracker;
    [SerializeField] float distanceFromFinishLine = 0.0f;
    [SerializeField] int vehiclesAmount;

    [SerializeField] GameObject finishLine;
    public GameObject[] vehiclesPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
