using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _angularSpeed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0)
        {
            transform.Rotate(transform.up, horizontal * _angularSpeed);
        }

        if (vertical != 0)
        {
            _rigidBody.AddForce(transform.forward * vertical * _moveSpeed);
        }
    }

    private void Move()
    {

        _rigidBody.AddForce(transform.forward * Input.GetAxis("Vertical") * _moveSpeed);
    }

    private void Rotate()
    {
        transform.Rotate(transform.up, Input.GetAxis("Horizontal") * _angularSpeed);
    }
}
