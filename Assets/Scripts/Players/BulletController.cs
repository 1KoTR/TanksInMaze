using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _bouncesNumber;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        MoveLogic();
    }

    private void MoveLogic()
    {
        _rigidbody.velocity = transform.forward * _moveSpeed * Time.fixedDeltaTime;
    }
}
