﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nodies;
using UnityEditor;
using System.IO;
public class MapMaker : MonoBehaviour
{

    public Node Source = new Node();
    public Node Target = new Node();
    [SerializeField]
    GameObject player;
    [SerializeField]
    float tileSize = 16.5f;
    public int worldSizex = 30;
    public int worldSizey = 30;
    Node[,] grid;
    int[] parentArray = new int[50];
    Stack<Node> path = new Stack<Node>();
    int newSquaresTravel = 30;
    [SerializeField]
    bool getPieces = false;
    //Zero rooms will have clear centers
    [SerializeField]
    List<GameObject> upRooms;
    [SerializeField]
    List<GameObject> downRooms;
    [SerializeField]
    List<GameObject> leftRooms;
    [SerializeField]
    List<GameObject> rightRooms;
    [SerializeField]
    List<GameObject> leftRightRooms;
    [SerializeField]
    List<GameObject> leftRightDownRooms;
    [SerializeField]
    List<GameObject> leftRightUpRooms;
    [SerializeField]
    List<GameObject> upDownRooms;
    [SerializeField]
    List<GameObject> upLeftRooms;
    [SerializeField]
    List<GameObject> upRightRooms;
    [SerializeField]
    List<GameObject> downLeftRooms;
    [SerializeField]
    List<GameObject> downRightRooms;
    [SerializeField]
    List<GameObject> upLeftDown;
    [SerializeField]
    List<GameObject> upRightDown;
    [SerializeField]
    List<GameObject> intersection;
    public static MapMaker mapThingy;
    List<Node> nodalList = new List<Node>();
    List<Object> possiblyRoom = new List<Object>();
    void ClearAllRooms()
    {
    upRooms.Clear();
    downRooms.Clear();
    leftRooms.Clear();
    rightRooms.Clear();
    leftRightRooms.Clear();
    leftRightDownRooms.Clear();
    leftRightUpRooms.Clear();
    upDownRooms.Clear();
    upLeftRooms.Clear();
    upRightRooms.Clear();
    downLeftRooms.Clear();
    downRightRooms.Clear();
    upLeftDown.Clear();
    upRightDown.Clear();
    intersection.Clear();
}
    void FillOutRooms()
    {
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + "Rooms");
        foreach (string fileName in fileEntries)
        {
            int assetPathIndex = fileName.IndexOf("Assets");
            string localPath = fileName.Substring(assetPathIndex);
            Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(Object));
            if (t != null)
                possiblyRoom.Add(t);
        }
        print(possiblyRoom.Count);
        for (int k = 0; k < possiblyRoom.Count; k++)
            print(possiblyRoom[k]);

        ClearAllRooms();
        foreach (Object piece in possiblyRoom)
        {
            if (piece.GetType() == typeof(GameObject))
            {
                GameObject t = (GameObject)Instantiate(piece);
                if (t.CompareTag("Up"))
                {
                    upRooms.Add(t);
                }
                if (t.CompareTag("Down"))
                {
                    downRooms.Add(t);
                }
                if (t.CompareTag("DownUp"))
                {
                    upDownRooms.Add(t);
                }
                if (t.CompareTag("Left"))
                {
                    leftRooms.Add(t);
                }
                if (t.CompareTag("LeftUp"))
                {
                    upLeftRooms.Add(t);
                }
                if (t.CompareTag("LeftDown"))
                {
                    downLeftRooms.Add(t);
                }
                if (t.CompareTag("LeftDownUp"))
                {
                    upLeftDown.Add(t);
                }
                if (t.CompareTag("Right"))
                {
                    rightRooms.Add(t);
                }
                if (t.CompareTag("RightUp"))
                {
                    upRightRooms.Add(t);
                }
                if (t.CompareTag("RightDown"))
                {
                    downRightRooms.Add(t);
                }
                if (t.CompareTag("RightDownUp"))
                {
                    upRightDown.Add(t);
                }
                if (t.CompareTag("RightLeft"))
                {
                    leftRightRooms.Add(t);
                }
                if (t.CompareTag("RightLeftUp"))
                {
                    leftRightUpRooms.Add(t);
                }
                if (t.CompareTag("RightLeftDown"))
                {
                    leftRightDownRooms.Add(t);
                }
                if (t.CompareTag("RightLeftDownUp"))
                {
                    intersection.Add(t);
                }
            }
        }
        //upRooms = GetComponents<GameObject>().tag.CompareTo("up");

    }
    // Start is called before the first frame update
    void Awake()
    {
        if(getPieces)
        {
            FillOutRooms();
        }
        mapThingy = this;
        grid = new Node[worldSizex, worldSizey];
        int id = 1;
        for (int i = 0; i < worldSizex; i++)
        {
            for (int j = 0; j < worldSizey; j++)
            {
                grid[i, j] = new Node();
                grid[i, j].id = id;
                id++;
            }
        }
        Source.id = 0;
        grid[worldSizex / 2, worldSizey / 2] = Source;
        grid[worldSizex / 2, worldSizey / 2].AddNodalPosition(worldSizex, worldSizey, tileSize);
        path.Push(Source);
        startWalk();
    }
    public float getWorldSize()
    {
        return tileSize * worldSizey;
    }
    void startWalk()
    {
        Vector2Int currentPos = new Vector2Int(worldSizex / 2, worldSizey / 2);
        while (currentPos.x < worldSizex && currentPos.y < worldSizey && currentPos.x > 0 && currentPos.y > 0 && newSquaresTravel > 0)
        {
            int dir = Random.Range(0, 3);
            Vector2 dirToGo = new Vector2(0, 0);
            switch (dir)
            {
                case 0:
                    dirToGo = Vector2Int.right;
                    break;
                case 1:
                    dirToGo = Vector2Int.left;
                    break;
                case 2:
                    dirToGo = Vector2Int.down;
                    break;
                case 3:
                    dirToGo = Vector2Int.up;
                    break;

                default:
                    break;
            }
            if (currentPos.x == 50 && dirToGo.x == 1)
            {
                dirToGo = Vector2Int.left;

            }
            if (currentPos.y == 50 && dirToGo.y == 1)
            {
                dirToGo = Vector2Int.down;
            }
            if (currentPos.x == 0 && dirToGo.x == -1)
            {
                dirToGo = Vector2Int.down;

            }
            if (currentPos.y == 0 && dirToGo.y == -1)
            {
                dirToGo = Vector2Int.up;
            }
            currentPos.x += (int)dirToGo.x;
            currentPos.y += (int)dirToGo.y;
            grid[currentPos.x, currentPos.y].AddNodalPosition(currentPos.x, currentPos.y, tileSize);
            path.Peek().AddParent(grid[currentPos.x, currentPos.y]);
            grid[currentPos.x, currentPos.y].AddParent(path.Peek());
            path.Push(grid[currentPos.x, currentPos.y]);
            newSquaresTravel--;
        }
        /*
        for(int i = 0; i < path.Count; i++)
        {
            Node tmp = path.Pop();
            print("( " + tmp.nodalPositionX +","+ tmp.nodalPositionY + " )");
        }*/
        CreateMap(path);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (worldSizey/2 *tileSize), transform.position.y + (worldSizey / 2 * tileSize)), new Vector2(worldSizex* tileSize,worldSizey * tileSize));
        /*
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
        */
    }
    public List<Node> getNodeList()
    {
        return nodalList;
    }
    void CreateMap(Stack<Node> path)
    {
        Target = path.Peek();
        while (path.Count > 0)
        {
            
            Node tmp = path.Pop();
            nodalList.Add(tmp);
            Node[] parentArray = new Node[tmp.RetrieveParent().Length];
            bool up, down, left, right;
            float dirX = 0;
            float dirY = 0;
            up = down = left = right = false;
            parentArray = tmp.RetrieveParent();

            for (int i = 0; i < tmp.GetParentPos(); i++)
            {
                dirX = parentArray[i].nodalPositionX - tmp.nodalPositionX;
                dirY = parentArray[i].nodalPositionY - tmp.nodalPositionY;
                if (dirX > 0 && dirY == 0)
                {
                    right = true;
                }
                if (dirX < 0 && dirY == 0)
                {
                    left = true;
                }
                if (dirX == 0 && dirY > 0)
                {
                    up = true;
                }
                if (dirX == 0 && dirY < 0)
                {
                    down = true;
                }
            }
            if (right && left && up && down)
            {
                //intersection
                Instantiate(intersection[Random.Range(0, intersection.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (right && left && up && !down)
            {
                //right left up
                Instantiate(leftRightUpRooms[Random.Range(0, leftRightUpRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);


            }
            else if (!right && left && !up && down)
            {
                //right left up
                Instantiate(downLeftRooms[Random.Range(0, leftRightUpRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (right && left && !up && down)
            {
                //right left down
                Instantiate(leftRightDownRooms[Random.Range(0, leftRightDownRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (right && !left && up && down)
            {
                //right up down
                Instantiate(upRightDown[Random.Range(0, upRightDown.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (!right && left && up && down)
            {
                //left up down
                Instantiate(upLeftDown[Random.Range(0, upLeftDown.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (right && left && !up && !down)
            {
                //right left
                Instantiate(leftRightRooms[Random.Range(0, leftRightRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (right && !left && !up && down)
            {
                //right down
                Instantiate(downRightRooms[Random.Range(0, downRightRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (right && !left && up && !down)
            {
                //right up
                Instantiate(upRightRooms[Random.Range(0, upRightRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (!right && left && up && !down)
            {
                //up left
                Instantiate(upLeftRooms[Random.Range(0, upLeftRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (!right && !left && up && down)
            {
                //up down
                Instantiate(upDownRooms[Random.Range(0, upDownRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (right && !left && !up && !down)
            {
                //right
                Instantiate(rightRooms[Random.Range(0, rightRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (!right && !left && !up && down)
            {
                //down
                Instantiate(downRooms[Random.Range(0, downRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (!right && left && !up && !down)
            {
                //left
                Instantiate(leftRooms[Random.Range(0, leftRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
            else if (!right && !left && up && !down)
            {
                //up
                Instantiate(upRooms[Random.Range(0, upRooms.Count)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);

            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(MapMaker))]
public class CustomInspector : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Zero rooms should have clear");
        EditorGUILayout.LabelField("centers for Exits and Entrances");
        EditorGUILayout.LabelField("For the Dungeon Layout.");
    }
}
#endif