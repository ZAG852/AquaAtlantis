using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAI : MonoBehaviour
{
    
    List<PathNode> openSet = new List<PathNode> ();
    List<PathNode> closedSet = new List<PathNode>();
    int[] prev;
    double[] gScore;
    double[] fScore;
    double[] hScore;



    // Start is called before the first frame update
    void Start()
    {// initialize
        prev = new int[PathGrid.Xlen];
        gScore = new double[PathGrid.Xlen];
        fScore = new double[PathGrid.Xlen];

        for (int i = 0; i < PathGrid.Xlen; i++)
        {
            gScore[i] = double.MaxValue;
            fScore[i] = double.MaxValue;
            prev[i] = -1;
        }
    }

    void astar(PathNode source)
    {
        //We know the cost to get to source from source is 0
        //So 0 becomes the gScore of source
        //Since we change gScore, we must also change fScore
        openSet.Add(source); // must be in open set before finding hscore
        gScore[source.nodePosition.x] = 0;
        fScore[source.nodePosition.x] = gScore[source.nodePosition.x] + hScore[source.nodePosition.x];
        while (openSet.Count != 0)
        {
            // Find lowest fScore node
            double minScore = double.MaxValue;
            PathNode minNode = new PathNode();
            for(int i = 0; i < openSet.Count; i++)
            {
                PathNode n = openSet[i];
                if (fScore[n.nodePosition.x] < minScore)
                {
                    minNode = n;
                }
            }
            // Set current equal to that
            PathNode current = openSet.;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Update heuristic for only the nodes in open set to save resources
        foreach (PathNode node in openSet)
        {
            // The heuristics array should be updated for nodes on an as-needed basis.
            hScore[node.nodePosition.x] = (double) node.DistanceFrom(PathGrid.player);
        }
    }
}
