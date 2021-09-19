using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotTankController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Transform _target;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        _navMeshAgent.SetDestination(_target.position);
    }
}
