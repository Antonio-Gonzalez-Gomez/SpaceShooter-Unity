using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int lifePoints = 2;
    [Header("Movement")]
    [SerializeField] protected float speed = 0.5f;
    [SerializeField] public Vector3 movementDirection = Vector3.up;
    [Header("Shooting")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float shootingPeriod = 2f;
    [Header("Effects")]
    [SerializeField] protected AudioClip shootSFX;
    [SerializeField] protected AudioClip damageSFX;
    [SerializeField] protected GameObject destroyEffect;

    protected AudioSource audioSource;
    protected bool shootEnabled = false;
    protected bool despawnReady = false;
    protected float shotCooldown = 0f;
    protected Vector3 velocity;

    virtual protected void Start()
    {
        velocity = Quaternion.Inverse(transform.rotation) * movementDirection * speed;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    virtual protected void Shoot()
    {
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        shotCooldown = shootingPeriod;
        audioSource.PlayOneShot(shootSFX);
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        shotCooldown -= Time.deltaTime;
        if (shotCooldown <= 0f && shootEnabled)
        {
            Shoot();
        }
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
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
            shootEnabled = false;
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenBorder"))
        {
            if (despawnReady)
                Destroy(gameObject);
            else
            {
                shootEnabled = true;
                despawnReady = true;
            }
        }
    }
}
