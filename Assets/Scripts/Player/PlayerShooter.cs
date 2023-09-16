using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Events;

[System.Serializable]
public struct ShootOptions
{
    public BulletScriptable bulletScriptable;
    public int level;
    public BulletMovementScriptable bulletMovementScriptable;
}
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private int maxLevel;
    [SerializeField] private int bulletCooldown;
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private ShootOptions[] shootOptions;
    [SerializeField] private ShootEvent OnShoot;
    [SerializeField] private UpdateBulletLevelEvent OnUpdateBulletLevel;
    private int level = 1;
    private int bulletCooldownCounter = 0;
    private bool canShoot = true;
    private bool isShooting;

    void Update()
    {
        if (!canShoot)
        {
            bulletCooldownCounter++;
            if (bulletCooldownCounter >= bulletCooldown)
            {
                canShoot = true;
                bulletCooldownCounter = 0;
            }
        }
        if(isShooting){
            Shoot();
        }
    }

    public void LevelUp(Action callback = null)
    {
        if (level == maxLevel)
        {
            callback?.Invoke();
            return;
        }
        level++;
        OnUpdateBulletLevel?.Invoke(level);
    }

    public void ResetLevel()
    {
        level = 1;
        OnUpdateBulletLevel?.Invoke(level);
    }

    public void OnShootBullet(InputAction.CallbackContext context)
    {
        if(context.interaction is HoldInteraction && context.performed)
        {
            isShooting = true;
        }
        if(context.interaction is HoldInteraction && context.canceled)
        {
            isShooting = false;
        }
    }

    public void Shoot(){
        if (!canShoot) return;
        canShoot = false;
        animator.SetTrigger("Shoot");
        foreach (ShootOptions shootOption in shootOptions)
        {
            if (shootOption.level <= level)
            {
                OnShoot.Invoke(shootOption.bulletScriptable, bulletSpawnPoint.transform.position, shootOption.bulletMovementScriptable);
            }
        }
    }
}
