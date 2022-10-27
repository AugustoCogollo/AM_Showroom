using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] Laptracker laptracker;
    [SerializeField] int vehiclesAmount;

    [SerializeField] GameObject finishLine;
    public GameObject[] vehicles;
    public Dictionary<GameObject, int> players;
    public float dot1To2;
    public float dot2To1;


    void Start()
    {
        vehiclesAmount = vehicles.Length;
        players = new Dictionary<GameObject, int>();
        for(int i = 0; i < vehiclesAmount; ++i)
        {
            players.Add(vehicles[i], i);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            SwapPlaces(vehicles[0], vehicles[1]);
        }
        dot1To2 = Vector3.Dot(vehicles[0].transform.forward, (vehicles[0].transform.position - vehicles[1].transform.position).normalized);
        for(int i = 0; i < vehiclesAmount; ++i)
        {
            for(int j = 0; j < vehiclesAmount; ++j)
            {
                if (GetDistanceBetweenVehicles(vehicles[i], vehicles[j]) > 4f)
                    return;
                if (GetDotProductBetweenVehicles(vehicles[i], vehicles[j]) > 0.7f)
                    return;

                SwapPlaces(vehicles[i], vehicles[j]);

            }
        }

        Debug.Log(GetDistanceBetweenVehicles(vehicles[0], vehicles[1]).ToString());

        //Debug.Log(players.Count);
        //if (dot1To2 > 0.7f)
        //{
        //    Debug.Log("Player 1 is in front of Player 2: " + dot1To2);
        //}
        //else if (dot1To2 < -0.7)
        //{
        //    Debug.Log("Player 1 is behind Player 2: " + dot1To2);
        //    SwapPlaces(vehicles[0], vehicles[1]);
        //}

        //else
        //{
        //    Debug.Log("Player 1 is besides Player 2: " + dot1To2);
        //}
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 50;

        for(int i = 0; i < vehiclesAmount; ++i)
        {
            GUILayout.Label(players[vehicles[i]] + ". " + vehicles[i].name);
        }
    }

    float GetDistanceBetweenVehicles(GameObject vehicle1, GameObject vehicle2)
    {
        return Vector3.Distance(vehicle1.transform.position, vehicle2.transform.position);
    }

    float GetDotProductBetweenVehicles(GameObject front, GameObject behind)
    {
        return Vector3.Dot(front.transform.forward, (front.transform.position - behind.transform.position).normalized);
    }

    void SwapPlaces(GameObject front, GameObject behind)
    {
        int temp = players[front];
        players[front] = players[behind];
        players[behind] = temp;
    }
}
