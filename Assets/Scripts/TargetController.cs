using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int points;
    [SerializeField] private GameObject destroyEffectPrefab;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 moveDirection;
    private float moveSpeed = 1f;
    private float journeyLength;
    private float startTime;
    private const float minY = 10f; // Минимальное значение Y

    [SerializeField] private AudioClip _audio;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        startPosition = transform.position;
        ChooseNewDirection();
    }

    private void Update()
    {
        MoveTarget();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Ensure that only the cannonball triggers this event
        if (other.gameObject.CompareTag("Cannonball"))
        {
            EventManager.RaiseAddPoints(points);
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        // Play the destruction sound
        if (_audio != null && audioSource != null)
        {
            audioSource.PlayOneShot(_audio);
        }

        // Show the destroy effect
        if (destroyEffectPrefab != null)
        {
            GameObject spawnedDestroyEffect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
            GetComponent<Collider>().enabled = false; // Отключаем коллайдер
            GetComponent<MeshRenderer>().enabled = false; // Отключаем рендерер

            // Wait for 1 second to destroy the effect
            yield return new WaitForSeconds(1);
            Destroy(spawnedDestroyEffect);
        }

        // Destroy the parent object
        Destroy(transform.parent.gameObject);
    }

    private void ChooseNewDirection()
    {
        // Randomly choose a direction (x, y, or z)
        int direction = Random.Range(0, 3);
        float offset = Random.Range(-2f, 2f);

        switch (direction)
        {
            case 0: // x direction
                moveDirection = new Vector3(offset, 0, 0);
                break;
            case 1: // y direction
                moveDirection = new Vector3(0, offset, 0);
                // Ensure the new target Y position does not go below minY
                if (startPosition.y + moveDirection.y < minY)
                {
                    moveDirection.y = Mathf.Abs(offset); // Move up instead
                }
                break;
            case 2: // z direction
                moveDirection = new Vector3(0, 0, offset);
                break;
        }

        targetPosition = startPosition + moveDirection;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, targetPosition);
    }

    private void MoveTarget()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        // Move the object to the target position
        transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

        // If the object has reached the target position, choose a new direction
        if (fractionOfJourney >= 1)
        {
            startPosition = targetPosition;
            ChooseNewDirection();
        }
    }
}
