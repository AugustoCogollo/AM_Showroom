using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public List<GameObject> vehicles = new List<GameObject>();
    public Dictionary<GameObject, int> vehicleValues = new Dictionary<GameObject, int>();
    public Dictionary<GameObject, int> leaderboard; //Sorted vehicleValues
    float lastUpdate; 
    float timeToUpdateLeaderboard = 2;

    void Start()
    {
        lastUpdate = Time.realtimeSinceStartup;
        for(int i = 0; i < vehicles.Count; ++i)
        {
            vehicleValues.Add(vehicles[i], vehicles[i].GetComponent<VehiclePosition>().trackPoints);
        }
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup - lastUpdate > timeToUpdateLeaderboard)
        {
            for (int i = 0; i < vehicles.Count; ++i)
            {
                Debug.Log(vehicles[i].name + " " + vehicleValues[vehicles[i]] + '\n');
            }
            Debug.Log(DotProductBetweenVehicles(vehicles[0], vehicles[1]).ToString());

            lastUpdate = Time.realtimeSinceStartup;
        }
        Debug.Log("Count: " + vehicleValues.Count.ToString());
    }

    //private void OnGUI()
    //{
    //    GUI.skin.label.fontSize = Screen.width / 50;
    //    for(int i = 0; i < vehicles.Capacity; ++i)
    //    {
            
    //    }
    //}

    private float DotProductBetweenVehicles(GameObject front, GameObject back)
    {
        return Vector3.Dot(front.transform.forward, (front.transform.position - back.transform.position.normalized));
    }

    public string GetVehiclePosition(GameObject vehicle)
    {
        return vehicleValues[vehicle].ToString();
    }
}
