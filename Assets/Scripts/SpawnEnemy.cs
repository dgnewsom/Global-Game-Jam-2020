using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyType;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (gameManager.GetComponent<GameManagerScript>().readyToSpawn())
        {
            spawnEnemy();
            gameManager.GetComponent<GameManagerScript>().increaseCount();
        }
        */
    }

    public void spawnEnemy()
    {
        Instantiate(enemyType, transform.position, new Quaternion(0, 0, 0, 0));


    }
}
