using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunShooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 500f;
    public float impactForce = 15f;
    public float fireRate = 10f;
    public int pelletNumber = 4;

    public float fireRatePerLevel = 5f;
    private int level = 1;

    public int ammoCost = 1;

    Vector3[] pelletArray;


    public ParticleSystem MuzzleFlash;
    public GameObject impact;

    private float nextShot = 0f;

    public bool randomSpread = true;
    public float maxRandomSpread;
    public float minRandomSpread;

    private PlayerStats playerStates;

    private AudioSource mAudioSrc;

    private void Start()
    {
        playerStates = FindObjectOfType<PlayerStats>();
        mAudioSrc = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

        
        /*
        Debug.DrawRay(transform.position, pelletArray[0] * 50, Color.green);
        Debug.DrawRay(transform.position, pelletArray[1] * 50, Color.red);
        Debug.DrawRay(transform.position, pelletArray[2] * 50, Color.blue);
        Debug.DrawRay(transform.position, pelletArray[3] * 50, Color.cyan);
        */
        if (Input.GetMouseButton(0) && Time.time >= nextShot)
        {
            //print(playerStates.Ammo);
            nextShot = Time.time + 10f / fireRate;
            if (consumeAmmo())
            {
                Shoot();

            }

        }

    }

    private void Shoot()
    {
        MuzzleFlash.Clear();
        MuzzleFlash.Play();
        mAudioSrc.Play();

        pelletArray = new Vector3[pelletNumber];
        Vector3 pellet = transform.forward;
        float pelletRotate;
        for (int i = 0; i < pelletNumber; i++)
        {
            pelletArray[i] = Quaternion.AngleAxis(10, transform.up) * pellet;
            pelletRotate = (360 * i / pelletNumber + 360 / pelletNumber * 2);
            pelletArray[i] = Quaternion.AngleAxis(pelletRotate, transform.forward) * pelletArray[i];
        }



        RaycastHit[] hitArray = new RaycastHit[pelletNumber+1];


        if (randomSpread)
        {

            randomiseShots();
        }


        if (Physics.Raycast(transform.position, transform.forward , out hitArray[0], range))
        {
            Debug.Log(hitArray[0].transform.name);

            Target target = hitArray[0].transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hitArray[0].rigidbody != null)
            {
                hitArray[0].rigidbody.AddForce(hitArray[0].normal * impactForce);
            }

            GameObject impactGo = Instantiate(impact, hitArray[0].point, Quaternion.LookRotation(hitArray[0].normal));
            Destroy(impactGo, 1f);
        }


        for (int i = 1; i <= pelletNumber; i++) {
            if (Physics.Raycast(transform.position, pelletArray[i-1], out hitArray[i], range))
            {
                Debug.Log(hitArray[i].transform.name+" "+i);

                Target target = hitArray[i].transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (hitArray[i].rigidbody != null)
                {
                    hitArray[i].rigidbody.AddForce(hitArray[i].normal * impactForce);
                }

                GameObject impactGo = Instantiate(impact, hitArray[i].point, Quaternion.LookRotation(hitArray[i].normal));
                Destroy(impactGo, 1f);
            }

        }

    }

    private void randomiseShots()
    {

        for (int i = 0; i <pelletArray.Length; i++)
        {
            pelletArray[i] =Quaternion.AngleAxis(UnityEngine.Random.Range(minRandomSpread,maxRandomSpread), transform.up)* pelletArray[i];
        }
    }
    public void levelUp()
    {
        if (level != 5)
        {
            fireRate += fireRatePerLevel;
            pelletNumber += 2;
            level += 1;

        }

    }

    public Boolean consumeAmmo()
    {
        return playerStates.useAmmo(ammoCost * (pelletNumber + 1));

    }

}
