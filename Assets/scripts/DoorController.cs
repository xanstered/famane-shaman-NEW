using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string nextSceneName = "Lvl 1 INDOORS";
    public float interactionDistance = 3.0f;
    public LayerMask doorLayer;

    public AudioClip doorOpenSound;
    private AudioSource audioSource;

    private InventorySystem playerInventory;
    private Camera playerCamera;

    public TextMeshProUGUI promptText;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindAnyObjectByType<InventorySystem>();
        playerCamera = Camera.main;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;

        if (playerInventory == null)
        {
            Debug.LogError("cant find InventorySystem component");
        }

        gameObject.layer = LayerMask.NameToLayer("pickupLayer");

        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDoorInteraction();
    }

    void CheckDoorInteraction()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, doorLayer))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (playerInventory.promptText != null)
                {
                    playerInventory.promptText.text = "locked";
                    playerInventory.promptText.gameObject.SetActive(true);
                }

                CheckKeyUsage();
            }
        }
    }

    void CheckKeyUsage()
    {

    }

    public void UseKeyOnDoor(GameObject keyItem)
    {
        PickupableItem pickupableItem = keyItem.GetComponent<PickupableItem>();


        if (pickupableItem != null && pickupableItem.isKey)
        {
            Debug.Log("using key on door");

            if (doorOpenSound != null)
            {
                AudioSource.PlayClipAtPoint(doorOpenSound, transform.position);
            }

            StartCoroutine(LoadSceneWithDelay());
        }
        else
        {
            Debug.Log("this is not a key");
            if (playerInventory.promptText != null)
            {
                playerInventory.promptText.text = "locked";
            }
        }
    }

    System.Collections.IEnumerator LoadSceneWithDelay()
    {
        // opoznienie 1sek przed przejsciem
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nextSceneName);
    }

}
