using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    int mapPos = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(MapMaker.mapThingy != null)
        {
            transform.position = new Vector2(MapMaker.mapThingy.getList()[mapPos].positionX,MapMaker.mapThingy.getList()[mapPos].positionY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
