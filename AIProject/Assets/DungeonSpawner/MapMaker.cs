using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{

    public Node Source = new Node();
    public Node Target = new Node();
    [SerializeField]
    GameObject player;
    public int worldSizex = 50;
    public int worldSizey = 50;
    Node[,] grid;
    int[] parentArray = new int[50];
    Stack<Node> path = new Stack<Node>();
    int newSquaresTravel = 30;
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

    // Start is called before the first frame update
    void Awake()
    {
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
        grid[worldSizex / 2, worldSizey / 2].AddNodalPosition(worldSizex, worldSizey);
        path.Push(Source);
        startWalk();
    }
    
    void startWalk()
    {
        Vector2Int currentPos = new Vector2Int(worldSizex / 2, worldSizey / 2);
        while (currentPos.x < 50 && currentPos.y < 50 && currentPos.x > 0 && currentPos.y > 0 && newSquaresTravel > 0)
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
            grid[currentPos.x, currentPos.y].AddNodalPosition(currentPos.x, currentPos.y);
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

    void CreateMap(Stack<Node> path)
    {
        Target = path.Peek();
        while (path.Count > 0)
        {
            
            Node tmp = path.Pop();
            if (path.Count == 0)
            {
                
            }
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
