using UnityEngine;

public class EnemyFastProyectile : EnemyBase
{
    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            BulletBase bullet = collision.gameObject.GetComponent<BulletBase>();
            lifePoints -= bullet.damage;
            Destroy(bullet.gameObject);
            if (lifePoints <= 0)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
                audioSource.PlayOneShot(damageSFX);
            return;
        }

        if (collision.CompareTag("ScreenBorder"))
        {
            velocity.y = -velocity.y;
            transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
        }
    }

    override protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenBorder"))
        {
            if (despawnReady)
                Destroy(gameObject);
            else
                shootEnabled = true;
        }
    }
}
