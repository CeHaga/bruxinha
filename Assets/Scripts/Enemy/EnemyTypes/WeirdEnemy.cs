using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdEnemy : EnemyController
{
	[SerializeField] private AnimationClip idle;
	[SerializeField] private AnimationClip flying;
	[SerializeField] private AnimationClip dying;

	public override EnemyState Move(float t)
	{
		if (t < 225)
		{
			Vector2 position = new Vector2(120 - t / 2, Mathf.Sin(t / 90 * 2 * Mathf.PI) * 25);
			return new EnemyState(position, flying);
		}
		if (t < 300)
		{
			Vector2 position = new Vector2(7.5f, 0);
			return new EnemyState(position, idle);
		}
		if (t < 420)
		{
			Vector2 position = new Vector2(7.5f - (t - 300), 0);
			return new EnemyState(position, flying);
		}
		return new EnemyState(null, dying);
	}

	public override AnimationClip GetDyingAnimation()
	{
		return dying;
	}
}
