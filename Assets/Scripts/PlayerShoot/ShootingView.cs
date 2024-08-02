using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingView : MonoBehaviour
{
    public ObjectPool objectPool;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private void Start()
    {
        objectPool = GameObject.Find("Player").GetComponent<ObjectPool>();
    }
    public void ShootBullet(Vector2 direction, float speed)
    {
        objectPool.InstantiateWithPool(firePoint.position, firePoint.rotation, bulletPrefab.transform.localScale);
    }
}
