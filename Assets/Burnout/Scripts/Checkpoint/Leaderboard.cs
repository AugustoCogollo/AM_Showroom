using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public List<GameObject> vehicles = new List<GameObject>();
    public Dictionary<GameObject, int> vehicleToInt = new Dictionary<GameObject, int>();
    public Dictionary<int, GameObject> intToVehicle = new Dictionary<int, GameObject>();
    public List<int> leaderboard = new List<int>();
    float lastUpdate; 
    float timeToUpdateLeaderboard = 0.5f;

    void Start()
    {   
        lastUpdate = Time.realtimeSinceStartup;
        for(int i = 0; i < vehicles.Count; ++i)
        {
            vehicleToInt.Add(vehicles[i], vehicles[i].GetComponent<VehiclePosition>().trackPoints);
            intToVehicle.Add(vehicles[i].GetComponent<VehiclePosition>().trackPoints, vehicles[i]);
            leaderboard.Add(vehicleToInt[vehicles[i]]);
        }
        leaderboard.Sort();
        leaderboard.Reverse();
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup - lastUpdate > timeToUpdateLeaderboard)
        {
            for (int i = 0; i < vehicles.Count; ++i)
            {
                vehicleToInt[vehicles[i]] = vehicles[i].GetComponent<VehiclePosition>().trackPoints;
                intToVehicle[vehicles[i].GetComponent<VehiclePosition>().trackPoints] = vehicles[i];
                //Debug.Log(vehicleToInt[vehicles[i]] + ". " + vehicles[i].GetComponent<VehiclePosition>().racePosition.ToString() + '\n');
            }
            UpdateLeaderboard();
            lastUpdate = Time.realtimeSinceStartup;
        }
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 50;
        string guiOutput;
        for (int i = 0; i < vehicles.Capacity; ++i)
        {
            guiOutput = vehicles[i].GetComponent<VehiclePosition>().racePosition.ToString();
        }
    }

    private void UpdateLeaderboard()
    {
        for(int i = 0; i < leaderboard.Count; ++i)
        {
            leaderboard[i] = GetVehiclePoints(vehicles[i]);
        }
        leaderboard.Sort();
        leaderboard.Reverse();
        for (int i = 0; i < leaderboard.Count; ++i)
        {
            vehicles[i].GetComponent<VehiclePosition>().racePosition = leaderboard.IndexOf(vehicleToInt[vehicles[i]]) + 1;

        }
    }

    private int GetVehiclePoints(GameObject vehicle)
    {
        return vehicleToInt[vehicle];
    }

    private GameObject GetPointsFromVehicle(int trackPoints)
    {
        return intToVehicle[trackPoints];
    }
}
