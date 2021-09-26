using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DefaultRocket : MonoBehaviour
{
    #region ����������

    [Header("����������:")]
    [SerializeField] private float MoveSpeed;

    #endregion

    #region ������� ������

    private void Start()
    {
        Move();
    }

    #endregion

    #region ������������

    private void Move()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * MoveSpeed, ForceMode.Impulse);
    }

    #endregion

    #region ������

    private void Death()
    {
        var particles = transform.Find("Particles");
        var death = particles.Find("Death").GetComponent<ParticleSystem>();

        particles.Find("Flame").GetComponent<ParticleSystem>().Stop();
        transform.Find("Model").gameObject.SetActive(false);

        death.Play();

        Destroy(gameObject, death.main.startLifetimeMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Death();
        Destroy(collision.gameObject);
    }

    #endregion
}
