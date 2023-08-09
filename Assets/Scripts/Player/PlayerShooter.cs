using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private BulletScriptable[] bulletScriptables;
    [SerializeField] private ShootEvent OnShoot;
    public void OnShootBullet1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Shoot");
            OnShoot.Invoke(bulletScriptables[0], transform.position);
        }
    }
}
