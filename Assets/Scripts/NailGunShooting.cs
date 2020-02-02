using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailGunShooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100000f;
    public float impactForce = 15f;
    public float fireRate = 15f;
    public float fireRatePerLevel = 20f;
    private int level = 1;
    public int ammoCost = 1;



    public ParticleSystem MuzzleFlash;
    public GameObject impact;

    private float nextShot = 0f;

    public bool randomSpread = true;
    public float maxRandomSpread;
    public float minRandomSpread;

    private Vector3 shotAngle;

    private PlayerStats playerStates;

    private AudioSource mAudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerStates = FindObjectOfType<PlayerStats>();

        mAudioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward*-1, Color.green);


        if (Input.GetMouseButton(1) && Time.time >= nextShot)
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

        //("shoot");
        //ParticleSystem muzzelFlashGo = Instantiate(MuzzleFlash);
        //muzzelFlashGo.Play();
        shotAngle = transform.forward * -1;

        if (randomSpread)
        {

            randomiseShots();
        }


        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, shotAngle, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);
        }
        //Destroy(muzzelFlashGo);

    }
    private void randomiseShots()
    {
        shotAngle = Quaternion.AngleAxis(UnityEngine.Random.Range(minRandomSpread, maxRandomSpread), transform.up) * shotAngle;

    }

    public void levelUp()
    {
        if (level != 5)
        {
            fireRate += fireRatePerLevel;
            level += 1;
            print("current level " + level);
        }
       
    }

    public Boolean consumeAmmo()
    {
        return playerStates.useAmmo(ammoCost);

    }
}
