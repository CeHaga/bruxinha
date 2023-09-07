using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletLevel3_2 : BulletController
{
    public override Vector2 Move(float t)
    {
        return new Vector2(t, -t / 2 - 10);
    }
}
