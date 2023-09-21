using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBullet_O1", menuName = "Bullets/Bullet O1", order = 0)]
public class EnemyBullet_O1 : BulletMovementScriptable
{
    public override Vector2 Move(float t)
    {
        return new Vector2(-3 * t, 0);
    }
}
