using UnityEngine;

public class StraightLineEnemy_0 : EnemyController
{
	public override EnemyState Move(float t)
	{
		if (t < 240)
		{
			Vector2 position = new Vector2(120 - t, 0);
			return new EnemyState(position, flying, false);
		}
		return new EnemyState(null, dying, false);
	}
}
