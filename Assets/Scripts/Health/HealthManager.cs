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
    public UnityEvent OnDeath;
    [SerializeField] private float invincibleTime;
    private float invincibleTimer;
    private bool canTakeDamage;
    private bool isDying;

    [Header("Damage")]
    [SerializeField] private float damage;
    [SerializeField] private bool isFragile;

    [Header("Collision")]
    [SerializeField] private string[] collisionTags;

    [Header("Blinking")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkingInterval;

    private void Start()
    {
        ResetHealth();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(float maxHealth, float invincibleTime, float damage, bool isFragile, string[] collisionTags, float blinkingInterval)
    {
        this.maxHealth = maxHealth;
        this.invincibleTime = invincibleTime;
        this.damage = damage;
        this.isFragile = isFragile;
        this.collisionTags = collisionTags;
        this.blinkingInterval = blinkingInterval;
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
        invincibleTimer = 0;
        isDying = false;
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
        if (isDying) return;
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
            isDying = true;
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
