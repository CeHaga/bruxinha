using UnityEngine;

public class A4 : EnemyController
{
	public override EnemyState Move(float t)
	{
		if (t < 250)
		{
			Vector2 position = new Vector2(120 - t, -70);
			return new EnemyState(position, flying, true);
		}
		return new EnemyState(null, dying, false);
	}
}
