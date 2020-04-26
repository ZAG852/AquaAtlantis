using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    // Create static adj matrix 

    int dfp;
    public string IDX;
    public int area; // box around center
    // X and Y origin of node
    public double X;
    public double Y;
    public bool containsPlayer = false;
    public List<PathNode> neighbors;
    private int vIdx; // Index of vertex column of which this node represents

    //Each node tracks it's own scores
    double fCost;
    double gScore;
    double hScore;


    public double getX()
    {
        return X;
    }

    public double getY()
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
        containsPlayer = hasPlayerWithin();
        //hScore[vIdx] = distanceCheck(player);
        
    }

    bool hasPlayerWithin()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        double pX = player.transform.position.x;
        double pY = player.transform.position.y;
        // if player x is greater than node center - 5 AND less than node center -5
        if ( (pX < X + Mathf.Sqrt(area) ) && (pX > X - (double) Mathf.Sqrt(area) ) ) {
            //if inside here, then player is within X bounds of this node!
            if ( (pY < Y + Mathf.Sqrt(area)/2 ) && pY > (Y - Mathf.Sqrt(area)/2 ) )  {
                //if inside here, then player is also within Y bounds of this node! player is in this node!
                return true;
            }
        }
            return false;
        // True if the player is within the bounds of this node.
    }
}
