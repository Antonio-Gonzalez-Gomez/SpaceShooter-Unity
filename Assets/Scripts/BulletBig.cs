using UnityEngine;

public class BulletBig : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ScreenBorder"))
        {
            Destroy(gameObject);
        }
    }
}
