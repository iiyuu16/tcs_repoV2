using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormShooting : MonoBehaviour
{
    public survivalSoundSource sfx;
    public Transform player;
    public plyrWorm _plyrWorm;
    public List<GameObject> bulletPrefabs;
    public Transform[] bulletSpawn;
    public Collider bulletRange;
    public float bulletSpeed;
    public float fireRate;
    private float nextFireTime;
    private int bulletIndex = 0;

    void Start()
    {
        nextFireTime = 0f;
    }

    void Update()
    {
        if (Time.time >= nextFireTime && !_plyrWorm.isStunned)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1 / fireRate;
        }

        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1 / fireRate;
        }
    }

    void ShootAtPlayer()
    {
        Vector3 directionToPlayer = (player.position - bulletSpawn[bulletIndex].position).normalized;
        float distanceToPlayer = Vector3.Distance(bulletRange.bounds.center, player.position);

        if (distanceToPlayer <= bulletRange.bounds.extents.x)
        {
            GameObject bulletPrefab = bulletPrefabs[bulletIndex];
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawn[bulletIndex].position, Quaternion.identity);
            Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = directionToPlayer * bulletSpeed;

            bulletIndex = (bulletIndex + 1) % bulletPrefabs.Count;

            Destroy(bulletObj, 10f);

        }
    }
}
