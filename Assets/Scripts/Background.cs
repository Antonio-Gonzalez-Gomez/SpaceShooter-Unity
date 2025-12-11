using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Background : MonoBehaviour
{
    private Vector3 velocity = new Vector3 (-0.4f, 0, 0);

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        if (transform.position.x <= -8)
            transform.position = new Vector3 (16, 0, 0);
    }
}
