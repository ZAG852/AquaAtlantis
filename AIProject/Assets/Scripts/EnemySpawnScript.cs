﻿using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    #region State Variables
    public GameObject batPrefab;
    public GameObject slimePrefab;
    public GameObject slugPrefab;
    public GameObject frogPrefab;
    public GameObject snekPrefab;
    public GameObject dragonPrefab;
    public GameObject golemPrefab;

    public GameObject enemyInstance;

    // The total type of unique nonboss enemies the game has to choose from
    public int totalNonBoss;

    // The total type of unique boss enemies the game has to choose from
    public int totalBoss;

    // A static random number generator to combat the pseudo-random nature of Random.Next().
    public static System.Random random = new System.Random();
    #endregion

    void Start()
    {
        for (int i = 0; i < MapMaker.mapThingy.getNodeList().Count; i++)
        {
            int centerX = (int)(MapMaker.mapThingy.getNodeList()[i].positionX);
            int centerY = (int)(MapMaker.mapThingy.getNodeList()[i].positionY);
            int weightMax;
            if (i == 14)
                weightMax = 20;
            else
                weightMax = 15;
            FillRoom(weightMax, centerX, centerY);
        }
    }

    /// <summary>
    /// Fills the room to capacity with randomly chosen enemies.
    /// </summary>
    /// <param name="weightMax">How many HP worth of enemies will spawn in this room</param>
    /// <param name="roomCenterPoint">The center coordinate of the node (to randomize spawnpoints)</param>
    public void FillRoom(int weightMax, int roomCenterX, int roomCenterY)
    {
        //Set key variables.
        int currentWeight = 0, enemyChoice = -1;

        //Check to make sure there's room in the encounter.
        while (currentWeight < weightMax - 1)
        {
            if (weightMax != 20)
            {
                //If there is, draw a random number.
                enemyChoice = random.Next(0, totalNonBoss);

                switch (enemyChoice)
                {
                    case 0:
                        {
                            //If there's room to spawn a bat, spawn one at a random position in the room.
                            if (batPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                int x = random.Next(roomCenterX - 8, roomCenterX + 8);
                                int y = random.Next(roomCenterY - 8, roomCenterY + 8);
                                enemyInstance = Instantiate(batPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                                currentWeight += batPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 1:
                        {
                            //If there's room to spawn a slime, spawn one at a random position in the room.
                            if (slimePrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                int x = random.Next(roomCenterX - 8, roomCenterX + 8);
                                int y = random.Next(roomCenterY - 8, roomCenterY + 8);
                                enemyInstance = Instantiate(slimePrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                                currentWeight += slimePrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 2:
                        {
                            //If there's room to spawn a slug, spawn one at a random position in the room.
                            if (slugPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                int x = random.Next(roomCenterX - 8, roomCenterX + 8);
                                int y = random.Next(roomCenterY - 8, roomCenterY + 8);
                                enemyInstance = Instantiate(slugPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                                currentWeight += slugPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 3:
                        {
                            //If there's room to spawn a frog, spawn one at a random position in the room.
                            if (frogPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                int x = random.Next(roomCenterX - 8, roomCenterX + 8);
                                int y = random.Next(roomCenterY - 8, roomCenterY + 8);
                                enemyInstance = Instantiate(frogPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                                currentWeight += frogPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                }
            }
            else
            {
                //If there is, draw a random number.
                enemyChoice = random.Next(0, totalBoss);
                switch (enemyChoice)
                {
                    case 0:
                        {
                            //If there's room to spawn a snek, spawn one at a random position in the room.
                            if (snekPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                int x = random.Next(roomCenterX - 8, roomCenterX + 8);
                                int y = random.Next(roomCenterY - 8, roomCenterY + 8);
                                enemyInstance = Instantiate(snekPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                                currentWeight += snekPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 1:
                        {
                            //If there's room to spawn a dragon, spawn one at a random position in the room.
                            if (dragonPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                int x = random.Next(roomCenterX - 8, roomCenterX + 8);
                                int y = random.Next(roomCenterY - 8, roomCenterY + 8);
                                enemyInstance = Instantiate(dragonPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                                currentWeight += dragonPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 2:
                        {
                            //If there's room to spawn a lava golem, spawn one at a random position in the room.
                            if (golemPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                int x = random.Next(roomCenterX - 8, roomCenterX + 8);
                                int y = random.Next(roomCenterY - 8, roomCenterY + 8);
                                enemyInstance = Instantiate(golemPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                                currentWeight += golemPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                }
            }
        }
    }
}