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
    public List<PathNode> current_neighbors;
    public float nodeXLength;
    public int current_neighbors_len;
    public float Xidx;
    public float Yidx;
    
    static float ws;

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
    public List<PathNode> FindNeighbors(Vector2Int nodelabel)
    {
        // Stores a vector2 for each neighbor. Vector 2 corresponds to row/col in mstrGrid[,]. Reverse Astar solve from player to given enemy;
        List<PathNode> neighbors = new List<PathNode>(4);
        
        try
        {
            if (mstrGrid[nodelabel.x + 1, nodelabel.y].walkable)
            {
                neighbors.Add(mstrGrid[nodelabel.x + 1, nodelabel.y]);
                neighbor_E = mstrGrid[nodelabel.x + 1, nodelabel.y].nodePosition;
            } else
            {
                Debug.Log("E node is not walkable!");
            }
        } catch(Exception e)
        {
            Debug.Log("No more Eastern nodes!");
            neighbor_E = new Vector2Int(-1, -1);
        }
        try
        {
            if (mstrGrid[nodelabel.x - 1, nodelabel.y].walkable)
            {
                neighbors.Add(mstrGrid[nodelabel.x - 1, nodelabel.y]);
                neighbor_W = mstrGrid[nodelabel.x - 1, nodelabel.y].nodePosition;
            } else
            {
                Debug.Log("W node is not walkable!");
            }
        }
        catch (Exception e)
        {
            Debug.Log("No more Western nodes!");
            neighbor_W = new Vector2Int(-1, -1);
        }
        try
        {
            if (mstrGrid[nodelabel.x, nodelabel.y + 1].walkable) {
                neighbors.Add(mstrGrid[nodelabel.x, nodelabel.y + 1]);
                neighbor_N = mstrGrid[nodelabel.x, nodelabel.y + 1].nodePosition;
            } else
            {
                Debug.Log("N is not walkable!");
            }
        }
        catch (Exception e)
        {
            Debug.Log("No more Northern nodes!");
        }
        try
        {
            if (mstrGrid[nodelabel.x, nodelabel.y - 1].walkable)
            {
                neighbors.Add(mstrGrid[nodelabel.x, nodelabel.y - 1]);
                neighbor_S = mstrGrid[nodelabel.x, nodelabel.y - 1].nodePosition;
            } else
            {
                Debug.Log("S node is not walkable!");
            }
        }
        catch (Exception e)
        {
            Debug.Log("No more Southern nodes!");
            neighbor_S = new Vector2Int(-1, -1);
        }
        neighbors.TrimExcess();
        return neighbors;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int playerlocation = Translate(player.transform.position);
        Xidx = playerlocation.x;
        Yidx = playerlocation.y;
        current_neighbors = FindNeighbors(playerlocation);
        current_neighbors_len = current_neighbors.Count;
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
        