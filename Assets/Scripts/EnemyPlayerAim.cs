using UnityEngine;

public class EnemyPlayerAim : EnemyBase
{
    private Transform player;
    override protected void Start()
    {
        velocity = Quaternion.Inverse(transform.rotation) * movementDirection * speed;
        audioSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override protected void Shoot()
    {
        BulletBase bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<BulletBase>();
        bullet.direction = (player.position - transform.position).normalized;

        shotCooldown = shootingPeriod;
        audioSource.PlayOneShot(shootSFX);
    }
}
