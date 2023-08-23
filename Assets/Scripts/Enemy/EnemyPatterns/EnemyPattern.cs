using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[System.Serializable]
public struct EnemyPatternSpawn
{
    public float spawnTime;
    public EnemyDefault enemyDefault;
}

[CreateAssetMenu(menuName = "Enemy Patterns/Enemy Pattern")]
public class EnemyPattern : ScriptableObject
{
    public EnemyPatternSpawn[] patternData;
}
