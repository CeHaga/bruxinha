using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public struct EnemySpawnOptions
{
    public bool canSpawn;
    public EnemyController enemyController;
}
public class EnemySpawner : MonoBehaviour
{
    private EnemyController[] enemyPrefabs;
    [SerializeField] private EnemySpawnOptions[] enemySpawnOptions;
    [SerializeField] private ShootEvent OnShoot;

    [Header("Debug")]
    [SerializeField] private bool logEnemySpawn;

    private ObjectPool<EnemyController>[] enemyPools;

    private void Start()
    {
        enemyPrefabs = enemySpawnOptions
                        .Where(option => option.canSpawn)
                        .Select(option => option.enemyController)
                        .ToArray();
        enemyPools = new ObjectPool<EnemyController>[enemyPrefabs.Length];
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPools[i] = createObjectPool(enemyPrefabs[i], i, 10, 20);
        }
        InvokeRepeating(nameof(Spawn), 0, 3);
    }

    private ObjectPool<EnemyController> createObjectPool(EnemyController prefab, int enemyType, int size, int maxSize)
    {
        return new ObjectPool<EnemyController>(() =>
        {
            var enemy = Instantiate(prefab);
            enemy.OnCreateObject(
                (enemy, playerKill) => KillEnemy(enemy, enemyType, playerKill),
                (bulletScriptable, position) => OnShoot.Invoke(bulletScriptable, position));
            return enemy;
        }, (enemy) =>
            {
                enemy.gameObject.SetActive(true);
                float offset = Random.Range(-80, 80);
                enemy.OnReuseObject(offset);
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
