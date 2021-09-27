using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotTank : MonoBehaviour
{
    #region Переменные

    private NavMeshAgent _navMeshAgent;

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

    private List<Transform> Targets;

    private Transform _tempObjects;
    private Transform _bulletSpawner;

    private ParticleSystem _PSDeath;

    #endregion

    #region Главные методы

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _tempObjects = GameObject.Find("TempObjects").transform;
        _bulletSpawner = transform.Find("BulletSpawner");

        _PSDeath = transform.Find("Particles").Find("Death").GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        Targets = new List<Transform>();
        var parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i); 
            if (child != transform)
            {
                Targets.Add(child);
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(Targets.Count);

        Move();
        Rotate();

        Fire();
    }

    #endregion

    #region Передвижение

    private void Move()
    {

    }

    private void Rotate()
    {

    }

    #endregion

    #region Стрельба

    private void Fire()
    {

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

        GetComponent<BotTank>().enabled = false;
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

    private bool IsTargetVisible()
    {

        return false;
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
