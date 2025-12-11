using UnityEngine;

public class EnemyRandomAim : EnemyBase
{
    Vector3 maxDirection = new Vector3(-1, 0.6f, 0).normalized;
    Vector3 minDirection = new Vector3(-1, -0.6f, 0).normalized;
    override protected void Shoot()
    {
        float t = Random.Range(0f, 1f);
        Vector3 direction = Vector3.Lerp(maxDirection, minDirection, t);
        BulletBase bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<BulletBase>();
        bullet.direction = direction;

        shotCooldown = shootingPeriod;
        audioSource.PlayOneShot(shootSFX);
    }
}