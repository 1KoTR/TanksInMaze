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

        _rigidbody.velocity = transform.forward * _moveSpeed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        //MoveLogic();
    }

    private void MoveLogic()
    {
        _rigidbody.AddForce(transform.forward * _moveSpeed * Time.fixedDeltaTime);
    }

    private void RotateLogic(Collision collision)
    {
        Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (_bouncesNumber > 0 && collision.gameObject.layer == 3)
    //    {
    //        RotateLogic(collision);
    //        _bouncesNumber--;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
