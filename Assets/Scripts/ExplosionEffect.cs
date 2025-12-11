using UnityEngine;
using UnityEngine.Audio;

public class ExplosionEffect : MonoBehaviour
{
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Destroy(gameObject, 2f);
    }
}
