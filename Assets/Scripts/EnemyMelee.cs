using UnityEngine;

public class EnemyMelee : EnemyBase
{
    private Transform player;
    private Vector3 cosmeticRotation = new Vector3 (0, 0, 180);
    override protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        movementDirection = (player.position - transform.position).normalized;
        velocity = Quaternion.Inverse(transform.rotation) * movementDirection * speed;
        transform.Translate(velocity * Time.deltaTime);
        transform.Rotate(cosmeticRotation * Time.deltaTime);
    }
}
