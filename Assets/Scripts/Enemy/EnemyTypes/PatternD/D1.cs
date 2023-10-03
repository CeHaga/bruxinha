using UnityEngine;

public class D1 : EnemyController
{
	[SerializeField] private float height = 10f;
	public override EnemyState Move(float t)
	{
		if (t < 160)
		{
			Vector2 position = new Vector2(120 - t / 4 * 20 / Mathf.PI, height * Mathf.Cos(t / 4));
			return new EnemyState(position, flying, true);
		}
		return new EnemyState(null, dying, false);
	}
}
