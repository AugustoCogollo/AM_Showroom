using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public List<GameObject> vehicles;
    public SortedList<int, GameObject> leaderboard;
    float lastUpdate; 
    float timeToUpdateLeaderboard = 2;

    void Start()
    {
        lastUpdate = Time.realtimeSinceStartup;
        for(int i = 0; i < vehicles.Capacity; ++i)
        {
            leaderboard.Add(vehicles[i].GetComponent<VehiclePosition>().trackPoints, vehicles[i]);
        }
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup - lastUpdate > timeToUpdateLeaderboard)
        {
            Debug.Log(leaderboard.Keys.ToString() + leaderboard.Values.ToString());

            lastUpdate = Time.realtimeSinceStartup;
        }
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 50;
        for(int i = 0; i < vehicles.Capacity; ++i)
        {
            
        }
    }

    public string GetVehiclePosition(GameObject vehicle)
    {
        return vehicle.name;
    }
}
