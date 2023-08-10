using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public struct EnemyState
{
	public Vector2? position;
	public AnimationClip animation;

	public EnemyState(Vector2? position, AnimationClip animation)
	{
		this.position = position;
		this.animation = animation;
	}
}

public abstract class EnemyController : MonoBehaviour
{
	public UnityEvent ResetHealth;
	private Animator animator;
	private Rigidbody2D rb;
	private Collider2D collider2d;
	private int t0;
	private bool isDying;
	private float yOffset;
	private Action<EnemyController, bool> onEnemyKilled;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
	}

	public void OnCreateObject(Action<EnemyController, bool> onEnemyKilled)
	{
		this.onEnemyKilled = onEnemyKilled;
	}

	public void OnReuseObject(float yOffset)
	{
		this.yOffset = yOffset;
		transform.position = new Vector2(256, 256);
		isDying = false;
		t0 = Time.frameCount;
		ChangeCollisions(true);
		ResetHealth?.Invoke();
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
			return;
		}
		StartCoroutine(PlayDyingAnimation(state.animation));
	}

	private IEnumerator PlayDyingAnimation(AnimationClip animation, bool didPlayerKill = false)
	{
		isDying = true;
		ChangeCollisions(false);
		animator.Play(animation.name);
		yield return new WaitForSeconds(animation.length);
		onEnemyKilled?.Invoke(this, didPlayerKill);
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
