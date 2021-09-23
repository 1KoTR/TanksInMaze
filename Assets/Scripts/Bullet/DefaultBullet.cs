using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DefaultBullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("Variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _maxBouncesCount;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Move();
    }

    #region Передвижение

    private void Move()
    {
        _rigidbody.AddForce(transform.forward * _moveSpeed, ForceMode.Impulse);
    }

    #endregion

    #region Уничтожение

    private void Destroy()
    {
        _maxBouncesCount--;
        if (_maxBouncesCount == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy();

        if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
