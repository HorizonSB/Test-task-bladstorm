using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float spawnTime = 1f;

    public static EnemySpawnManager Instance { get; private set; }
    private int enemiesKilled = 0;
    private int enemiesKilledToSpawnBoss = 10;

    public UnityEvent OnEnemyKilled = new UnityEvent();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
        OnEnemyKilled.AddListener(SpawnBoss);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Vector3 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnBoss()
    {
        enemiesKilled++;

        if (enemiesKilled >= enemiesKilledToSpawnBoss)
        {
            enemiesKilled = 0;
            Vector3 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
