using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FindPathInTrack : MonoBehaviour
{
    public static FindPathInTrack s_Instance = null;

    public static FindPathInTrack instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(FindPathInTrack)) as FindPathInTrack;
                if (s_Instance == null)
                {
                    Debug.Log("Could not locate a FindPathInTrack object.\n You have to have exactly one GridManager in the scene");
                }
            }
            return s_Instance;
        }
    }

    private Transform startPos, endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }
    public ArrayList pathArray = new ArrayList();
    public GameObject[] vehiclesList;
    public GameObject objStartCube, objEndCube;
    
    //Follow Path
    public float pathLength;
    public float mass = 5.0f;

    void Start()
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");
        FindPath();
        pathLength = pathArray.Count;
        vehiclesList = GameObject.FindGameObjectsWithTag("IA");
        foreach(GameObject vehicle in vehiclesList)
        {
            vehicle.GetComponent<FollowAStar>().enabled = !vehicle.GetComponent<FollowAStar>().enabled;
        }
    }

    void FindPath()
    {
        startPos = objStartCube.transform;
        endPos = objEndCube.transform;
        startNode = new Node(GridManager.instance.GetGridCellCenter(
        GridManager.instance.GetGridIndex(startPos.position)));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(
        GridManager.instance.GetGridIndex(endPos.position)));
        pathArray = AStar.FindPath(startNode, goalNode);
    }
    void OnDrawGizmos()
    {
        if (pathArray == null)
            return;
        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            }
        }
    }
}
