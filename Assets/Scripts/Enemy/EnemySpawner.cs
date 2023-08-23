using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public struct EnemySpawnOptions
{
    public bool canSpawn;
    public EnemyPattern enemyPattern;
}
public class EnemySpawner : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private ShootEvent OnShoot;

    [Header("Spawn")]
    [SerializeField] private EnemySpawnOptions[] enemySpawnOptions;
    private EnemySpawnOptions[] validSpawnOptions;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnMultiplierBonus;
    private float spawnMultiplier = 1;
    [SerializeField] private float maxSpawnMultiplier;

    private ObjectPool<EnemyController>[][] enemyPool;

    private void Start()
    {
        validSpawnOptions = enemySpawnOptions.Where(enemySpawnOption => enemySpawnOption.canSpawn).ToArray();

        enemyPool = new ObjectPool<EnemyController>[validSpawnOptions.Length][];
        for (int i = 0; i < validSpawnOptions.Length; i++)
        {
            enemyPool[i] = new ObjectPool<EnemyController>[validSpawnOptions[i].enemyPattern.patternData.Length];
            for (int j = 0; j < validSpawnOptions[i].enemyPattern.patternData.Length; j++)
            {
                Debug.Log("i: " + i + " j: " + j);
                enemyPool[i][j] = createObjectPool(validSpawnOptions[i].enemyPattern.patternData[j], i, j, 1, 5);
            }
        }

        StartCoroutine(SpawnPatterns());
    }

    private ObjectPool<EnemyController> createObjectPool(EnemyPatternSpawn enemyPattern, int patternIndex, int enemyIndex, int size, int maxSize)
    {
        EnemyDefault enemyDefault = enemyPattern.enemyDefault;
        return new ObjectPool<EnemyController>(() =>
        {
            GameObject enemy = Instantiate(enemyDefault.prefab);
            enemy.name = enemyPattern.enemyDefault.enemyName;

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            HealthManager healthManager = enemy.GetComponent<HealthManager>();

            Debug.Log("enemyController: " + enemyController + " healthManager: " + healthManager); ;
            healthManager.OnDeath.AddListener(enemyController.OnPlayerKill);

            enemyController.OnCreateObject(
                (enemy, playerKill) => KillEnemy(enemy, patternIndex, enemyIndex, playerKill),
                (bulletScriptable, position) => OnShoot.Invoke(bulletScriptable, position),
                enemyDefault.bulletScriptables,
                enemyDefault.shootInterval,
                enemyDefault.idle,
                enemyDefault.flying,
                enemyDefault.dying,
                () => healthManager.ResetHealth()
            );

            return enemyController;
        }, (enemy) =>
            {
                enemy.gameObject.SetActive(true);
                enemy.OnReuseObject();
            }, (enemy) =>
            {
                enemy.gameObject.SetActive(false);
            }, (enemy) =>
            {
                Destroy(enemy.gameObject);
            }, false, size, maxSize);
    }

    private IEnumerator SpawnPatterns()
    {
        while (true)
        {
            int patternIndex = Random.Range(0, validSpawnOptions.Length);
            EnemyPattern enemyPattern = validSpawnOptions[patternIndex].enemyPattern;
            float totalTime = 0;
            for (int i = 0; i < enemyPattern.patternData.Length; i++)
            {
                EnemyPatternSpawn enemyPatternSpawn = enemyPattern.patternData[i];

                float waitingTime = enemyPatternSpawn.spawnTime - totalTime;
                yield return new WaitForSeconds(waitingTime / spawnMultiplier);
                totalTime += enemyPatternSpawn.spawnTime;

                enemyPool[patternIndex][i].Get();
            }
            yield return new WaitForSeconds(spawnInterval / spawnMultiplier);
            spawnMultiplier = Mathf.Min(maxSpawnMultiplier, spawnMultiplierBonus + spawnMultiplier);
        }
    }

    private void KillEnemy(EnemyController enemy, int patternIndex, int enemyIndex, bool didPlayerKill = false)
    {
        enemyPool[patternIndex][enemyIndex].Release(enemy);
    }
}
