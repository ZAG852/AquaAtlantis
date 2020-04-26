using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    float ws;
    // Center of room X coord, Y coord
    PathNode[ , ] mstrGrid;

    //World bounds, node side length, 
    // Start is called before the first frame update
    void Start()
    {
        // each node takes up 5x5 space so div the world size by 10 to create appropriate # of path node entries in the array
        ws = MapMaker.mapThingy.getWorldSize();
        mstrGrid = new PathNode[ (int) ws / 5 , (int) ws / 5];
        int cX = 0;
        int cY = 0;
   
        for ( int x = 0; x < ws; x++)
        {
       
            for (int y = 0; y < ws; y++)
            {
                mstrGrid[x, y] = new PathNode(cX,cY);
                cY += 10;
            }
            cX += 10;
        }
        print(mstrGrid.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}