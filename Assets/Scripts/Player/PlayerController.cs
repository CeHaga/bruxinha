using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private float speed = 5f;

	[Header("Animations")]
	[SerializeField] private Animator animator;
	[SerializeField] private AnimationClip idle;
	[SerializeField] private AnimationClip flying;
	[SerializeField] private AnimationClip dying;

	private Rigidbody2D rb;
	private Vector2 moveInput;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		// Move player
		rb.velocity = moveInput * speed;
		if (moveInput.x != 0 || moveInput.y != 0)
		{
			animator.Play(flying.name);
			return;
		}
		animator.Play(idle.name);
		moveInput = Vector2.zero;
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
	}
}
