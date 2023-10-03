using UnityEngine;

public class D2 : EnemyController
{
	[SerializeField] private float height = 10f;
	public override EnemyState Move(float t)
	{
		if (t < 160)
		{
			Vector2 position = new Vector2(120 - t / 4 * 20 / Mathf.PI, 40 + height * Mathf.Cos(t / 4));
			return new EnemyState(position, flying, true);
		}
		return new EnemyState(null, dying, false);
	}
}
