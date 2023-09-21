using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletLevel2_2", menuName = "Bullets/Player Bullet L2_2", order = 0)]
public class PlayerBulletLevel2_2 : BulletMovementScriptable
{
    public override Vector2 Move(float t, float speed)
    {
        return new Vector2(3 * speed * t, -10);
    }
}
