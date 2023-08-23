using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour{
    [SerializeField] private float secondsToExplode;

    private GameObject[] enemies;
    private HealthManager healthManager;
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private int t0;
    
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        t0 = Time.frameCount;
        startPosition = transform.position;
        StartCoroutine(BombTimer());
    }

    private void Update() {
        float t = Time.frameCount - t0;
        Debug.Log(t0 + " | " + t);
        rb.position = new Vector2(t, -1*((t/10)*(t/10) - t)) + startPosition;
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
