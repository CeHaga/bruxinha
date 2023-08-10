using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private float invincibleTime;
    private float invincibleTimer;
    private bool canTakeDamage;

    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] private bool isFragile;

    [Header("Collision")]
    [SerializeField] private string[] collisionTags;

    [Header("Blinking")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkingInterval;

    private void Awake()
    {
        ResetHealth();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
        invincibleTimer = 0;
    }

    private void Update()
    {
        if (canTakeDamage) return;
        invincibleTimer += Time.deltaTime;
        if (invincibleTimer >= invincibleTime)
        {
            canTakeDamage = true;
            invincibleTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage) return;
        if (Array.IndexOf(collisionTags, other.gameObject.tag) == -1) return;

        Debug.Log($"{gameObject.name} hit by {other.gameObject.name}");

        if (isFragile)
        {
            OnDeath.Invoke();
            return;
        }

        HealthManager otherHealth = other.gameObject.GetComponent<HealthManager>();
        if (otherHealth == null)
        {
            Debug.LogError($"No HealthManager found on {other.gameObject.name}");
            return;
        }
        float takenDamage = otherHealth.damage;

        TakeDamage(takenDamage);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} died");
            OnDeath.Invoke();
            return;
        }
        canTakeDamage = false;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (!canTakeDamage)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkingInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkingInterval);
        }
    }
}
