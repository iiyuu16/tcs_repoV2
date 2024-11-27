using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class survivalTurret : MonoBehaviour
{
    public Transform player;
    public List<GameObject> bulletPrefabs;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public int maxHP = 5;
    private int currentHP;
    public Renderer turretSkin;

    public ParticleSystem hitFX;
    public ParticleSystem sparksFX;
    public ParticleSystem flashFX;
    public ParticleSystem fireFX;
    public ParticleSystem smokeFX;

    public float fireRate = 0.5f;
    private float nextFireTime;
    private int bulletIndex = 0;

    public survivalSoundSource sfx;

    void Start()
    {
        currentHP = maxHP;
        nextFireTime = 0f;
        turretSkin.enabled = true;
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + 1 / fireRate;
        }
    }

    void ShootAtPlayer()
    {
        Vector3 directionToPlayer = (player.position - bulletSpawn.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        GameObject bulletPrefab = bulletPrefabs[bulletIndex];
        GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.velocity = directionToPlayer * bulletSpeed;

        // Move to the next bullet type in the list, wrapping around if necessary
        bulletIndex = (bulletIndex + 1) % bulletPrefabs.Count;

        Destroy(bulletObj, 5f);
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
