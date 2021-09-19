using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private Transform _tempObjectsParent;
    
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _mine;

    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private Transform _mineSpawner;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _shotReloadTime;
    [SerializeField] private float _mineReloadTime;
    [SerializeField] private float _minePlacingDistance;

    private float _vertical
    {
        get { return Input.GetAxis("Vertical") * _moveSpeed; }
    }
    private float _horizontal
    {
        get { return Input.GetAxis("Horizontal") * _rotateSpeed; }
    }
    private bool _isShooting
    {
        get { return Input.GetAxis("Fire1") != 0 ? true : false; }
    }
    private bool _isMinePlacing
    {
        get { return Input.GetAxis("Fire2") != 0 ? true : false; }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveLogic();
        RotationLogic();

        ShootingLogic();
        MinePlantingLogic();
    }

    private void MoveLogic()
    {
        if (_vertical != 0)
        {
            _rigidbody.AddForce(transform.forward * _vertical);
        }
    }

    private void RotationLogic()
    {
        if (_horizontal != 0)
        {
            transform.Rotate(0, _horizontal, 0);
        }
    }

    private float _time1 = 0;
    private void ShootingLogic()
    {
        if ((_time1 += Time.deltaTime) > _shotReloadTime && _isShooting)
        {
            _time1 = 0;
            var bullet = Instantiate(_bullet, _bulletSpawner.position, _bulletSpawner.rotation);
        }
    }

    private float _time2 = 0;
    private void MinePlantingLogic()
    {
        if ((_time2 += Time.deltaTime) > _mineReloadTime && _isMinePlacing)
        {
            if (Physics.Raycast(_mineSpawner.position, _mineSpawner.forward, out RaycastHit hit, _minePlacingDistance))
                if (hit.transform.tag == "Wall")
                {
                    _time2 = 0;
                    Debug.Log("Mine!");
                }
        }
    }
}
