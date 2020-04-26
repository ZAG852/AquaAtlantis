using UnityEngine;

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

    // The total type of unique enemies the game has to choose from
    public int totalEnemies;

    // A static random number generator to combat the pseudo-random nature of Random.Next().
    public static System.Random random = new System.Random();
    #endregion

    void Start()
    {
        Debug.Log("Yes");
        for (int i = 0; i < MapMaker.mapThingy.getNodeList().Count; i++)
        {
            int centerX = (int)(MapMaker.mapThingy.getNodeList()[i].positionX);
            int centerY = (int)(MapMaker.mapThingy.getNodeList()[i].positionY);
            int weightMax = 20;
            Debug.Log("Now filling room #" + i);
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
            Debug.Log(currentWeight + "/" + weightMax);
            //If there is, draw a random number.
            enemyChoice = random.Next(0, totalEnemies);

            //Attempt to add the enemy corresponding to the number drawn.
            switch (enemyChoice)
            {
                case 0:
                    {
                        //If there's room to spawn a bat, spawn one at a random position in the room.
                        if (batPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                        {
                            Debug.Log(currentWeight + "/" + weightMax + "; spawning an enemy worth " + batPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth);
                            int x = random.Next(roomCenterX - 9, roomCenterX + 9);
                            int y = random.Next(roomCenterY - 9, roomCenterY + 9);
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
                            Debug.Log(currentWeight + "/" + weightMax + "; spawning an enemy worth " + slimePrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth);
                            int x = random.Next(roomCenterX - 9, roomCenterX + 9);
                            int y = random.Next(roomCenterY - 9, roomCenterY + 9);
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
                            Debug.Log(currentWeight + "/" + weightMax + "; spawning an enemy worth " + slugPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth);
                            int x = random.Next(roomCenterX - 9, roomCenterX + 9);
                            int y = random.Next(roomCenterY - 9, roomCenterY + 9);
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
                            Debug.Log(currentWeight + "/" + weightMax + "; spawning an enemy worth " + frogPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth);
                            int x = random.Next(roomCenterX - 9, roomCenterX + 9);
                            int y = random.Next(roomCenterY - 9, roomCenterY + 9);
                            enemyInstance = Instantiate(frogPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                            currentWeight += frogPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                        }
                        break;
                    }
                case 4:
                    {
                        //If there's room to spawn a snek, spawn one at a random position in the room.
                        if (snekPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                        {
                            Debug.Log(currentWeight + "/" + weightMax + "; spawning an enemy worth " + snekPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth);
                            int x = random.Next(roomCenterX - 9, roomCenterX + 9);
                            int y = random.Next(roomCenterY - 9, roomCenterY + 9);
                            enemyInstance = Instantiate(snekPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                            currentWeight += snekPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                        }
                        break;
                    }
                case 5:
                    {
                        //If there's room to spawn a dragon, spawn one at a random position in the room.
                        if (dragonPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                        {
                            Debug.Log(currentWeight + "/" + weightMax + "; spawning an enemy worth " + dragonPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth);
                            int x = random.Next(roomCenterX - 9, roomCenterX + 9);
                            int y = random.Next(roomCenterY - 9, roomCenterY + 9);
                            enemyInstance = Instantiate(dragonPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                            currentWeight += dragonPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                        }
                        break;
                    }
                case 6:
                    {
                        //If there's room to spawn a lava golem, spawn one at a random position in the room.
                        if (golemPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth <= (weightMax - currentWeight))
                        {
                            Debug.Log(currentWeight + "/" + weightMax + "; spawning an enemy worth " + golemPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth);
                            int x = random.Next(roomCenterX - 9, roomCenterX + 9);
                            int y = random.Next(roomCenterY - 9, roomCenterY + 9);
                            enemyInstance = Instantiate(golemPrefab, new Vector2(x, y), Quaternion.identity) as GameObject;
                            currentWeight += golemPrefab.GetComponent<EnemyHealthManager>().enemyMaxHealth;
                        }
                        break;
                    }
            }
        }
    }
}