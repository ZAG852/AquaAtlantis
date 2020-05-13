using Nodies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    // !World position to graph position translate function
    // Create static adj matrix 
    public Vector2Int nodePosition;
    float XLength;
    public double fScore = double.MaxValue;
    public double gScore = double.MaxValue;
    public double hScore = double.MaxValue;
    public int ID;
    public bool walkable = true;
    // create constructor with right click

    public PathNode()
    {
        nodePosition = new Vector2Int(0, 0);
        this.ID = -1; // a blank node has an ID of -1
    }
    public PathNode(Vector2Int nodePosition, float sideLength, int ID)
    {
        this.nodePosition = nodePosition;
        this.ID = ID;
        XLength = sideLength;
    }


}


