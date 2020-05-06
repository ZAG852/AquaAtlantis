using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    // !World position to graph position translate function
    // Create static adj matrix 
   public Vector2Int nodePosition;
    public int x;
    public int y;
    // create constructor with right click
    public PathNode(Vector2Int nodePosition)
    {
        this.nodePosition = nodePosition;
        x = nodePosition.x;
        y = nodePosition.y;
    }

}


