using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdEnemy : EnemyController
{
	public override Vector2 Move(float t)
	{
		if (t < 225)
			return new Vector2(120 - t / 2, Mathf.Sin(t / 90) * 25);
		if (t < 300)
			return new Vector2(7.5f, 0);
		if (t < 430)
			return new Vector2(7.5f - (t - 300), 0);
		return new Vector2(-1000, -1000);
	}
}
