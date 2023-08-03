using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	private Animator animator;
	private Rigidbody2D rb;
	private int t0;
	private bool isDying;
	private float yOffset;
	private Action<EnemyController> onEnemyKilled;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	public void Init(float yOffset, Action<EnemyController> onEnemyKilled = null)
	{
		this.yOffset = yOffset;
		this.onEnemyKilled = onEnemyKilled ?? this.onEnemyKilled;
		transform.position = new Vector2(256, 256);
		isDying = false;
		t0 = Time.frameCount;
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
		isDying = true;
		StartCoroutine(PlayDyingAnimation(state.animation));
	}

	private IEnumerator PlayDyingAnimation(AnimationClip animation)
	{
		animator.Play(animation.name);
		yield return new WaitForSeconds(animation.length);
		onEnemyKilled?.Invoke(this);
	}

	public abstract EnemyState Move(float t);
}
