using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushing : MonoBehaviour
{
    public float pushForce = 5f;
    Rigidbody rb;

    public AudioClip pushingSound;

    private AudioSource audioSource;

    private bool isPushing = false;
    private GameObject currentPushableObject = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = pushingSound;
        audioSource.loop = true;
        audioSource.volume = 1f;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (isPushing && currentPushableObject == null)
        {
            StopPushingSound();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            currentPushableObject = collision.gameObject;
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


                if (!isPushing && pushingSound != null)
                {
                    isPushing = true;
                    currentPushableObject = collision.gameObject;
                    audioSource.Play();
                    Debug.Log("Rozpoczêto odtwarzanie dŸwiêku pchania");
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable") && collision.gameObject == currentPushableObject)
        {
            currentPushableObject = null;
            StopPushingSound();
            Debug.Log("Kolizja zakoñczona, zatrzymujê dŸwiêk");
        }
    }

    private void StopPushingSound()
    {
        if (isPushing)
        {
            isPushing = false;
            audioSource.Stop();
            Debug.Log("Zatrzymano dŸwiêk pchania");
        }
    }

}