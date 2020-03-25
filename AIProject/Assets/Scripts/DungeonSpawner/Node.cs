﻿using System.Collections;
using System.Collections.Generic;

namespace Nodies
{
    public class Node
    {
        Node[] parents = new Node[4];
        public int id = 0;
        public float positionX = 0f, positionY = 0f;
        public int nodalPositionX = 0, nodalPositionY = 0;
        int parentPos = 0;
        bool isSource = false, isTarget = false;
        public float playerX = 25, playerY = 25;
        bool isLand = false;
        static List<Node> nodalList = new List<Node>();
        public void AddParent(Node p)
        {
            if (parentPos > 0)
            {
                for (int i = 0; i < parentPos; i++)
                {
                    if (parents[i].id == p.id)
                        return;
                }
            }
            parents[parentPos++] = p;
        }
        public void AddNodalPosition(int x, int y, float tileWidth)
        {
            nodalPositionX = x;
            playerX = positionX = x * tileWidth;
            nodalPositionY = y;
            playerY = positionY = y * tileWidth;
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
}
