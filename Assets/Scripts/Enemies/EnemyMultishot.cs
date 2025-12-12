using UnityEngine;

public class MultishotEnemy : Enemy
{
    Vector3 topDirection = new Vector3(-1, 0.6f, 0).normalized;
    Vector3 bottomDirection = new Vector3(-1, -0.6f, 0).normalized;
    override protected void Shoot()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Bullet topBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        Bullet bottomBullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        topBullet.direction = topDirection;
        bottomBullet.direction = bottomDirection;

        shotCooldown = shootingPeriod;
        audioSource.PlayOneShot(shootSFX);
    }
}
