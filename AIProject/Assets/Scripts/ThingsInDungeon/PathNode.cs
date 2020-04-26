using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    // Create static adj matrix 

    
    private int weight;
    private List<PathNode> neighbors;
    private int vIdx; // Index of vertex column of which this node represents
    

    // Start is called before the first frame update. new path nodes should specify vertex count
    void Start()
    {
        //initialize
        init();
       
    }

    void init()
    {
        mstrGrid = new PathNode[(int)MapMaker.mapThingy.getWorldSize(), (int)MapMaker.mapThingy.getWorldSize()];
        
    }
    // Update is called once per frame
    void Update()
    {
        // Put heuristic calc into here!
        hScore[vIdx] = distanceCheck(player);
        
    }

    void distanceCheck()
    {

        // True if the player is colliding with this node.
    }
}
