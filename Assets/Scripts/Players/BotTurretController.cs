using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTurretController : MonoBehaviour
{
    [SerializeField] private Transform _head;

    [SerializeField] private Transform _player;

    [SerializeField] private float _rotateSpeed;

    private void FixedUpdate()
    {
        RotateLogic();
    }

    private void RotateLogic()
    {
        var look = Quaternion.LookRotation(_player.position - _head.position);
        _head.rotation = Quaternion.Lerp(_head.rotation, look, _rotateSpeed * Time.fixedDeltaTime);
    }
}
