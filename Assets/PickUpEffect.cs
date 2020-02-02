using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffect : MonoBehaviour
{
    public string type = "";
    public int amount = 10;
    private void OnCollisionEnter(Collision collisionInfo)
    {
        print("hit detected "+ collisionInfo.collider.tag);
        if (collisionInfo.collider.tag == "Player")
        {
            
            PlayerStats playerStats = collisionInfo.collider.GetComponent<PlayerStats>();
            playerStats.applyEffect(type, amount);
            Destroy(gameObject);
        }

    }
}
