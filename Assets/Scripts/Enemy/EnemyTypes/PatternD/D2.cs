using UnityEngine;

public class D2 : EnemyController
{
	public override EnemyState Move(float t, float speed)
	{
		if (t < 160)
		{
			Vector2 position = new Vector2(120 - t * speed / 4 * 20 / Mathf.PI, 40 + 10 * Mathf.Cos(t * speed / 4));
			return new EnemyState(position, flying, true);
		}
		return new EnemyState(null, dying, false);
	}
}
