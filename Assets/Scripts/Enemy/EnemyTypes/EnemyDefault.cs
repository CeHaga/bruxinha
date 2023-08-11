using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnemyDefault", menuName = "Enemy Patterns/EnemyDefault", order = 0)]
public class EnemyDefault : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;

    [Header("Controller")]
    public BulletScriptable[] bulletScriptables;
    public float shootInterval;

    [Header("Animations")]
    public AnimationClip idle;
    public AnimationClip flying;
    public AnimationClip dying;
}
