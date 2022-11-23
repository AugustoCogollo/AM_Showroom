using UnityEngine;
using System.Collections.Generic;
using System;

public class Node : IComparable
{
    public float nodeTotalCost; //G
    public float estimatedCost; //H
    public bool isObstacle;
    public Node parent;
    public Vector3 position;

    public Node()
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.isObstacle = false;
        this.parent = null;
    }

    public Node(Vector3 pos)
    {
        this.estimatedCost = 0.0f;
        this.estimatedCost = 0.0f;
        this.isObstacle = false;
        this.parent = null;
        this.position = pos;
    }

    public void MarkAsObstacle()
    {
        this.isObstacle = true;
    }

    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        if (this.estimatedCost < node.estimatedCost) return -1;
        if (this.estimatedCost > node.estimatedCost) return 1;
        return 0;
    }
}
