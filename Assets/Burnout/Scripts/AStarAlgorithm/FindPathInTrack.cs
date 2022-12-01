using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FindPathInTrack : MonoBehaviour
{
    private Transform startPos, endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }
    public ArrayList pathArray;
    public List<Vector3> pathList;
    GameObject objStartCube, objEndCube;
    private float elapsedTime = 0.0f;
    //Interval time between pathfinding
    public float intervalTime = 1.0f;

    //Follow Path
    int curPathIndex = 0;
    float pathLength;

    public float speed = 20.0f;
    public float curSpeed = 0.0f;
    public float speedY;
    public float mass = 5.0f;
    Node curNode;

    private Vector3 targetPoint;
    Vector3 velocity;
    public bool isLooping = true;

    void Start()
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");
        pathArray = new ArrayList();
        FindPath();

        transform.position = objStartCube.transform.position;

        curPathIndex = 0;
        pathLength = pathArray.Count;
        velocity = transform.forward;
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            speed = Random.Range(10.0f, 20.0f);
            elapsedTime = 0;
        }

        curSpeed = speed * Time.deltaTime;
        curNode = (Node)pathArray[curPathIndex];
        targetPoint = curNode.position;

        if (Vector3.Distance(transform.position, targetPoint) < 2)
        {
            if (curPathIndex < pathLength - 1) ++curPathIndex;
            else return;
        }
        if (curPathIndex >= pathLength && isLooping) curPathIndex = 0;
        if (curPathIndex >= pathLength - 1 && !isLooping)
            velocity += Steer(targetPoint, true);
        else velocity += Steer(targetPoint);

        transform.position += velocity;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    {
        Vector3 desiredVelocity = (target - transform.position);
        float dist = desiredVelocity.magnitude;

        desiredVelocity.Normalize();

        if (bFinalPoint && dist < 10.0f) desiredVelocity *=
            (curSpeed * (dist / 10.0f));
        else desiredVelocity *= curSpeed;

        Vector3 steeringForce = desiredVelocity - velocity;
        Vector3 acceleration = steeringForce / mass;
        return acceleration;
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
