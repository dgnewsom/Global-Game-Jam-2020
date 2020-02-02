using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgunShooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 500f;
    public float impactForce = 15f;
    public float fireRate = 10f;
    Vector3[] pelletArray = new Vector3[4];


    public ParticleSystem MuzzleFlash;
    public GameObject impact;

    private int wait = 150;
    private int counter = 0;

    private AudioSource mAudioSrc;

    private void Start()
    {
        mAudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 pellet = transform.forward;
        float pelletRotate;
        //pellet1 = Quaternion.AngleAxis(-30, transform.up) * pellet1;
        for (int i = 0; i < 4f; i++)
        {
            pelletArray[i] = Quaternion.AngleAxis(-12, transform.up) * pellet;
            //print(i);
            pelletRotate = (i / 4f);
            pelletArray[i] = Quaternion.AngleAxis(360 * pelletRotate, transform.forward) * pelletArray[i];
            //print(pelletArray[i]);
            //print(pelletRotate);
        }

                     
        if (counter >= wait)
        {

            Shoot();
            counter = 0;

        }
        counter ++;
        

    }
  


    private void Shoot()
    {
        MuzzleFlash.Clear();
        MuzzleFlash.Play();
        mAudioSrc.Play();

        RaycastHit[] hitArray = new RaycastHit[5];

        if (Physics.Raycast(transform.position, transform.forward, out hitArray[4], range))
        {
            Debug.Log(hitArray[4].transform.name);

            Target target = hitArray[4].transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hitArray[4].rigidbody != null)
            {
                hitArray[4].rigidbody.AddForce(hitArray[4].normal * impactForce);
            }

            GameObject impactGo = Instantiate(impact, hitArray[4].point, Quaternion.LookRotation(hitArray[4].normal));
            Destroy(impactGo, 2f);
        }

        for (int i = 0; i < 4f; i++)
        {
            if (Physics.Raycast(transform.position, pelletArray[i], out hitArray[i], range))
            {
                //Debug.Log(hitArray[i].transform.name + " " + i);

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
                Destroy(impactGo, 2f);
            }

        }

    }
}
