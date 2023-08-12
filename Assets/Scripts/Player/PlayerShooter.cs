using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletSpawnPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private BulletScriptable[] bulletScriptables;
    [SerializeField] private ShootEvent OnShoot;
    public void OnShootBullet1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Shoot");
            animator.SetTrigger("Shoot");
            OnShoot.Invoke(bulletScriptables[0], bulletSpawnPoint.transform.position);
        }
    }
}
