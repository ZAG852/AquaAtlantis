using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{

    // Center of room X coord, Y coord
    public GameObject player;
    public PathNode node;
    public double pX = 0;
    public double pY = 0;
    private PathNode[,] mstrGrid;
    int cX = 0;
    int cY = 0;
    //World bounds, node side length, 
    // Start is called before the first frame update
    void Start()
    {
        // each node takes up 5x5 space so div the world size by 10 to create appropriate # of path node entries in the array
        player = GameObject.FindGameObjectWithTag("Player");
        
        float ws = MapMaker.mapThingy.getWorldSize();
        print(ws);
        int nodeArea = 10; // squared
        int B = (int)(ws / nodeArea);
        mstrGrid = new PathNode[B, B];


        for (int x = 0; x < B; x++)
        {

            for (int y = 0; y < B; y++)
            {
                mstrGrid[x, y] = Instantiate(node); // creates reference to the object stored in node
                                                    // call master grid to return a node and set the coordinate properties.
                mstrGrid[x, y].Area = nodeArea;               // cX & cY are both based on nodeArea
                mstrGrid[x, y].X = cX;
                mstrGrid[x, y].Y = cY;
                mstrGrid[x, y].IDX = "[" + x + "," + y + "]";
                cY += nodeArea;
            }
            cX += nodeArea;
            cY = 0;// account for node area and bring row back
        }
        print(mstrGrid.Length);
    }

    // Update is called once per frame
    void Update()
    {
        pX = player.transform.position.x;
        pY = player.transform.position.y;
    }
}
        