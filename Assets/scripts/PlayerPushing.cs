using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushing : MonoBehaviour
{
    public float pushForce = 5f;
    Rigidbody rb;

    public AudioClip pushingSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            Rigidbody objectRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (objectRigidbody != null)
            {
                Vector3 pushDirection = collision.gameObject.transform.position - transform.position;
                pushDirection = pushDirection.normalized;

                objectRigidbody.AddForce(pushDirection * pushForce, ForceMode.Force);

                if (pushingSound != null && !audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(pushingSound);
                }
            }
        }
    }
}
