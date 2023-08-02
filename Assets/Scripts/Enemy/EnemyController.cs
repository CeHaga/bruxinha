using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
	[SerializeField] private float speed = 0.5f;
	
	protected Rigidbody2D rb;
	private float t;
	private int t0;
	
	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		t = 0;
		t0 = Time.frameCount;
	}
	
	// private void Start() {
	// 	StartCoroutine(MoveCoroutine());
	// }
	
	private void Update() {
		// t += Time.deltaTime;
		t = Time.frameCount - t0;
		Vector2 movement = Move(t);
		rb.position = movement;
	}
	
	private IEnumerator MoveCoroutine() {
		while(true)
		{
			t++;
			Vector2 movement = Move(t);
			rb.position = movement;
			yield return null;
		}
	}
	
	public abstract Vector2 Move(float t);
}
