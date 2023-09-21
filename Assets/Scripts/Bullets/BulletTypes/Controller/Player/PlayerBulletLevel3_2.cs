using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletLevel3_2", menuName = "Bullets/Player Bullet L3_2", order = 0)]
public class PlayerBulletLevel3_2 : BulletMovementScriptable
{
    public override Vector2 Move(float t, float speed)
    {
        return new Vector2(3 * t * speed, -3 * t * speed / 2 - 10);
    }
}
