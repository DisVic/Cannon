using UnityEngine;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public GameObject[] targetPrefabs; // ������ � ��������� �������
    public Vector3 spawnAreaSize = new Vector3(10, 10, 10); // ������ ������� ��� ������ �������
    public LayerMask spawnLayerMask; // ����, �� ������� ����� ���� �������� ������

    public int maxAttempts = 10; // ������������ ���������� ������� ��� ������ �������
    public int maxTargets = 10; // ������������ ���������� �������
    public float spawnInterval = 2f; // �������� ����� ������� �������

    private List<GameObject> spawnedTargets = new List<GameObject>(); // ������ ��� ��������� �������

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
            GameObject randomPrefab = targetPrefabs[Random.Range(0, targetPrefabs.Length)]; // ��������� ����� ������� ������
            Vector3 randomPosition = GetRandomPosition(); // ��������� ��������� ������� ��� ������ ������

            // ��������, �� ��������� �� �� ��������� ������� ������ ������
            Collider[] colliders = Physics.OverlapBox(randomPosition, randomPrefab.transform.localScale / 2, Quaternion.identity, spawnLayerMask);
            if (colliders.Length == 0)
            {
                // ���� �� ������� ��� ������ ��������, ������� ������
                GameObject newTarget = Instantiate(randomPrefab, randomPosition, Quaternion.identity);
                spawnedTargets.Add(newTarget);
                break;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        // �������� ��������� ������� � ���������� ������� ������
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        // ������������� ��������� ������� � �������� ������������� ������������, ����� ������ �� ���������� �� ��������� �������� ����
        randomPosition += transform.position;

        return randomPosition;
    }

    void OnDrawGizmosSelected()
    {
        // ������ ������� ������� ������ �������
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
