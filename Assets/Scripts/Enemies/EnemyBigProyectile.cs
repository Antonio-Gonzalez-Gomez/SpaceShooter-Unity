using UnityEngine;

public class EnemyBigProyectile : Enemy
{
    [SerializeField] float movingTimer = 1f;
    Vector3 maxDirection = new Vector3(-1, 0.3f, 0).normalized;
    Vector3 minDirection = new Vector3(-1, -0.3f, 0).normalized;
    void Update()
    {
        if (movingTimer > 0f)
        {
            transform.Translate(velocity * Time.deltaTime);
            movingTimer -= Time.deltaTime;
        }
        else
        {
            shotCooldown -= Time.deltaTime;
            if (shotCooldown <= 0f && shootEnabled)
            {
                Shoot();
            }
        }
    }
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
