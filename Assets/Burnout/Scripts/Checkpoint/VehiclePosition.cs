using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehiclePosition : MonoBehaviour
{
    [SerializeField] Leaderboard leaderboard;
    private TextMeshPro textPosition;
    public List<GameObject> checkpoints;
    public int trackPoints = 0;
    public int racePosition;

    void Start()
    {
        textPosition = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        textPosition.text = racePosition.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched a checkpoint");
        if(other.transform.tag == "Checkpoint")
        {
            if (checkpoints.Contains(other.gameObject))
            {
                return;
            }
            else
            {
                ++trackPoints;
                checkpoints.Add(other.gameObject);
            }
        }
        if (other.GetComponent<Checkpoint>().isLapStart)
        {
            CleanAfterNewLap();
        }
    }

    public void CleanAfterNewLap()
    {
        checkpoints.Clear();
    }

    public void UpdatePosition(int newPosition)
    {
        racePosition = newPosition;
    }
}
