using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    private int levelWidth;
    private int levelHeight;
    private int[,] grid2d; // 2D array
    public WorldGrid(int width, int height)
    {
        levelWidth = width;
        levelHeight = height;


        grid2d = new int[width, height]; // rows.len = width cols.len = height

        

        for (int i = 0; i < grid2d.GetLength(0); i ++)
        {
            for (int j = 0; j < grid2d.GetLength(1); j++)
            {
                Debug.Log(i + " " + j);
            }
        }
    }



}
