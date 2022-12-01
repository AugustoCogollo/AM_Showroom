using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Laptracker : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] int longestPermittedShortcut = 2;
    [SerializeField] GameObject wrongWayIndicator;
    [SerializeField] TMP_Text lapCounter;
    public List<VehiclePosition> vehicles;

    int lapsComplete = 0;

    [Header("Time Values")]
    public float totalTime;
    public float currentLapTime;
    public float bestLapTime;

    [Header("UI Values")]
    public TMP_Text totalTimeTxt;
    public TMP_Text bestLapTxt;

    public Checkpoint lastSeenCheckpoint;
    Checkpoint[] allCheckpoints;
    Checkpoint StartCheckpoint { 
        get
        {
            return FindObjectsOfType<Checkpoint>().Where(c => c.isLapStart).FirstOrDefault();
        }
    }

    Leaderboard leaderboard;

    void Start()
    {
        UpdateLapCounter();

        wrongWayIndicator.SetActive(false);

        allCheckpoints = FindObjectsOfType<Checkpoint>();

        CreateCircuit();

        lastSeenCheckpoint = StartCheckpoint;

        bestLapTime = 0;

        leaderboard = GameObject.FindObjectOfType<Leaderboard>();
    }

    private void Update()
    {
        bestLapTxt.text = (Mathf.Floor(bestLapTime / 60)).ToString("00") + " : " + (bestLapTime % 60).ToString("00");
        totalTime += Time.deltaTime;
        currentLapTime += Time.deltaTime;
        
        totalTimeTxt.text = (Mathf.Floor(totalTime/60)).ToString("00") + " : " + (totalTime % 60).ToString("00");


        var nearestCheckpoint = NearestCheckpoint();

        if (nearestCheckpoint == null)
        {
            return;
        }

        if (nearestCheckpoint.index == lastSeenCheckpoint.index)
        {
            // nothing to do
        }
        else if (nearestCheckpoint.index > lastSeenCheckpoint.index)
        {

            var distance = nearestCheckpoint.index - lastSeenCheckpoint.index;

            if (distance > longestPermittedShortcut + 1)
            {
                wrongWayIndicator.SetActive(true);
            }
            else
            {
                lastSeenCheckpoint = nearestCheckpoint;
                wrongWayIndicator.SetActive(false);
            }

        }
        else if (nearestCheckpoint.isLapStart && lastSeenCheckpoint.next.isLapStart)
        {
            lastSeenCheckpoint = nearestCheckpoint;

            lapsComplete += 1;
            UpdateLapCounter();

            for(int i = 0; i < vehicles.Count; ++i)
            {
                vehicles[i].CleanAfterNewLap();
            }

            if(currentLapTime > bestLapTime)
            {
                bestLapTime = currentLapTime;
            }
            currentLapTime = 0;
        }
        else
        {
            wrongWayIndicator.SetActive(true);
        }

        
    }

    Checkpoint NearestCheckpoint()
    {

        if (allCheckpoints == null)
        {
            return null;
        }

        Checkpoint nearestSoFar = null;
        float nearestDistanceSoFar = float.PositiveInfinity;

        for (int c = 0; c < allCheckpoints.Length; c++)
        {
            var checkpoint = allCheckpoints[c];
            var distance = (target.position - checkpoint.transform.position).sqrMagnitude;

            if (distance < nearestDistanceSoFar)
            {
                nearestSoFar = checkpoint;
                nearestDistanceSoFar = distance;
            }
        }

        return nearestSoFar;
    }
    void CreateCircuit()
    {
        var index = 0;

        var currentCheckpoint = StartCheckpoint;

        do
        {
            currentCheckpoint.index = index;
            index += 1;

            currentCheckpoint = currentCheckpoint.next;

            if (currentCheckpoint == null)
            {
                Debug.LogError("The circuit is not closed!");
                return;
            }
        } 
        while (currentCheckpoint.isLapStart == false);

    }

    void UpdateLapCounter()
    {
        lapCounter.text = string.Format("Lap {0}/3", lapsComplete + 1);
    }

    private void OnDrawGizmos()
    {
        var nearest = NearestCheckpoint();
        if (target != null && nearest != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(target.position, nearest.transform.position);
        }
        if(lapsComplete == 3)
        {
            foreach(GameObject vehicle in leaderboard.vehicles)
            {
                if (vehicle.gameObject.tag == "IA")
                    vehicle.GetComponent<FollowAStar>().canMove = false;
                else
                    vehicle.GetComponent<Vehicle>().canMove = false;
            }
        }
    }
}