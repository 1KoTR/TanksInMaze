using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private GameObject _camera;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private float _vertical
    {
        get { return Input.GetAxis("Vertical") * _moveSpeed; }
    }
    private float _horizontal
    {
        get { return Input.GetAxis("Horizontal") * _rotateSpeed; }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveLogic();
        RotateLogic();
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
}
