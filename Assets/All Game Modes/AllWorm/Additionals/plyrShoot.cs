using UnityEngine;

public class plyrShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed;
    public float fireRate = 0.5f;
    private float nextFireTime;
    public survivalSoundSource sfx;
    public Rigidbody rb;

    public plyrWorm plyrWorm;

    private void Start()
    {
        nextFireTime = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && plyrWorm.isStunned == false)
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        if (Time.time >= nextFireTime)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            rb = bullet.GetComponent<Rigidbody>();
            if (bullet != null)
            {
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = this.transform.rotation;
                bullet.SetActive(true);
            }
            rb.velocity = (transform.forward * bulletSpeed);
            nextFireTime = Time.time + 1f / fireRate;
            sfx.slashSFX();
        }
    }
}