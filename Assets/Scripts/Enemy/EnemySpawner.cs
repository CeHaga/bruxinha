using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemyPrefabs;

    [Header("Debug")]
    [SerializeField] private bool logEnemySpawn;

    private ObjectPool<EnemyController>[] enemyPools;

    private void Start()
    {
        enemyPools = new ObjectPool<EnemyController>[enemyPrefabs.Length];
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPools[i] = createObjectPool(enemyPrefabs[i], i, 10, 20);
        }
        InvokeRepeating(nameof(Spawn), 0, 1);
    }

    private ObjectPool<EnemyController> createObjectPool(EnemyController prefab, int enemyType, int size, int maxSize)
    {
        return new ObjectPool<EnemyController>(() =>
        {
            var enemy = Instantiate(prefab);
            float offset = Random.Range(-80, 80);
            enemy.Init(offset, (enemy) => KillEnemy(enemy, enemyType));
            return enemy;
        }, (enemy) =>
        {
            enemy.gameObject.SetActive(true);
            float offset = Random.Range(-80, 80);
            enemy.Init(offset);
        }, (enemy) =>
        {
            enemy.gameObject.SetActive(false);
        }, (enemy) =>
        {
            Destroy(enemy.gameObject);
        }, false, size, maxSize);
    }

    private void Spawn()
    {
        int enemyType = Random.Range(0, enemyPrefabs.Length);
        if (logEnemySpawn) Debug.Log($"Spawning enemy of type {enemyPrefabs[enemyType].name}");
        enemyPools[enemyType].Get();
    }

    private void KillEnemy(EnemyController enemy, int enemyType, bool didPlayerKill = false)
    {
        enemyPools[enemyType].Release(enemy);
    }
}
