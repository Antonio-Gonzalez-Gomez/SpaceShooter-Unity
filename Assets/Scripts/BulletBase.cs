using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] public int damage = 1;
    [SerializeField] public Vector2 direction = Vector2.left;
    protected Vector3 velocity;
    void Start()
    {
        velocity = Quaternion.Inverse(transform.rotation) * direction * speed;
    }
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenBorder"))
        {
            Destroy(gameObject);
        }
    }
}
