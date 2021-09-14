using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnController : MonoBehaviour
{
    private Transform _floor;
    private Transform _parent;
     
    [SerializeField] private GameObject _wall;
    [SerializeField] private GameObject _door;

    [SerializeField] private int _wallSpawnChance;
    [SerializeField] private int _doorSpawnChance;

    private void Start()
    {
        _floor = GameObject.FindGameObjectWithTag("Floor").transform;
        _parent = _floor.parent.parent.Find("Walls");

        int floorLength = (int)_floor.localScale.x * 2;

        var startPos = _floor.transform.position - new Vector3(floorLength * 2.5f, 0, floorLength * 2.5f - 2.5f);
        var startRot = new Vector3(0, 90, 0);
        for (int x = 0; x <= floorLength; x++)
        {
            for (int y = 0; y < floorLength; y++)
            {
                if (x == 0 || x == floorLength)
                {
                    Spawn(_wall, startPos + new Vector3(5 * x, 0, 5 * y), startRot);
                }
                else
                {
                    int chance = Random.Range(0, 100);
                    if (chance < _wallSpawnChance)
                        if (chance < _doorSpawnChance)
                            Spawn(_door, startPos + new Vector3(5 * x, 0, 5 * y), startRot);
                        else
                            Spawn(_wall, startPos + new Vector3(5 * x, 0, 5 * y), startRot);
                }
            }
        }

        startPos = _floor.transform.position - new Vector3(floorLength * 2.5f - 2.5f, 0, floorLength * 2.5f);
        startRot = Vector3.zero;
        for (int x = 0; x < floorLength; x++)
        {
            for (int y = 0; y <= floorLength; y++)
            {
                if (y == 0 || y == floorLength)
                {
                    Spawn(_wall, startPos + new Vector3(5 * x, 0, 5 * y), startRot);
                }
                else
                {
                    int chance = Random.Range(0, 100);
                    if (chance < _wallSpawnChance)
                        if (chance < _doorSpawnChance)
                            Spawn(_door, startPos + new Vector3(5 * x, 0, 5 * y), startRot);
                        else
                            Spawn(_wall, startPos + new Vector3(5 * x, 0, 5 * y), startRot);
                }
            }
        }
    }

    private void Spawn(GameObject obj, Vector3 pos, Vector3 rot)
    {
        var wall = Instantiate(obj, pos, Quaternion.Euler(rot));
        wall.transform.SetParent(_parent);
        wall.name = obj.name;
    }
}
