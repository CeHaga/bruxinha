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

	[Header("Shooting")]
	private Action<BulletScriptable, Vector2> OnShoot;
	private bool canShoot;
	[SerializeField] private BulletScriptable[] bulletScriptables;
	[SerializeField] private float shootInterval;

	[Header("Health")]
	public UnityEvent ResetHealth;
	private Action<EnemyController, bool> onEnemyKilled;

	private Animator animator;
	private Rigidbody2D rb;
	private Collider2D collider2d;

	private int t0;
	private bool isDying;
	private float yOffset;


	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
		canShoot = false;
	}

	public void OnCreateObject(Action<EnemyController, bool> onEnemyKilled, Action<BulletScriptable, Vector2> OnShoot)
	{
		this.onEnemyKilled = onEnemyKilled;
		this.OnShoot = OnShoot;
	}

	public void OnReuseObject(float yOffset)
	{
		this.yOffset = yOffset;
		transform.position = new Vector2(256, 256);
		isDying = false;
		t0 = Time.frameCount;
		ChangeCollisions(true);
		ResetHealth?.Invoke();
		InvokeRepeating("Shoot", 0, shootInterval);
	}

	private void Update()
	{
		if (isDying) return;
		float t = Time.frameCount - t0;
		EnemyState state = Move(t);
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
		if (!canShoot) return;
		if (bulletScriptables.Length == 0) return;

		int index = UnityEngine.Random.Range(0, bulletScriptables.Length);
		BulletScriptable bulletScriptable = bulletScriptables[index];
		OnShoot?.Invoke(bulletScriptable, transform.position);
	}

	public void OnPlayerKill()
	{
		StartCoroutine(PlayDyingAnimation(GetDyingAnimation(), true));
	}

	private void ChangeCollisions(bool enabled)
	{
		collider2d.enabled = enabled;
	}

	public abstract EnemyState Move(float t);
	public abstract AnimationClip GetDyingAnimation();
}
