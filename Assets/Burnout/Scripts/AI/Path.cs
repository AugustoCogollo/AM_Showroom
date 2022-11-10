using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public bool bDebug = true;
    public float radius = 2.0f;
    public Vector3[] pointA;
    public Color pathColor = Color.red;

    public float GetLength()
    {
        return pointA.Length;
    }

    public Vector3 GetPoint(int index)
    {
        return pointA[index];
    }

    private void OnDrawGizmos()
    {
        if (!bDebug) return;

        for(int i = 0; i < pointA.Length; i++)
        {
            if (i + 1 < pointA.Length)
            {
                Debug.DrawLine(pointA[i], pointA[i + 1], Color.red);
            }
        }
    }

    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
