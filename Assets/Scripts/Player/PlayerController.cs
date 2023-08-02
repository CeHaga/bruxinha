using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	
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
	}
	
	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
	}
}
