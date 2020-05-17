using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

	public bool displayGridGizmos;
	public LayerMask obstacle;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	PathNode[,] grid;
    [SerializeField]
    public static bool loaded = false;
	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start()
	{
        //gameObject.transform.position = new Vector2(transform.position.x + (50 / 2 * 21), transform.position.y + (50 / 2 * 21));

        nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid();
        loaded = true;
	}

	public int MaxSize
	{
		get
		{
			return gridSizeX * gridSizeY;
		}
	}

	void CreateGrid()
	{
		grid = new PathNode[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				//Tried checking the OverlapCircleAll but it didn't really do anything
				bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacle));
				grid[x, y] = new PathNode(walkable, worldPoint, x, y);
			}
		}
	}

	public List<PathNode> GetNeighbors(PathNode node)
	{
		List<PathNode> neighbours = new List<PathNode>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}


	public PathNode NodeFromWorldPoint(Vector3 worldPosition)
	{//0,0 to the positive directions
        int x;
        int y;
        if((worldPosition.x - (int) worldPosition.x) > 0.5)
        {
            x = (int)(worldPosition.x + 0.5);
        }
        else
        {
            x = (int)(worldPosition.x);
        }
        if ((worldPosition.y - (int)worldPosition.y) > 0.5)
        {
            y = (int)(worldPosition.y + 0.5);
        }
        else
        {
            y = (int)(worldPosition.y);
        }
        /*
         *When A* is around 0,0 as the starting point
    float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
    float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
    percentX = Mathf.Clamp01(percentX);
    percentY = Mathf.Clamp01(percentY);

    int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
    int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
    */
        return grid[x, y];
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));
		if (grid != null && displayGridGizmos)
		{
			foreach (PathNode n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter - .1f));
			}
		}
	}
}
