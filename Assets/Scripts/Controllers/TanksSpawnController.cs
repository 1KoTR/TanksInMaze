using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksSpawnController : MonoBehaviour
{
    private List<Transform> _spawnPoints;
    private Transform _players;

    [SerializeField] private GameObject _playerTank;
    [SerializeField] private GameObject _botTank;

    private void Awake()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("TankSpawnPoint"))
            _spawnPoints.Add(obj.transform);
        _players = GameObject.Find("Players").transform;
    }

    private void Start()
    {
        
    }
    
    private void Spawn(GameObject gameObj, Transform point)
    {
        var obj = Instantiate(gameObj, point.position, point.rotation);
        obj.transform.SetParent(_players);
        obj.name = gameObj.name;
    }
}
