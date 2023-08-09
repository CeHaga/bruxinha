using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBulletsSpawner : MonoBehaviour
{
    [SerializeField] private BulletController[] bulletPrefabs;

    private ObjectPool<BulletController>[] bulletPools;

    private void Awake()
    {
        bulletPools = new ObjectPool<BulletController>[bulletPrefabs.Length];
        for (int i = 0; i < bulletPrefabs.Length; i++)
        {
            bulletPools[i] = createObjectPool(bulletPrefabs[i], i, 10, 20);
        }
    }

    private ObjectPool<BulletController> createObjectPool(BulletController prefab, int bulletType, int size, int maxSize)
    {
        return new ObjectPool<BulletController>(() =>
        {
            var bullet = Instantiate(prefab);
            // bullet.Init(transform.position, new string[] { "Enemy" }, (bullet, healthManager) => BulletHit(bullet, healthManager, bulletType));
            return bullet;
        }, (bullet) =>
        {
            bullet.gameObject.SetActive(true);
            // bullet.Init(transform.position, new string[] { "Enemy" });
        }, (bullet) =>
        {
            bullet.gameObject.SetActive(false);
        }, (bullet) =>
        {
            Destroy(bullet.gameObject);
        }, false, size, maxSize);
    }

    private void BulletHit(BulletController bullet, HealthManager target, int bulletType)
    {
        bulletPools[bulletType].Release(bullet);
        if (target != null)
        {
            Debug.Log($"Bullet of type {bulletPrefabs[bulletType].name} hit {target.name}");
        }
        else
        {
            Debug.Log($"Bullet of type {bulletPrefabs[bulletType].name} missed");
        }
    }

    public void Shoot(BulletController bullet)
    {
        Debug.Log($"Shooting {bullet.name}");
    }
}
