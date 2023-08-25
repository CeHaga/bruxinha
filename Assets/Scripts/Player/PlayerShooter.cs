using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[System.Serializable]
public struct ShootOptions
{
    public BulletScriptable bulletScriptable;
    public int level;
}
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private ShootOptions[] shootOptions;
    [SerializeField] private ShootEvent OnShoot;
    private int level = 1;

    public void LevelUp()
    {
        level++;
    }

    public void ResetLevel()
    {
        level = 1;
    }

    public void OnShootBullet(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("Shoot");
            foreach (ShootOptions shootOption in shootOptions)
            {
                if (shootOption.level <= level)
                {
                    OnShoot.Invoke(shootOption.bulletScriptable, bulletSpawnPoint.transform.position);
                }
            }
        }
    }
}
