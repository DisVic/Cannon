using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private float lifeTime = 2.5f;
    [SerializeField] private GameObject destroyEffect;
    private bool destroy = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        StatusCheck();
    }

    public void StatusCheck()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 && !destroy) DestroyCannonBall();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && !destroy)
        {
            CoinController coinController = collision.gameObject.GetComponent<CoinController>();
            //if (coinController != null)
            //{
            //    //EventManager.RaiseAddPoints(coinController.points);
            //    Destroy(collision.gameObject); // ”ничтожаем мишень
            //}
            DestroyBall();
        }
        else if (!destroy)
        {
            DestroyCannonBall();
        }
    }

    private void DestroyBall()
    {
        destroy = true;
        rb.velocity = Vector3.zero;
        Destroy(gameObject); // ”ничтожаем снар€д через 1 секунду
    }

    private void DestroyCannonBall()
    {
        destroy = true;

        if (destroyEffect != null)
        {
            GameObject spawnedDestroyEffect = Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(spawnedDestroyEffect, 1); // ”ничтожаем эффект через 1 секунду
        }

        rb.velocity = Vector3.zero;
        Destroy(gameObject); // ”ничтожаем снар€д через 1 секунду
    }
}
