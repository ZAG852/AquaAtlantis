using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathAI : MonoBehaviour
{
    
    List<PathNode> openSet = new List<PathNode> ();
    List<PathNode> closedSet = new List<PathNode>();
    public static int [] prev; // Set by PathGrid .start() all to -1. Stores path node ID values

    // Start is called before the first frame update
    void Start()
    {// initialize

    }

    List<PathNode> Astar(PathNode source, PathNode target)
    {
        PathNode s = source;
        s.gScore = 0;
        openSet.Add(s);


        // must be in open set before finding hscore
        //We know the cost to get to source from source is 0
        //So 0 becomes the gScore of source
        //Since we change gScore, we must also change fScore
        s.gScore = 0;
        s.hScore = updatehScore(s);
        s.fScore = s.fScore + s.gScore;
    
        while (openSet.Count != 0)
        {
            // Find lowest fScore node
            // Set current equal to that
            PathNode current = findLowestFScore(openSet);
            openSet.Remove(current);
            closedSet.Add(current);

            // Upon finding the target...
            if (current.ID == target.ID)
            {
                List<PathNode> path = new List<PathNode>();
                while (prev[current.ID] != -1)
                {
                    path.Add(current);
                    current = FindNodeID(prev[current.ID]);
                }
                path.Reverse();
                return path;
            }

            // If target not found continue...

           foreach (PathNode neighbor in current.FindNeighbors()){
                int edgeWeight = 1;
                double tentativeGScore = current.gScore + edgeWeight;
                if (tentativeGScore < neighbor.gScore)
                {
                    neighbor.gScore = tentativeGScore;
                    neighbor.fScore = neighbor.gScore + neighbor.hScore;
                    prev[neighbor.ID] = current.ID; // store the ID of previous node

                    //If we have found a better path
                    if (closedSet.Contains(neighbor))
                    {
                        closedSet.Remove(neighbor);
                        openSet.Add(neighbor);
                    } else if (openSet.Contains(neighbor) == false)
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    PathNode findLowestFScore(List<PathNode> openSet)
    {
        double minScore = openSet[0].fScore;
        
        PathNode minNode = openSet[0];

        for (int i = 0; i < openSet.Count; i++)
        {
            PathNode n = openSet[i];
            if (n.fScore < minScore)
            {
                minNode = n;
                minScore = minNode.fScore;
            }
        }
        return minNode;
    }

    PathNode FindNodeID(int ID)
    {
        // Query the master graph for node with matching ID.
        PathNode n = new PathNode(); // empty pathNode
        for (int x = 0; x < PathGrid.Xlen; x++)
        {

            for (int y = 0; y < PathGrid.Xlen; y++)
            {
                if (PathGrid.mstrGrid[x, y].ID == ID)
                {
                    return PathGrid.mstrGrid[x, y];
                }
            }
        }
            return n;
    }

    public double updatehScore(PathNode n)
    {
        // distance function to find the distance from the player location to this node
        Vector2Int targetNode = PathGrid.Translate(PathGrid.player.transform.position);
        // the grid index of this node is stored in nodeposition. simply count the h + V distance
        int xdist = Mathf.Abs(n.nodePosition.x - targetNode.x);
        int ydist = Mathf.Abs(n.nodePosition.y - targetNode.y);
        return (double) Mathf.Sqrt((float)Math.Pow(xdist, 2) +(float) Math.Pow(ydist, 2));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Update heuristic for only the nodes in open set to save resources
        foreach (PathNode node in openSet)
        {
            // The heuristics array should be updated for nodes on an as-needed basis. Update fScore as well because it is dependant
            node.hScore = updatehScore(node);
            node.fScore = node.hScore + node.gScore;
        }
    }
}
