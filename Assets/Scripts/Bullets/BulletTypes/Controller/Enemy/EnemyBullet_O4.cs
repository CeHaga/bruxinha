using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBullet_O4", menuName = "Bullets/Bullet O4", order = 0)]
public class EnemyBullet_O4 : BulletMovementScriptable
{
    private float speed = 2f;
    public override Vector2 Move(float t)
    {
        return new Vector2(-speed / Mathf.Sqrt(2) * t, speed / Mathf.Sqrt(2) * t);
    }
}
