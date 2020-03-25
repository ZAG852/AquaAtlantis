using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField]
    int posGO = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(MapMaker.mapThingy.getNodeList()[posGO].positionX, MapMaker.mapThingy.getNodeList()[posGO].positionY);
    }

}
