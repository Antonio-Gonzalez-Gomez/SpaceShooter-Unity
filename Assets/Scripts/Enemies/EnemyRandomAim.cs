using UnityEngine;

public class EnemyRandomAim : Enemy
{
    Vector3 maxDirection = new Vector3(-1, 0.6f, 0).normalized;
    Vector3 minDirection = new Vector3(-1, -0.6f, 0).normalized;
    override protected void Shoot()
    {
        float t = Random.Range(0f, 1f);
        Vector3 direction = Vector3.Lerp(maxDirection, minDirection, t);
        Bullet bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.direction = direction;

        shotCooldown = shootingPeriod;
        audioSource.PlayOneShot(shootSFX);
    }
}