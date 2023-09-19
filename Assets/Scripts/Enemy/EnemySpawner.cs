using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

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
    private float spawnTime;

    [Header("Items")]
    [SerializeField] private ItemSpawnEvent OnItemSpawn;

    [Header("Score")]
    [SerializeField] private UpdateScoreCountEvent OnScoreGained;

    private ObjectPool<EnemyController>[][] enemyPool;

    [Header("Debug")]
    [SerializeField] private bool isGamePaused;


    private int frameCount = -1;
    private bool newPattern = true;
    private int currentEnemyPatternIndex;
    private EnemyPattern currentEnemyPattern;
    private int totalEnemies;
    private int currentEnemy;
    private float waitingTime;
    private bool isBetweenPatterns;

    private void Start()
    {
        validSpawnOptions = enemySpawnOptions.Where(enemySpawnOption => enemySpawnOption.canSpawn).ToArray();

        enemyPool = new ObjectPool<EnemyController>[validSpawnOptions.Length][];
        for (int i = 0; i < validSpawnOptions.Length; i++)
        {
            enemyPool[i] = new ObjectPool<EnemyController>[validSpawnOptions[i].enemyPattern.patternData.Length];
            for (int j = 0; j < validSpawnOptions[i].enemyPattern.patternData.Length; j++)
            {
                enemyPool[i][j] = createObjectPool(validSpawnOptions[i].enemyPattern.patternData[j], i, j, 1, 5);
            }
        }

        waitingTime = spawnInterval / spawnMultiplier;
        isBetweenPatterns = true;
    }

    private void Update()
    {
        if (isGamePaused) return;

        frameCount++;

        if (isBetweenPatterns && frameCount < waitingTime)
        {
            return;
        }

        if (isBetweenPatterns)
        {
            isBetweenPatterns = false;
            newPattern = true;
            frameCount = 0;
        }

        if (newPattern)
        {
            newPattern = false;
            currentEnemyPatternIndex = Random.Range(0, validSpawnOptions.Length);
            currentEnemyPattern = validSpawnOptions[currentEnemyPatternIndex].enemyPattern;
            totalEnemies = currentEnemyPattern.patternData.Length;
            currentEnemy = 0;
        }

        for (; currentEnemy < totalEnemies; currentEnemy++)
        {
            EnemyPatternSpawn enemyPatternSpawn = currentEnemyPattern.patternData[currentEnemy];
            if (enemyPatternSpawn.spawnTime / spawnMultiplier > frameCount) break;

            enemyPool[currentEnemyPatternIndex][currentEnemy].Get();
        }

        if (currentEnemy < totalEnemies)
        {
            return;
        }

        isBetweenPatterns = true;
        frameCount = -1;
        waitingTime = spawnInterval / spawnMultiplier;
        spawnMultiplier = Mathf.Min(maxSpawnMultiplier, spawnMultiplierBonus + spawnMultiplier);
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

            healthManager.OnDeath.AddListener(enemyController.OnPlayerKill);

            enemyController.OnCreateObject(
                (enemy, playerKill) => KillEnemy(enemy, patternIndex, enemyIndex, enemyDefault, playerKill),
                (bulletScriptable, position, bulletMovementScriptable) => OnShoot.Invoke(bulletScriptable, position, bulletMovementScriptable),
                enemyDefault.bulletScriptables,
                enemyDefault.shootInterval,
                enemyDefault.idle,
                enemyDefault.flying,
                enemyDefault.dying,
                () => healthManager.ResetHealth(),
                () => isGamePaused,
                enemyDefault.bulletMovementScriptable
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

    private void KillEnemy(EnemyController enemy, int patternIndex, int enemyIndex, EnemyDefault enemyDefault, bool didPlayerKill = false)
    {
        enemyPool[patternIndex][enemyIndex].Release(enemy);

        if (!didPlayerKill) return;
        OnScoreGained?.Invoke(enemyDefault.score);
        OnItemSpawn?.Invoke(enemy.transform.position);
    }

    public void PauseEnemies(bool isGamePaused)
    {
        this.isGamePaused = isGamePaused;
    }
}
