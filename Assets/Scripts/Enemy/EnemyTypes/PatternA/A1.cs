using UnityEngine;

public class A1 : EnemyController
{
	public override EnemyState Move(float t, float speed)
	{
		if (t < 250)
		{
			Vector2 position = new Vector2(120 - t * speed, 0);
			return new EnemyState(position, flying, true);
		}
		return new EnemyState(null, dying, false);
	}
}
