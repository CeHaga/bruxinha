using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletLevel1_1", menuName = "Bullets/Player Bullet L1", order = 0)]
public class PlayerBulletLevel1_1 : BulletMovementScriptable
{
    public override Vector2 Move(float t, float speed)
    {
        return new Vector2(3 * speed * t, 0);
    }
}
