using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform _door;

    [SerializeField] private float _moveSpeed;

    private float _maxDoorPosY;
    private float _minDoorPosY;
    private bool _isEntered = false;

    private void Start()
    {
        _maxDoorPosY = _door.position.y;
        _minDoorPosY = -(_door.position.y + 1);
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    private void MoveLogic()
    {
        if (_isEntered && _door.position.y > _minDoorPosY)
        {
            _door.Translate(Vector3.down * _moveSpeed * Time.fixedDeltaTime);
        }
        else if (!_isEntered && _door.position.y < _maxDoorPosY)
        {
            _door.Translate(Vector3.up * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isEntered && other.tag == "Player")
        {
            _isEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isEntered = false;
        }
    }
}
