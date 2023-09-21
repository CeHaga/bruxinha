using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBullet_O5", menuName = "Bullets/Bullet O5", order = 0)]
public class EnemyBullet_O5 : BulletMovementScriptable
{
    public override Vector2 Move(float t, float speed)
    {
        return new Vector2(-3 * t * speed, -3 * t * speed);
    }
}
