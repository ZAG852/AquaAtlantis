using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    #region State Variables
    //Slots to hold the prefabs for every boss and nonboss enemy in the game. These should be filled manually in Unity.
    public GameObject batPrefab;
    public GameObject slimePrefab;
    public GameObject slugPrefab;
    public GameObject frogPrefab;
    public GameObject snekPrefab;
    public GameObject dragonPrefab;
    public GameObject golemPrefab;

    //The empty slot into which enemies are instantiated as the game spawns them.
    public GameObject enemyInstance;

    // The total type of unique nonboss enemies the game has to choose from.
    public int totalNonBoss;

    // The total type of unique boss enemies the game has to choose from.
    public int totalBoss;

    // A static random number generator to combat the pseudo-random nature of Random.Next().
    public static System.Random random = new System.Random();
    #endregion

    void Start()
    {
        //For every node (room) on the map...
        for (int i = 0; i < MapMaker.mapThingy.getNodeList().Count; i++)
        {
            //...find its center x and y coordinates...
            int centerX = (int)(MapMaker.mapThingy.getNodeList()[i].positionX);
            int centerY = (int)(MapMaker.mapThingy.getNodeList()[i].positionY);

            //...set its carrying capacity for enemies...
            int weightMax = 15;

            //...if it's the last room, upgrade it to a boss room...
            try
            {
                Nodies.Node test = MapMaker.mapThingy.getNodeList()[i + 1];
            }
            catch
            {
                weightMax = 20;
            }

            //...and call the FillRoom() method to actually put the enemies into it.
            FillRoom(weightMax, centerX, centerY);
        }
    }

    /// <summary>
    /// Generates a spawn point for an enemy in one of three zones.
    /// </summary>
    /// <param name="roomCenterX">The x coordinate for the center of the room.</param>
    /// <param name="roomCenterY">The y coordinate for the center of the room.</param>
    /// <returns>A 2D vector containing the point that the next enemy will spawn at.</returns>
    public Vector2 SpawnPoint(int roomCenterX, int roomCenterY)
    {
        //Create variables for the future enemy's x and y spawn coordinates.
        int x = 0, y = 0;

        //Randomly choose one of the three spawn areas for the enemy to go to.
        int gridArea = random.Next(0, 4);

        switch (gridArea)
        {
            //Determine a random spawn point for this enemy within Area #1.
            case (0):
                {
                    x = random.Next(roomCenterX - 7, roomCenterX + 7);
                    y = random.Next(roomCenterY - 3, roomCenterY + 8);
                    break;
                }
            //Determine a random spawn point for this enemy within Area #1 (again). Area #1 is listed twice because it covers considerably more of the map than areas two or three.
            case (1):
                {
                    x = random.Next(roomCenterX - 7, roomCenterX + 7);
                    y = random.Next(roomCenterY - 3, roomCenterY + 8);
                    break;
                }
            //Determines a random spawn point for this enemy within Area #2.
            case (2):
                {
                    x = random.Next(roomCenterX - 11, roomCenterX + 7);
                    y = random.Next(roomCenterY - 9, roomCenterY - 6);
                    break;
                }
            //Determines a random spawn point for this enemy within Area #3.
            case (3):
                {
                    x = random.Next(roomCenterX - 11, roomCenterX - 9);
                    y = random.Next(roomCenterY - 9, roomCenterY + 8);
                    break;
                }
        }

        //Return a vector containing the point at which the next enemy will be spawned.
        return new Vector2(x, y);
    }
    
    
    /// <summary>
    /// Fills the room to capacity with randomly chosen enemies.
    /// </summary>
    /// <param name="weightMax">How many HP worth of enemies will spawn in this room.</param>
    /// <param name="roomCenterPoint">The center coordinate of the node (to randomize spawnpoints).</param>
    public void FillRoom(int weightMax, int roomCenterX, int roomCenterY)
    {
        //Set key variables.
        int currentWeight = 0, enemyChoice = -1;

        //Check to make sure there's room in the encounter.
        while (currentWeight < weightMax - 1)
        {
            //As long as the maximum weight is NOT exactly 20, we know that this room isn't a boss room.
            if (weightMax != 20)
            {
                //Try to choose one of the regular enemies to put in the room.
                enemyChoice = random.Next(0, totalNonBoss);
                switch (enemyChoice)
                {
                    case 0:
                        {
                            //If there's room to spawn a bat, spawn one at a random position in the room.
                            if (batPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                enemyInstance = Instantiate(batPrefab, SpawnPoint(roomCenterX, roomCenterY), Quaternion.identity) as GameObject;
                                currentWeight += batPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 1:
                        {
                            //If there's room to spawn a slime, spawn one at a random position in the room.
                            if (slimePrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                enemyInstance = Instantiate(slimePrefab, SpawnPoint(roomCenterX, roomCenterY), Quaternion.identity) as GameObject;
                                currentWeight += slimePrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 2:
                        {
                            //If there's room to spawn a slug, spawn one at a random position in the room.
                            if (slugPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                enemyInstance = Instantiate(slugPrefab, SpawnPoint(roomCenterX, roomCenterY), Quaternion.identity) as GameObject;
                                currentWeight += slugPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 3:
                        {
                            //If there's room to spawn a frog, spawn one at a random position in the room.
                            if (frogPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                enemyInstance = Instantiate(frogPrefab, SpawnPoint(roomCenterX, roomCenterY), Quaternion.identity) as GameObject;
                                currentWeight += frogPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                }
            }
            //If the room's maximum weight IS exactly 20, then we know that this is a boss room.
            else
            {
                //Choose one of the bosses at random to spawn into this room.
                enemyChoice = random.Next(0, totalBoss);
                switch (enemyChoice)
                {
                    case 0:
                        {
                            //If there's room to spawn a snek, spawn one at a random position in the room.
                            if (snekPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                enemyInstance = Instantiate(snekPrefab, SpawnPoint(roomCenterX, roomCenterY), Quaternion.identity) as GameObject;
                                currentWeight += snekPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 1:
                        {
                            //If there's room to spawn a dragon, spawn one at a random position in the room.
                            if (dragonPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                enemyInstance = Instantiate(dragonPrefab, SpawnPoint(roomCenterX, roomCenterY), Quaternion.identity) as GameObject;
                                currentWeight += dragonPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                    case 2:
                        {
                            //If there's room to spawn a lava golem, spawn one at a random position in the room.
                            if (golemPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                            {
                                enemyInstance = Instantiate(golemPrefab, SpawnPoint(roomCenterX, roomCenterY), Quaternion.identity) as GameObject;
                                currentWeight += golemPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                            }
                            break;
                        }
                }
            }
        }
    }
}