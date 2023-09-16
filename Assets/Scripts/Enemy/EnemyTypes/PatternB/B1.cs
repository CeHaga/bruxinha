using UnityEngine;

public class B1 : EnemyController
{
    public override EnemyState Move(float t)
    {
        if (t <= 120)
        {
            Vector2 position = new Vector2(120 - t, -40);
            return new EnemyState(position, flying, true);
        }
        if (t <= 250)
        {
            Vector2 position = new Vector2(-(t - 120), (t - 120) - 40);
            return new EnemyState(position, flying, true);
        }
        return new EnemyState(null, dying, false);
    }
}
