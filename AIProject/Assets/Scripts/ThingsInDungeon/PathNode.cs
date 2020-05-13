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
    public bool walkable = true;
    // create constructor with right click

    public PathNode()
    {
        nodePosition = new Vector2Int(0, 0);
    }
    public PathNode(Vector2Int nodePosition, float sideLength)
    {
        this.nodePosition = nodePosition;
        XLength = sideLength;
    }

    public double DistanceFrom(GameObject player)
    {
        // distance function to find the distance from the player location to this node
        Vector2Int targetNode = PathGrid.Translate(player.transform.position);
        // the grid index of this node is stored in nodeposition. simply count the h + V distance
        int xdist = Mathf.Abs(nodePosition.x - targetNode.x);
        int ydist = Mathf.Abs(nodePosition.y - targetNode.y);
        return xdist + ydist;
    }
}


