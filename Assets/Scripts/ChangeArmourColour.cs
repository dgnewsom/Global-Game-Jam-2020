using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArmourColour : MonoBehaviour
{
    Renderer rend;
    public GameObject Player;
    private float maxArmour = -1;
    private float armour;
    private Color armourColour ;
    public string currentArmourName;
    public float alpha = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        //print(currentArmourName+maxArmour);
    }

    // Update is called once per frame
    void Update()
    {
        if (maxArmour == -1)
        {
            print("set armour");
            maxArmour = Player.GetComponent<PlayerStats>().getArmour(currentArmourName);

        }
        //armour = maxArmour;
        armour = Player.GetComponent<PlayerStats>().getArmour(currentArmourName);
        //print("armour " + (armour / maxArmour));
        ///*
        if (armour <= 0)
        {
            print("armour broken");
            armourColour = new Color(0.8f, 0.8f * (armour / maxArmour), 0.8f * (armour / maxArmour), 0f);


        }
        else
        {
            armourColour = new Color(0.8f, 0.8f * (armour / maxArmour), 0.8f * (armour / maxArmour), alpha);

        }
        //*/
        //armourColour = new Color(0.8f, 0.8f * (armour / maxArmour), 0.8f * (armour / maxArmour), 1f);

        rend.material.SetColor("_Color", armourColour);
        //print(armourColour);
        
    }
}
