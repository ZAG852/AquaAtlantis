using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{

    // Center of room X coord, Y coord
    
    public PathNode node;
    public GameObject player;
    public List<PathNode> current_neighbors;
    public Vector2Int N;
    public Vector2Int S;
    public Vector2Int E;
    public Vector2Int W;
    public float Xidx;
    public float Yidx;
    static int gridX = 200; // being the rightmost X coordinate
    public float ws;

    PathNode[,] mstrGrid;
    
    //World bounds, node side length, 
    // Start is called before the first frame update
    void Start()
    {
        // each node takes up 5x5 space so div the world size by 10 to create appropriate # of path node entries in the array
        
        ws = MapMaker.mapThingy.getWorldSize(); // world size is length of one side
        float xLength = (ws / gridX);
        mstrGrid = new PathNode[gridX, gridX];


        for (int x = 0; x < gridX ; x++)
        {

            for (int y = 0; y < gridX ; y++)
            {
                // creates reference to the object stored in node
                // call master grid to return a node and set the coordinate properties.
                // cX & cY are both based on nodeArea

                mstrGrid[x, y] = new PathNode(new Vector2Int(x,y)); 
   
            }
        }
    }
    public List<PathNode> findNeighbors(Vector2Int nodelabel)
    {
        // Stores a vector2 for each neighbor. Vector 2 corresponds to row/col in mstrGrid[,]. Reverse Astar solve from player to given enemy;
        List<PathNode> neighbors = new List<PathNode>(4);
        
        try
        {
            neighbors.Add(mstrGrid[nodelabel.x + 1, nodelabel.y]);
            E = mstrGrid[nodelabel.x + 1, nodelabel.y].nodePosition;
        } catch( IndexOutOfRangeException e)
        {
            Debug.Log("No more Eastern nodes!");
            E = new Vector2Int(-1, -1);
        }
        try
        {
            neighbors.Add(mstrGrid[nodelabel.x - 1, nodelabel.y]);
            W = mstrGrid[nodelabel.x - 1, nodelabel.y].nodePosition;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("No more Western nodes!");
            W = new Vector2Int(-1, -1);
        }
        try
        {
            neighbors.Add(mstrGrid[nodelabel.x, nodelabel.y + 1]);
            N = mstrGrid[nodelabel.x - 1, nodelabel.y].nodePosition;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("No more Northern nodes!");
        }
        try
        {
            neighbors.Add(mstrGrid[nodelabel.x, nodelabel.y-1]);
            S = mstrGrid[nodelabel.x, nodelabel.y-1].nodePosition;
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("No more Southern nodes!");
            S = new Vector2Int(-1, -1);
        }
        neighbors.TrimExcess();
        return neighbors;
    }
    
    Vector2Int Translate(Vector3 position)
    {
        // Take in player position in the game space, translate to path grid index in form of a vector2
        // Numbers between 0 and 1 where n * world size = coordinate
        float nXposition = position.x / ws;
        float nYposition = position.y / ws;
        // Node diameter is accounted for by rounding
        int xg = Mathf.RoundToInt(nXposition * gridX);
        int yg = Mathf.RoundToInt(nYposition * gridX);
        return new Vector2Int(xg, yg);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2Int playerlocation = Translate(player.transform.position);
        Xidx = playerlocation.x;
        Yidx = playerlocation.y;
        current_neighbors = findNeighbors(playerlocation);
        Debug.Log("CN=" + current_neighbors);
    }
}
        