using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class BulletController : MonoBehaviour
{
    public UnityEvent ResetHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private int t;
    private Vector2 startPosition;
    private Action<BulletController> onBulletHit;

    private Func<bool> onGamePaused;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCreateObject(Action<BulletController> onBulletHit, Func<bool> onGamePaused)
    {
        this.onBulletHit = onBulletHit;
        this.onGamePaused = onGamePaused;
    }

    public void OnReuseObject(Vector2 startPosition)
    {
        //t0 = Time.frameCount;
        t = 0;
        this.startPosition = startPosition;
        transform.position = startPosition;
        ResetHealth?.Invoke();
    }

    private void Update()
    {
        if(onGamePaused()) return;
        //float t = Time.frameCount - t0;
        Vector2 position = Move(t) + startPosition;
        t++;
        if (position.x > 120 || position.x < -120 || position.y > 80 || position.y < -80)
        {
            onBulletHit(this);
        }
        rb.position = position;
    }

    public void OnBulletHit()
    {
        onBulletHit(this);
    }

    public abstract Vector2 Move(float t);
}
