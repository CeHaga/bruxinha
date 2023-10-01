using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBullet_O1", menuName = "Bullets/Bullet O1", order = 0)]
public class EnemyBullet_O1 : BulletMovementScriptable
{
    private float speed = 2f;
    public override Vector2 Move(float t)
    {
        return new Vector2(-speed * t, 0);
    }
}
