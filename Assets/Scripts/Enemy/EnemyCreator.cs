using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public void OnCreateEnemy(GameObject enemy)
    {
        Debug.Log($"{enemy.name} created");
    }

    public void OnResetEnemy(GameObject enemy)
    {
        Debug.Log($"{enemy.name} reset");
    }
    public void OnDeath(GameObject enemy)
    {
        Debug.Log($"{enemy.name} died");
    }
}
