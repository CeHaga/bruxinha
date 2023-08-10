using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineEnemy : EnemyController
{
	[SerializeField] private AnimationClip flying;
	[SerializeField] private AnimationClip dying;

	public override EnemyState Move(float t)
	{
		if (t < 240)
		{
			Vector2 position = new Vector2(120 - t, 0);
			return new EnemyState(position, flying);
		}
		return new EnemyState(null, dying);
	}

	public override AnimationClip GetDyingAnimation()
	{
		return dying;
	}
}
