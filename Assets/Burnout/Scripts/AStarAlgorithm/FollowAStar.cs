using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAStar : MonoBehaviour
{
    //Follow Path
    private int curPathIndex;
    public float pathLength;

    public float speed = 20.0f;
    public float curSpeed = 0.0f;

    public float mass = 5.0f;
    private Node curNode;

    private Vector3 targetPoint;
    private Vector3 velocity;
    private ArrayList path = new ArrayList();
    public bool isLooping = true;
    public bool canMove = false;

    //Time
    float elapsedTime = 0.0f;
    public float intervalTime = 0.0f;

    void OnEnable()
    {
        path = FindPathInTrack.instance.pathArray;
        curPathIndex = 0;
        velocity = transform.forward;
        pathLength = path.Count;
        speed = 0;
        canMove = true;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            speed = Random.Range(10.0f, 20.0f);
            elapsedTime = 0;
            Debug.Log(curPathIndex);
        }

        if (canMove)
        {
            curSpeed = speed * Time.deltaTime;
            curNode = (Node)path[curPathIndex];
            targetPoint = curNode.position;

            if (Vector3.Distance(transform.position, targetPoint) < 1)
            {
                if (curPathIndex < pathLength - 1) ++curPathIndex;
                else return;
            }
            if (curPathIndex >= pathLength && isLooping) curPathIndex = 0;
            if (curPathIndex >= pathLength && !isLooping)
                velocity += Steer(targetPoint, true);
            else velocity += Steer(targetPoint);

            transform.position += velocity;
            transform.rotation = Quaternion.LookRotation(velocity);
        }
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
}
