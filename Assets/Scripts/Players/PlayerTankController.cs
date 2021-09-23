using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTankController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Transform _bulletSpawner;
    private Transform _bombSpawner;
    private Transform _tempObjects;

    private float _horizontalAxis
    {
        get { return Input.GetAxis("Horizontal") * _rotateSpeed * Time.fixedDeltaTime; } 
    }
    private float _verticalAxis
    {
        get { return Input.GetAxis("Vertical") * _moveSpeed * Time.fixedDeltaTime; }
    }
    private float _fire1Axis
    {
        get { return Input.GetAxis("Fire1"); }
    }
    private float _fire2Axis
    {
        get { return Input.GetAxis("Fire2"); }
    }

    [Header("Variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private float _reloadFireTime;
    [SerializeField] private float _reloadSetBombTime;

    [SerializeField] private float _setBombDistance;

    [Header("Objects")]
    [SerializeField] private GameObject _defaultBullet;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _bulletSpawner = transform.GetChild(1);
        _bombSpawner = transform.GetChild(2);
        _tempObjects = GameObject.Find("TempObjects").transform;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        Fire();
    }

    #region Передвижение

    private void Move()
    {
        if (_verticalAxis != 0)
        {
            _rigidbody.AddForce(transform.forward * _verticalAxis);
        }
    }

    private void Rotate()
    {
        if (_horizontalAxis != 0)
        {
            _rigidbody.AddTorque(transform.up * _horizontalAxis);
        }
    }

    #endregion

    #region Стрельба

    private float _tmpFire1Time = 0;
    private void Fire()
    {
        if ((_tmpFire1Time += Time.fixedDeltaTime) >= _reloadFireTime && _fire1Axis > 0)
        {
            _tmpFire1Time = 0;

            Spawn(_defaultBullet, _bulletSpawner.position, _bulletSpawner.rotation);
        }
    }

    #endregion

    #region Уничтожение

    private void Destroy()
    {
        // Запуск анимации смерти.
        Destroy(transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Destroy();
        }
    }

    #endregion

    #region Методы

    private void Spawn(GameObject gameObj, Vector3 pos, Quaternion rot)
    {
        var obj = Instantiate(gameObj, pos, rot);
        obj.transform.SetParent(_tempObjects);
        obj.name = gameObj.name;
    }

    #endregion
}
