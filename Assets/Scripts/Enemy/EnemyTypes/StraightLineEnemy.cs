using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineEnemy : EnemyController
{
	public override Vector2 Move(float t)
	{
		return new Vector2(120 - t, 0);
	}
}
