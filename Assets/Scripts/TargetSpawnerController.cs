using UnityEngine;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public GameObject[] targetPrefabs; // Массив с префабами мишеней
    public Vector3 spawnAreaSize = new Vector3(10, 10, 10); // Размер области для спавна мишеней
    public LayerMask spawnLayerMask; // Слой, на котором могут быть спаунены мишени

    public int maxAttempts = 10; // Максимальное количество попыток для спавна мишеней
    public int maxTargets = 10; // Максимальное количество мишеней
    public float spawnInterval = 2f; // Интервал между спавном мишеней

    private List<GameObject> spawnedTargets = new List<GameObject>(); // Список уже созданных мишеней

    void Start()
    {
        InvokeRepeating("TrySpawnTarget", 0f, spawnInterval);
    }

    void TrySpawnTarget()
    {
        if (spawnedTargets.Count >= maxTargets)
        {
            return;
        }

        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            GameObject randomPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)]; // Случайный выбор префаба мишени
            Vector3 randomPosition = GetRandomPosition(); // Получение случайной позиции для спавна мишени

            // Проверка, не находится ли на выбранной позиции другой объект
            Collider[] colliders = Physics.OverlapBox(randomPosition, randomPrefab.transform.localScale / 2, Quaternion.identity, spawnLayerMask);
            if (colliders.Length == 0)
            {
                // Если на позиции нет других объектов, создаем мишень
                GameObject newTarget = Instantiate(randomPrefab, randomPosition, Quaternion.identity);
                spawnedTargets.Add(newTarget);
                break;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        // Получаем случайную позицию в кубической области спавна
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        // Устанавливаем случайную позицию в пределах ограниченного пространства, чтобы мишени не спавнились за границами игрового поля
        randomPosition += transform.position;

        return randomPosition;
    }

    void OnDrawGizmosSelected()
    {
        // Рисуем границы области спавна мишеней
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
