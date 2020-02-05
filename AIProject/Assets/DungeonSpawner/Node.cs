using System.Collections;
using System.Collections.Generic;

public class Node
{
    Node[] parents = new Node[4];
    public int id = 0;
    public float positionX = 0f, positionY = 0f;
    public int nodalPositionX = 0, nodalPositionY = 0;
    int parentPos = 0;
    bool isSource = false, isTarget = false;
    public float playerX = 25, playerY = 25;
    public void AddParent(Node p)
    {
        if(parentPos >0)
        {
            for(int i = 0; i < parentPos; i++)
            {
                if (parents[i].id == p.id)
                    return;
            }
        }
        parents[parentPos++] = p;
    }
    public void AddNodalPosition(int x, int y)
    {
        nodalPositionX = x;
        playerX = positionX = x * 16.5f;
        nodalPositionY = y;
        playerY = positionY = y * 16.5f;
    }
    public int GetParentPos()
    {
        return parentPos;
    }
    public Node[] RetrieveParent()
    {
        return parents;
    }
}
