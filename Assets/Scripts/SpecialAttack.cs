using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour{
    [SerializeField] private float secondsToExplode;

    private GameObject[] enemies;
    private HealthManager healthManager;
    
    private void OnEnable() {
        StartCoroutine(BombTimer());
    }

    private IEnumerator BombTimer(){
        Debug.Log("Contagem");
        yield return new WaitForSeconds(secondsToExplode);
        Debug.Log("BOOM!");
        OnExplosion("Enemy");
        OnExplosion("EnemyBullet");
        this.gameObject.SetActive(false);
    }

    private void OnExplosion(string tag){
        enemies = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject enemy in enemies){
            healthManager = enemy.GetComponent<HealthManager>();
            healthManager.OnDeath.Invoke();
        }
    }
}
