using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    #region ??????????

    [Header("??????????:")]
    [SerializeField] private float MoveSpeed;

    [SerializeField] private float MaxBouncesCount;

    #endregion

    #region ??????? ??????

    private void Start()
    {
        Move();
    }

    #endregion

    #region ????????????

    private void Move()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * MoveSpeed, ForceMode.Impulse);
    }

    #endregion

    #region ??????

    private void Death()
    {
        var death = transform.Find("Particles").Find("Death").GetComponent<ParticleSystem>();

        transform.Find("Model").gameObject.SetActive(false);

        death.Play();

        Destroy(gameObject, death.main.startLifetimeMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var gameObj = collision.gameObject;
        var t = gameObj.tag;
        if (gameObj.layer == 3 && --MaxBouncesCount <= 0 ||
            t == "Player" || t == "Bullet")
        {
            Death();
        }
    }

    #endregion
}
