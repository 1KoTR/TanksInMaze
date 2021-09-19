using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotTankController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private float _distanceToTarget 
    {
        get { return Vector3.Distance(transform.position, _target.position); }
    }
    private Transform _target;

    [SerializeField] private Transform[] _points;

    [SerializeField] private float _viewDistance;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        MoveTo();
    }

    private void MoveTo()
    {
        if (_distanceToTarget <= _viewDistance)
        {
            _navMeshAgent.SetDestination(_target.position);
        }
    }
}
