using UnityEngine;
using UnityEngine.AI;

public class BotTankController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private GameObject[] _waypoints;
    private Transform _player;
    private Transform _currnetWaypoint;

    [SerializeField] private float _maxDistanceToPlayer;
    [SerializeField] private float _maxDistanceToWaypoint;
    [SerializeField] private bool _isPlayerVisiable;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currnetWaypoint = _waypoints[Random.Range(0, _waypoints.Length - 1)].transform;
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, _player.position) <= _maxDistanceToPlayer)
        {
            if (!_isPlayerVisiable)
                _isPlayerVisiable = true;

            _navMeshAgent.SetDestination(_player.position);
        }
        else if (_isPlayerVisiable)
            _isPlayerVisiable = false;

        if (!_isPlayerVisiable)
        {
            if (Vector3.Distance(transform.position, _currnetWaypoint.position) > _maxDistanceToWaypoint)
            {
                if (_navMeshAgent.velocity == Vector3.zero)
                {
                    _navMeshAgent.SetDestination(_currnetWaypoint.position);
                }
            }
            else if (_navMeshAgent.velocity == Vector3.zero)
            {
                _currnetWaypoint = _waypoints[Random.Range(0, _waypoints.Length - 1)].transform;
            }
        }
    }
}
