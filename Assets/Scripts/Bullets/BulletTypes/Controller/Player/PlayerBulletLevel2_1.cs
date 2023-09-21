using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletLevel2_1", menuName = "Bullets/Player Bullet L2_1", order = 0)]
public class PlayerBulletLevel2_1 : BulletMovementScriptable
{
    public override Vector2 Move(float t, float speed)
    {
        return new Vector2(3 * speed * t, 10);
    }
}
