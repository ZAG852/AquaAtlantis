using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAI : MonoBehaviour
{
    static PathNode[,] mstrGrid;
    static float cX;
    static float cY;
    Queue<PathNode> openQ = new Queue<PathNode> () ;
    List<PathNode> closedSet;
    
    

    // Start is called before the first frame update
    void Start()
    {// initialize
        cX = MapMaker.mapThingy.getNodeList()[i].positionX;
        cY = MapMaker.mapThingy.getNodeList()[i].positionY;
    }

    voide initGrid()
    {
        mstrGrid = new PathNode[10,10];
       
    }
    // Update is called once per frame
    void Update()
    {
            // Update value of previous array


    }
}
