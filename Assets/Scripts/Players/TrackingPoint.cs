using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPoint : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = null;
    }

    private void Update()
    {
        if (_player == null)
        {
            _player ??= GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            Move();
        }
    }

    #region ������������

    private void Move()
    {
        transform.position = _player.position;
    }

    #endregion
}
