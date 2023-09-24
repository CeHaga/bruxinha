using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private float speed = 5f;
	[SerializeField] private AnimationClip dying;
	[SerializeField] private GameOverEvent gameOverEvent;
	[SerializeField] private PlayerInput playerInput;

	private Rigidbody2D rb;
	private Animator animator;
	private Vector2 moveInput;
	private bool isDying;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		isDying = false;
	}

	private void FixedUpdate()
	{
		if (isDying) return;
		// Move player
		rb.velocity = moveInput * speed;
		if (moveInput.x != 0 || moveInput.y != 0)
		{
			//animator.Play(flying.name);
			return;
		}
		//animator.Play(idle.name);
		moveInput = Vector2.zero;
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
	}

	public void OnDeath()
	{
		rb.velocity = Vector2.zero;
		isDying = true;
		playerInput.enabled = false;
		AudioManager.Instance.Play("SFX_PlayerDeath");
		StartCoroutine(PlayDyingAnimation());
	}

	private IEnumerator PlayDyingAnimation()
	{
		animator.Play(dying.name);
		yield return new WaitForSeconds(dying.length + 0.5f);
		Destroy(gameObject);
		gameOverEvent.Invoke();
	}
}
