using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBullet_O4", menuName = "Bullets/Bullet O4", order = 0)]
public class EnemyBullet_O4 : BulletMovementScriptable
{
    public override Vector2 Move(float t)
    {
        return new Vector2(-3 * t, 3 * t);
    }
}
