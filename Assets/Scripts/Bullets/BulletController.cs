using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BulletController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private int t0;
    private Vector2 startPosition;
    private Action<BulletController, HealthManager> onBulletHit;
    [SerializeField] private string[] collisionTags;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCreateObject(Action<BulletController, HealthManager> onBulletHit)
    {
        this.onBulletHit = onBulletHit;
    }

    public void OnReuseObject(Vector2 startPosition)
    {
        t0 = Time.frameCount;
        this.startPosition = startPosition;
        transform.position = startPosition;
    }

    private void Update()
    {
        float t = Time.frameCount - t0;
        Vector2 position = Move(t) + startPosition;
        if (position.x > 120 || position.x < -120 || position.y > 80 || position.y < -80)
        {
            onBulletHit(this, null);
        }
        rb.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Array.IndexOf(collisionTags, other.gameObject.tag) != -1)
        {
            onBulletHit(this, other.gameObject.GetComponent<HealthManager>());
        }
    }

    public abstract Vector2 Move(float t);
}
