using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPCMove_Test : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        _destination = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent == null)
        {
            Debug.LogError("the new mesh agent component is not attached to " + gameObject.name);

        }
        else
        {
            SetDestination();
        }

        //if (rb.)
    }
}
