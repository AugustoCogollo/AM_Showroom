using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehiclePosition : MonoBehaviour
{
    [SerializeField] Leaderboard leaderboard;
    TextMeshPro position;
    public int trackPoints = 0;
    public List<GameObject> checkpoints;

    void Start()
    {
        position = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        
        position.text = leaderboard.GetVehiclePosition(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
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
    }

    public void CleanAfterNewLap()
    {
        checkpoints.Clear();
    }
}
