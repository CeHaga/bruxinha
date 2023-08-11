using UnityEngine;

public class StraightLineEnemy_m40 : EnemyController
{
	public override EnemyState Move(float t)
	{
		if (t < 240)
		{
			Vector2 position = new Vector2(120 - t, -40);
			return new EnemyState(position, flying, false);
		}
		return new EnemyState(null, dying, false);
	}
}
