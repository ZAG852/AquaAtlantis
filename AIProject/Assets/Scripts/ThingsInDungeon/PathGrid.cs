using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    float ws;
    // Center of room X coord, Y coord
    List<PathNode> mstrGrid;

    //World bounds, node side length, 
    // Start is called before the first frame update
    void Start()
    {
        ws = MapMaker.mapThingy.getWorldSize();
        mstrGrid = new List<PathNode>[ws, ws];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}