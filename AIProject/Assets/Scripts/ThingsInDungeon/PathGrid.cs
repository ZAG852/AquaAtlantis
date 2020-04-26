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
        
        float ws = MapMaker.mapThingy.getWorldSize(); // world size is length of one side
        print(ws);
        int gridRes = 50; // SIDE LENGTH IS SQRT OF THIS NUM | This is grid resolution. Higher = more node objects
        int rowCount = (int) Mathf.Sqrt(gridRes);
        double B = ((double)( ws*ws ) / gridRes);
        mstrGrid = new PathNode[(int) B, (int) B];


        for (int x = 0; x < rowCount ; x++)
        {

            for (int y = 0; y < rowCount ; y++)
            {
                // creates reference to the object stored in node
                // call master grid to return a node and set the coordinate properties.
                // cX & cY are both based on nodeArea

                mstrGrid[x, y] = Instantiate(node); 
                //mstrGrid[x, y] = this.gameObject.AddComponent<PathNode>(); // returns game object                
                mstrGrid[x, y].area = gridRes;               
                mstrGrid[x, y].X = cX;
                mstrGrid[x, y].Y = cY;
                mstrGrid[x, y].IDX = "[" + x + "," + y + "]";
                cY += rowCount;
            }
            cX += rowCount;
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
        