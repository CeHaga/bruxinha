using UnityEngine;

public class A2 : EnemyController
{
	public override EnemyState Move(float t, float speed)
	{
		if (t < 250)
		{
			Vector2 position = new Vector2(120 - t * speed, -40);
			return new EnemyState(position, flying, true);
		}
		return new EnemyState(null, dying, false);
	}
}
