using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathGrid : MonoBehaviour
{

    // Center of room X coord, Y coord
    public Vector2Int neighbor_N;
    public Vector2Int neighbor_E;
    public Vector2Int neighbor_S;
    public Vector2Int neighbor_W;

    
    public static int Xlen = 249; // being the rightmost X coordinate
    public static int IDcount = 0;
    public static GameObject player;
    public PathNode [] current_neighbors;
    public float nodeXLength;

    static float ws;
    public int current_neighbors_len;
    public int Xidx;
    public int Yidx;

    public PathNode node;
    public static PathNode[,] mstrGrid;

    
    //World bounds, node side length, 
    // Start is called before the first frame update
    void Start()
    {
        // each node takes up 5x5 space so div the world size by 10 to create appropriate # of path node entries in the array
        player = GameObject.Find("Player");
        ws = MapMaker.mapThingy.getWorldSize(); // world size is length of one side
        nodeXLength = (ws / Xlen); 
        mstrGrid = new PathNode[Xlen, Xlen];
        

        for (int x = 0; x < Xlen ; x++)
        {

            for (int y = 0; y < Xlen ; y++)
            {
 

                mstrGrid[x, y] = new PathNode(new Vector2Int(x,y), nodeXLength, IDcount );
                IDcount++;
            }
        }

                
        
        // on start get unwalkable. This gets all unwalkable game objects and translates their raw coords to a node location.
        GameObject[] roadblocks = GameObject.FindGameObjectsWithTag("unwalkable");
        Vector3 position = transform.position;
        for (int i = 0; i < roadblocks.Length; i++)
        {
            mstrGrid[Translate(roadblocks[i].transform.position).x, Translate(roadblocks[i].transform.position).y].walkable = false;
        }
        // Sets up previous array at time of grid creation as to get appropriate sizing
        PathAI.prev = new int [IDcount];
        for (int i = 0; i < IDcount; i++)
        {
            PathAI.prev[i] = -1;
        }
    }

    void OnDrawGizmos()
    {
        print(mstrGrid);
        if (mstrGrid != null)
        {
            
            foreach (PathNode n in mstrGrid)
            {
                if (n.walkable)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawCube((Vector2)n.nodePosition, Vector3.one * (n.XLength));
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube((Vector2)n.nodePosition, Vector3.one * (n.XLength));
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int playerlocation = Translate(player.transform.position);
        Xidx = playerlocation.x;
        Yidx = playerlocation.y;
        current_neighbors = mstrGrid[Xidx, Yidx].FindNeighbors();
        current_neighbors_len = current_neighbors.Length ;
       // Debug.Log("Node 5,5 hscore = " + mstrGrid[5, 5].DistanceFrom(player));
    }

    public static Vector2Int Translate(Vector3 position)
    {
        // Take in a vector3 position in the game space, translate to path grid index in form of a vector2
        // Numbers between 0 and 1 where n * world size = coordinate
        float nXposition = position.x / ws;
        float nYposition = position.y / ws;
        // Node diameter is accounted for by rounding
        int xg = Mathf.RoundToInt(nXposition * Xlen);
        int yg = Mathf.RoundToInt(nYposition * Xlen);
        return new Vector2Int(xg, yg);
    }
}
        