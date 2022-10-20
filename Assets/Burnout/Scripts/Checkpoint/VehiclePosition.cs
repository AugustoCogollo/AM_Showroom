using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehiclePosition : MonoBehaviour
{
    TextMeshPro position;
    [SerializeField] Leaderboard leaderboard;

    void Start()
    {
        position = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        position.text = leaderboard.players[this.gameObject].ToString();   
    }
}
