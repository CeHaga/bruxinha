using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		t0 = Time.frameCount;
		isDying = false;
	}

	private void Update()
	{
		if (isDying) return;
		float t = Time.frameCount - t0;
		EnemyState state = Move(t);
		if (state.position != null)
		{
			animator.Play(state.animation.name);
			rb.position = (Vector2)state.position;
			return;
		}
		isDying = true;
		StartCoroutine(PlayDyingAnimation(state.animation));
	}

	private IEnumerator PlayDyingAnimation(AnimationClip animation)
	{
		animator.Play(animation.name);
		yield return new WaitForSeconds(animation.length);
		Destroy(gameObject);
	}

	public abstract EnemyState Move(float t);
}
