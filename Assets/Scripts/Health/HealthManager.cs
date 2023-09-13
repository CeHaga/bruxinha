using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public UnityEvent OnDeath;
    [SerializeField] private float invincibleTime;
    private float invincibleTimer;
    private bool canTakeDamage;
    private bool isDying;
    [SerializeField] private UpdateLifeCountEvent updateLifeCountEvent;
    [SerializeField] private UnityEvent OnLoseHealth;
    [SerializeField] private GameObject[] colliders;

    [Header("Damage")]
    [SerializeField] private int damage;
    [SerializeField] private bool isFragile;

    [Header("Blinking")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkingInterval;

    private void Start()
    {
        ResetHealth();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
        invincibleTimer = 0;
        isDying = false;
        updateLifeCountEvent?.Invoke(maxHealth);
        foreach (GameObject collider in colliders)
        {
            collider.SetActive(true);
        }
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

    public void CollidedWithTag(Collider2D other)
    {
        if (isDying) return;
        if (!canTakeDamage) return;

        if (isFragile)
        {
            OnDeath.Invoke();
            return;
        }

        int takenDamage = 1;
        TakeDamage(takenDamage);
    }


    public void TakeDamage(int damage)
    {
        // Set current health floor 0
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        Debug.Log($"{gameObject.name} took {damage} damage and has {currentHealth} health left");
        updateLifeCountEvent?.Invoke(currentHealth);
        OnLoseHealth?.Invoke();
        if (currentHealth == 0)
        {
            // Debug.Log($"{gameObject.name} died");
            OnDeath.Invoke();
            isDying = true;
            foreach (GameObject collider in colliders)
            {
                collider.SetActive(false);
            }
            return;
        }
        canTakeDamage = false;
        if (blinkingInterval > 0) StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        do
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkingInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkingInterval);
        }while (!canTakeDamage);
    }

    public void Heal(int healAmount, Action callback = null)
    {
        if (currentHealth == maxHealth)
        {
            callback?.Invoke();
            return;
        }
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        updateLifeCountEvent?.Invoke(currentHealth);
    }
}
