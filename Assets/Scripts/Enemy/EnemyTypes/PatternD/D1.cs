using UnityEngine;

public class D1 : EnemyController
{
	public override EnemyState Move(float t)
	{
		if (t < 160)
		{
			Vector2 position = new Vector2(120 - t / 4 * 20 / Mathf.PI, 10 * Mathf.Cos(t / 4));
			return new EnemyState(position, flying, true);
		}
		return new EnemyState(null, dying, false);
	}
}
