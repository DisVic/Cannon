using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private float lifeTime = 4f;
    [SerializeField] private GameObject destroyEffect;
    private bool destroy = false;
    private Rigidbody rb;

    void Start()
    {
    }

    void Update()
    {
        StatusCheck();
    }

    public void StatusCheck()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 && !destroy) TriggerDestroy();
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���������, �������� �� ������ ������� �� ����
        if (collision.gameObject.CompareTag("Target") && !destroy)
        {
            // ���������� ������
            Destroy(collision.gameObject);

            // ���������� ������
            TriggerDestroy();
            DestroyCannonBall();
        }
        else if (!destroy)
        {
            // ������ ���������� ������ ��� ����� ������ ������������, ���� ��� �� ������
            TriggerDestroy();
        }
    }

    public void TriggerDestroy()
    {
        if (destroy) return;

        destroy = true;

        if (destroyEffect != null)
        {
            GameObject spawnedDestroyEffect = Instantiate(destroyEffect, transform.position, transform.rotation);
            StartCoroutine(Death(spawnedDestroyEffect));
        }
        else
        {
            StartCoroutine(Death(null));
        }
    }
    private void DestroyCannonBall()
    {
        rb.velocity = Vector3.zero;
        Destroy(transform.GetChild(0).gameObject);
        Destroy(gameObject, 1);
    }
    private IEnumerator Death(GameObject spawnedDestroyEffect)
    {
        if (spawnedDestroyEffect != null)
        {
            yield return new WaitForSeconds(1);
            Destroy(spawnedDestroyEffect);
        }
        Destroy(gameObject);
    }
}
