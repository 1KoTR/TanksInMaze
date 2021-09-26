using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTank : MonoBehaviour
{
    #region Переменные

    private Rigidbody _rigidbody;

    [Header("Variables")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;

    [SerializeField] private float BulletFireReloadTime;
    [SerializeField] private float RocketFireReloadTime;

    private bool _isBulletFire;
    private bool _isRocketFire;

    [Header("Objects")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Rocket;

    private Transform _tempObjects;
    private Transform _bulletSpawner;
    private Transform _camera;

    private ParticleSystem _PSDeath;

    #endregion

    #region Главные методы

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _tempObjects = GameObject.Find("TempObjects").transform;
        _bulletSpawner = transform.Find("BulletSpawner");
        _camera = GameObject.Find("Camera").transform;

        _PSDeath = transform.Find("Particles").Find("Death").GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        SetCameraPosition();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        Fire();
    }

    #endregion

    #region Передвижение

    private void Move()
    {
        var v = Input.GetAxis("Vertical");
        if (v != 0)
        {
            _rigidbody.AddForce(transform.forward * v * MoveSpeed * Time.fixedDeltaTime);
        }
    }

    private void Rotate()
    {
        var h = Input.GetAxis("Horizontal");
        if (h != 0)
        {
            _rigidbody.AddTorque(transform.up * h * RotateSpeed * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Стрельба

    private void Fire()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            StartCoroutine(BulletFire());
        }
        else if (Input.GetAxis("Fire2") != 0)
        {
            StartCoroutine(RocketFire());
        }
    }

    private IEnumerator BulletFire()
    {
        if (!_isBulletFire)
        {
            _isBulletFire = true;
            SpawnObject(Bullet);
            yield return new WaitForSeconds(BulletFireReloadTime);
            _isBulletFire = false;
        }
    }

    private IEnumerator RocketFire()
    {
        if (!_isRocketFire)
        {
            _isRocketFire = true;
            SpawnObject(Rocket);
            yield return new WaitForSeconds(RocketFireReloadTime);
            _isRocketFire = false;
        }
    }

    #endregion

    #region Смерть

    private void Death()
    {
        transform.tag = "Untagged";

        GetComponent<PlayerTank>().enabled = false;
        transform.Find("Model").gameObject.SetActive(false);

        _PSDeath.Play();

        Destroy(gameObject, _PSDeath.main.startLifetimeMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Death();
        }
    }

    #endregion

    #region Другие методы

    private void SetCameraPosition()
    {
        _camera.position = transform.position;
    }

    private void SpawnObject(GameObject gameObject)
    {
        Instantiate(gameObject, _bulletSpawner.position, _bulletSpawner.rotation, _tempObjects);
    }

    private void SpawnObject(GameObject gameObject, Transform transform)
    {
        Instantiate(gameObject, transform.position, transform.rotation, _tempObjects);
    }

    #endregion
}
