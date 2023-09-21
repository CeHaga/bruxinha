using UnityEngine;

public class B1 : EnemyController
{
    public override EnemyState Move(float t, float speed)
    {
        if (t <= 120)
        {
            Vector2 position = new Vector2(120 - t * speed, -40);
            return new EnemyState(position, flying, true);
        }
        if (t <= 250)
        {
            Vector2 position = new Vector2(-(t * speed - 120), (t * speed - 120) - 40);
            return new EnemyState(position, flying, true);
        }
        return new EnemyState(null, dying, false);
    }
}
