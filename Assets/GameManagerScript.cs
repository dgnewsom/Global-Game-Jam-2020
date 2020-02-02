using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private float timer;
    private int enemyCounter;
    private int enemyKilled;
    public int maxEnemiesPerWave;
    public int maxEnemies = 50;
    public float interval = 5;
    public SpawnEnemy[] spawners;
    private int loop = 0;
    private bool increased = false;

    // Start is called before the first frame update
    void Start()
    {
        spawners = FindObjectsOfType<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //printEnemyCount();
        spawnEnemyRoutine();
        timer = Time.time;
        increaseDifficaulty();
        
    }

    public bool readyToSpawn()
    {
        return (enemyCounter <= maxEnemies && enemyCounter <= maxEnemiesPerWave / 2 );
    }

    public void increaseCount()
    {
        enemyCounter += 1;
    }

    public void increaseDifficaulty()
    {
        //print(increased);
        if (Mathf.Repeat( Mathf.Floor(timer),interval) == 0 && !increased && (Mathf.Repeat(enemyKilled, 5) == 3))
        {
            maxEnemiesPerWave += 2;
            increased = true;
        }
        if (Mathf.Repeat(Mathf.Floor(timer), interval) == 1)
        {
            increased = false;
        }
    }
    public void increaseKillCount()
    {
        enemyKilled += 1;
        enemyCounter -= 1;
        print(enemyKilled);

    }

    private void printEnemyCount()
    {
        print(enemyCounter+" "+ maxEnemiesPerWave + " " + maxEnemies +" "+ (Mathf.Repeat(Mathf.Floor(timer), interval) == 0));
    }

    private void spawnEnemyRoutine()
    {
        if (readyToSpawn())
        {
            spawners[(int) Mathf.Repeat(loop, spawners.Length)].GetComponent<SpawnEnemy>().spawnEnemy();
            loop += 1;
            increaseCount();
        }
    }


}
