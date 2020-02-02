using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health = 500f;
    public float ammo = 100f;
    public float armourValue = 100f;
    private bool armourBreak = false;
    private Dictionary<string, float> armourList= new Dictionary<string, float>();

    public float Health { get => health; set => health = value; }
    public float Ammo { get => ammo; set => ammo = value; }
    public float Armour { get => armourValue; set => armourValue = value; }
    public float ammoRegenTime = 5f;
    public float ammoRegenCount = 5f;

    public NailGunShooting nailgun;
    public shotgunShooting shotgun;

    // public ArmourSound armourSound;
    //public HealthSound healthSound;
    public PickupSound pickupSound;
    



    // Start is called before the first frame update
    void Start()
    {
        armourList.Add("front", armourValue);
        armourList.Add("left", armourValue);
        armourList.Add("right", armourValue);
        armourList.Add("top", armourValue);
        armourList.Add("back", armourValue);
        //print(armourList["left"]);

        //nailgun = GetComponent<NailGunShooting>();


        // armourSound = gameObject.GetComponent<ArmourSound>();
        //healthSound = gameObject.GetComponent<HealthSound>();
        pickupSound = GetComponent<PickupSound>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ammo <= 10)
        {
            ammoRegen();
        }
    }

    public float getArmour(string loc)
    {
        return armourList[loc];
    }

    public void reduceArmour(string loc)
    {
        if (armourList[loc] > 0){
            print(loc + " armour damaged");
            armourList[loc] -= 10;
        }

    }
    public void reduceArmour(float damage)
    {
        
        armourList["front"] -= damage;
        armourList["back"] -= damage;
        armourList["left"] -= damage;
        armourList["right"] -= damage;
        armourList["top"] -= damage;
        print("armour damaged " + damage);
        foreach (KeyValuePair<string, float> entry in armourList)
        {
            
            if (entry.Value <= 0)
            {
                armourList[entry.Key] = 0;
                armourBreak = true;
            }
        }

    }


    public void pickUpArmour(int repairAmount,string loc)
    {
        if (armourList[loc] < armourValue)
        {
            armourList[loc] += 10;
            print(loc + " armour repaired " + armourList[loc]);
        }

    }
    public void pickUpArmour(int repairAmount)
    {
        armourList["front"] += repairAmount;
        armourList["back"] += repairAmount;
        armourList["left"] += repairAmount;
        armourList["right"] += repairAmount;
        armourList["top"] += repairAmount;
        print("repair armour " + repairAmount);
        foreach (KeyValuePair<string, float> entry in armourList)
        {

            if (entry.Value > 0)
            {
                armourBreak = false;
            }
        }
        //pickupSound.playSound();
    }

    public void pickUpAmmo(int ammoAmount)
    {
        
        if (ammo != 100)
        {
            ammo += ammoAmount;
            if (ammo > 100)
            {
                ammo = 100;
            }
        }
        print("Added ammo " + ammoAmount);
        //pickupSound.playSound();
    }

    public bool useAmmo(int ammoAmount)
    {
        if (ammo <= 0)
        {
            ammo = 0;
            return false;
        }
        else
        {
            ammo -= ammoAmount;
            return true;
        }
    }


    public void pickUpNailgun(float levelAmount)
    {
        
        print("level up Nailgun");
        nailgun.levelUp();
        //print(nailgun);
       // pickupSound.playSound();
    }

    public void pickUpShotgun(float levelAmount)
    {
        shotgun.levelUp();
        print("level up Shotgun ");
        //pickupSound.playSound();
    }

    public void reduceHealth(float damage)
    {
        health -= damage;
    }

    public void TakeDamage(float damage)
    {
        if (!armourBreak)
        {
            
            reduceArmour(damage);
            //armourSound.playSound();
        }
        else
        {
            
            reduceHealth(damage);
            //healthSound.playSound();
        }
    }


    public void applyEffect(string effectType, int amount)
    {
        if (effectType.Equals("Armour"))
        {
            pickUpArmour(amount);
        }
        if (effectType.Equals("Ammo"))
        {
            pickUpAmmo(amount);

        }
        if (effectType.Equals("Nailgun"))
        {
            pickUpNailgun(amount);
        }
        if (effectType.Equals("Shotgun"))
        {
            pickUpShotgun(amount);
        }
    }

    public void ammoRegen()
    {
        if (ammoRegenCount <= 0 && Ammo < 10) 
        {
            Ammo += 1;
            ammoRegenCount = ammoRegenTime;
        }
        else
        {
            ammoRegenCount -= Time.deltaTime;
        }

    }


}
