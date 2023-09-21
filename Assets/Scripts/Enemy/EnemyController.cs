using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public struct EnemyState
{
	public Vector2? position;
	public AnimationClip animation;
	public bool canShoot;

	public EnemyState(Vector2? position, AnimationClip animation, bool canShoot)
	{
		this.position = position;
		this.animation = animation;
		this.canShoot = canShoot;
	}
}

public abstract class EnemyController : MonoBehaviour
{

	private Action<BulletScriptable, Vector2, BulletMovementScriptable> OnShoot;
	private bool canShoot;
	private BulletScriptable[] bulletScriptables;
	private float shootInterval;
	private BulletMovementScriptable bulletMovementScriptable;

	public Action ResetHealth;
	private Action<EnemyController, bool> onEnemyKilled;

	private Func<bool> onGamePaused;

	private Animator animator;
	private Rigidbody2D rb;
	private Collider2D collider2d;

	private int t;
	private bool isDying;
	private float yOffset;

	protected AnimationClip idle;
	protected AnimationClip flying;
	protected AnimationClip dying;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
		canShoot = false;
	}

	public void OnCreateObject(Action<EnemyController, bool> onEnemyKilled, Action<BulletScriptable, Vector2, BulletMovementScriptable> OnShoot,
		BulletScriptable[] bulletScriptables, float shootInterval, AnimationClip idle, AnimationClip flying, AnimationClip dying, Action ResetHealth,
		Func<bool> onGamePaused, BulletMovementScriptable bulletMovementScriptable)
	{
		this.onEnemyKilled = onEnemyKilled;
		this.OnShoot = OnShoot;
		this.bulletScriptables = bulletScriptables;
		this.shootInterval = shootInterval;
		this.idle = idle;
		this.flying = flying;
		this.dying = dying;
		this.ResetHealth = ResetHealth;
		this.onGamePaused = onGamePaused;
		this.bulletMovementScriptable = bulletMovementScriptable;
	}

	public void OnReuseObject()
	{
		transform.position = new Vector2(256, 256);
		isDying = false;
		t = 0;
		ChangeCollisions(true);
		ResetHealth?.Invoke();
		InvokeRepeating("Shoot", 0.1f, shootInterval);
	}

	private void Update()
	{
		if (onGamePaused()) return;
		if (isDying) return;
		EnemyState state = Move(t);
		t++;
		if (state.position != null)
		{
			Vector2 position = (Vector2)state.position;
			position.y += yOffset;
			position.y *= -1;
			animator.Play(state.animation.name);
			rb.position = position;
			canShoot = state.canShoot;
			return;
		}
		StartCoroutine(PlayDyingAnimation(state.animation));
	}

	private IEnumerator PlayDyingAnimation(AnimationClip animation, bool didPlayerKill = false)
	{
		CancelInvoke("Shoot");
		isDying = true;
		ChangeCollisions(false);
		animator.Play(animation.name);
		yield return new WaitForSeconds(animation.length);
		onEnemyKilled?.Invoke(this, didPlayerKill);
	}

	private void Shoot()
	{
		Debug.Log("canShoot: " + canShoot);
		if (!canShoot) return;
		Debug.Log("bulletScriptables.Length: " + bulletScriptables.Length);
		if (bulletScriptables.Length == 0) return;

		int index = UnityEngine.Random.Range(0, bulletScriptables.Length);
		Debug.Log("index: " + index);
		BulletScriptable bulletScriptable = bulletScriptables[index];
		OnShoot?.Invoke(bulletScriptable, transform.position, bulletMovementScriptable);
	}

	public void OnPlayerKill()
	{
		StartCoroutine(PlayDyingAnimation(dying, true));
	}

	private void ChangeCollisions(bool enabled)
	{
		collider2d.enabled = enabled;
	}

	public abstract EnemyState Move(float t);
}
