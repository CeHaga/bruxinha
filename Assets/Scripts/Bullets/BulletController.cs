using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class BulletController : MonoBehaviour
{
    public UnityEvent ResetHealth;
    private Animator animator;
    private Rigidbody2D rb;
    private int t;
    private Vector2 startPosition;
    private Action<BulletController> onBulletHit;
    private BulletMovementScriptable bulletMovementScriptable;
    private float speed;

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

    public void OnReuseObject(Vector2 startPosition, BulletMovementScriptable bulletMovementScriptable, float speed)
    {
        t = 0;
        this.startPosition = startPosition;
        transform.position = startPosition;
        this.bulletMovementScriptable = bulletMovementScriptable;
        this.speed = speed;
        ResetHealth?.Invoke();
    }

    private void Update()
    {
        if (onGamePaused()) return;
        Vector2 position = bulletMovementScriptable.Move(t, speed) + startPosition;
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
}
