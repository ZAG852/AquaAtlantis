using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathAI : MonoBehaviour
{
    
    Heap<PathNode> openSet;
    List<PathNode> closedSet = new List<PathNode>();
    public static int [] prev; // Set by PathGrid .start() all to -1. Stores path node ID values


    private void Start()
    {
        openSet = new Heap<PathNode>(MapMaker.mapThingy.worldSizex * MapMaker.mapThingy.worldSizey);
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
        s.hScore = PathGrid.updatehScore(s);
        s.fScore = s.hScore + s.gScore;
    
        while (openSet.Count != 0)
        {
            // Find lowest fScore node
            // Set current equal to that
            PathNode current = openSet.RemoveFirst();

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



}
