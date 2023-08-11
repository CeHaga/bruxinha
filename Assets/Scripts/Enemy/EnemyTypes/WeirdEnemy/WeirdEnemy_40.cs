using UnityEngine;

public class WeirdEnemy_40 : EnemyController
{
	public override EnemyState Move(float t)
	{
		if (t < 225)
		{
			Vector2 position = new Vector2(120 - t / 2, 40 + Mathf.Sin(t / 90 * 2 * Mathf.PI) * 25);
			return new EnemyState(position, flying, true);
		}
		if (t < 300)
		{
			Vector2 position = new Vector2(7.5f, 40);
			return new EnemyState(position, idle, true);
		}
		if (t < 420)
		{
			Vector2 position = new Vector2(7.5f - (t - 300), 40);
			return new EnemyState(position, flying, false);
		}
		return new EnemyState(null, dying, false);
	}
}
