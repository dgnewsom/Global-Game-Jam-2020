using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addWeapons : MonoBehaviour
{
    public HashSet<string> weapons;

    public GameObject shotgun;
    public GameObject robot;


    // Start is called before the first frame update

    void Start()
    {
        weapons.Add("shotgun");
        shotgun.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (weapons.Contains("shotgun"))
        {
            shotgun.SetActive(true);
        }
        
    }
}
