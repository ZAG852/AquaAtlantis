using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    // Create static adj matrix 

    
    public string IDX;
    public int Area;
    public int X;
    public int Y;
    public List<PathNode> neighbors;
    private int vIdx; // Index of vertex column of which this node represents
    
    public int getX()
    {
        return X;
    }

    public int getY()
    {
        return Y;
    }
    // Start is called before the first frame update. new path nodes should specify vertex count
    void Start()
    {
        //initialize

       
    }

    // Update is called once per frame
    void Update()
    {
       
        //hScore[vIdx] = distanceCheck(player);
        
    }

    void distanceCheck()
    {

        // True if the player is colliding with this node.
    }
}
