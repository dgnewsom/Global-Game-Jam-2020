using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerStats : MonoBehaviour
{
    private PlayerStats playerStats;
    public enum Stat { Health, Ammo, Armour };
    private float initHealth;
    private float initAmmo;

    public Stat stat;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        initHealth = playerStats.Health;
        initAmmo = playerStats.Ammo;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stat.Equals(Stat.Ammo))
        {
            //this.GetComponent<TextMesh>().text = Mathf.Floor(Mathf.Pow((playerStats.Ammo / (initHealth)), 2) + 1) + "%";
            this.GetComponent<TextMesh>().text = playerStats.Ammo + "%";
        }

        if (stat.Equals(Stat.Health))
        {
            //this.GetComponent<TextMesh>().text = Mathf.Floor(Mathf.Pow((playerStats.Health / (initHealth )),2) + 1) + "%";
            this.GetComponent<TextMesh>().text = Mathf.Floor(playerStats.Health*100/initHealth) + "%";

        }

        if (stat.Equals(Stat.Armour))
        {
            this.GetComponent<TextMesh>().text = playerStats.getArmour("front") + "%";
        }
    }
}
