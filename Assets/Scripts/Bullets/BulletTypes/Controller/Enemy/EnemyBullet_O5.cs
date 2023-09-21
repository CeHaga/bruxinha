using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBullet_O5", menuName = "Bullets/Bullet O5", order = 0)]
public class EnemyBullet_O5 : BulletMovementScriptable
{
    public override Vector2 Move(float t)
    {
        return new Vector2(-3 * t, -3 * t);
    }
}
