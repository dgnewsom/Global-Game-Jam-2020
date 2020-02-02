using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyNailGunShooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100000f;
    public float impactForce = 15f;
    public float fireRate = 10f;
    private Random random = new Random();

    public ParticleSystem MuzzleFlash;
    public GameObject impact;

    public int shootingGroup = 3;

    private float wait = 1f;
    private int shotCount;

    private AudioSource mAudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        shotCount = shootingGroup;
        mAudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(wait +" " +shotCount);
        if (wait < 0)
        {
            Shoot();
            shotCount--;
            if (shotCount != 0)
            {
                wait = 1;
            }
            else
            {
                shotCount = shootingGroup;
                wait = 3;
            }
        }
        else
        {
            wait -= Time.deltaTime;

        }


    }

    private void Shoot()
    {
        MuzzleFlash.Play();
        mAudioSrc.Play();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward * -1, out hit, range))
        {
            Debug.Log(hit.transform.name);

            PlayerStats playerStatus = hit.transform.GetComponent<PlayerStats>();
            if (playerStatus != null)
            {
                playerStatus.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * impactForce);
            }

            GameObject impactGo = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
        }
    }
}
