using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private GameObject _bullet;

    [SerializeField] private Transform _bulletSpawner;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _shootTime;

    private float _time = 0;

    private float _vertical
    {
        get { return Input.GetAxis("Vertical") * _moveSpeed; }
    }
    private float _horizontal
    {
        get { return Input.GetAxis("Horizontal") * _rotateSpeed; }
    }
    private bool _shoot
    {
        get { return Input.GetAxis("Fire1") != 0 ? true : false; }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveLogic();
        RotateLogic();

        ShootLogic();
    }

    private void MoveLogic()
    {
        if (_vertical != 0)
        {
            _rigidbody.AddForce(transform.forward * _vertical);
        }
    }

    private void RotateLogic()
    {
        if (_horizontal != 0)
        {
            transform.Rotate(0, _horizontal, 0);
        }
    }

    private void ShootLogic()
    {
        if ((_time += Time.deltaTime) > _shootTime && _shoot)
        {
            _time = 0;
            var bullet = Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
        }
    }
}
