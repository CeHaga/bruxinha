using UnityEngine;

public abstract class BulletMovementScriptable : ScriptableObject
{
    public abstract Vector2 Move(float t, float speed);
}
