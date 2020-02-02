
using System;
using UnityEngine;
using Random = System.Random;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject ArmorDrop;
    public GameObject AmmoDrop;
    public GameObject ShotgunDrop;
    public GameObject NailgunDrop;
    private GameObject ItemDrop;

    private Random random = new Random();

    private static GameObject[] items = new GameObject[4];

    public void Start()
    {
        items[0] = ArmorDrop;
        items[1] = AmmoDrop;
        items[2] = ShotgunDrop;
        items[3] = NailgunDrop;
    }

    public void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
    }

    private void Die()
    {
        decreaeGameManagerCounter();

        Destroy(gameObject);

        ItemDrop = items[random.Next(5)];

        Instantiate(ItemDrop, transform.position, ItemDrop.transform.rotation);
        
    }

    private void decreaeGameManagerCounter()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        gm.GetComponent<GameManagerScript>().increaseKillCount();
    }
}



    
