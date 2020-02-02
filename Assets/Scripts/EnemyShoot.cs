using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    private GameObject player;
    public Quaternion offset = Quaternion.Euler(0, 90, 0);
    private Vector3 diff;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()

    {
        transform.LookAt(player.transform);
    }
}
