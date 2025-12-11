using UnityEngine;

public class MultishotEnemy : EnemyBase
{
    Vector3 topDirection = new Vector3(-1, 0.6f, 0).normalized;
    Vector3 bottomDirection = new Vector3(-1, -0.6f, 0).normalized;
    override protected void Shoot()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        BulletBase topBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<BulletBase>();
        BulletBase bottomBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<BulletBase>();
        topBullet.direction = topDirection;
        bottomBullet.direction = bottomDirection;

        shotCooldown = shootingPeriod;
        audioSource.PlayOneShot(shootSFX);
    }
}
