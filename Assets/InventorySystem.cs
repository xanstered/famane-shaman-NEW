using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] inventorySlots = new GameObject[4];
    private GameObject[] itemsInInventory = new GameObject[4];
    
    public float pickupRange = 3.0f;
    public LayerMask pickupLayer;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupItem();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseOrDropItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseOrDropItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseOrDropItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseOrDropItem(3);
        }
    }

    void TryPickupItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, pickupRange, pickupLayer))
        {
            if (hit.collider.CompareTag("Pickupable"))
            {
                GameObject itemToPickup = hit.collider.gameObject;

                for (int i = 0; i < itemsInInventory.Length; i++)
                {
                    if (itemsInInventory[i] == null)
                    {
                        AddItemToInventory(itemToPickup, i);
                        return;
                    }
                }

                Debug.Log("inventory full");
            }
        }
    }

    void AddItemToInventory(GameObject item, int slotIndex)
    {
        itemsInInventory[slotIndex] = item;

        item.SetActive(false);

        UpdateInventoryUI(slotIndex, item);

        Debug.Log("dodano item do slotu" + (slotIndex + 1));
    }

    void UpdateInventoryUI(int slotIndex, GameObject item)
    {
        PickupableItem pickupableItem = item.GetComponent<PickupableItem>();

        if (pickupableItem != null && pickupableItem.icon != null)
        {
            Image slotImage = inventorySlots[slotIndex].GetComponentInChildren<Image>();
            if (slotImage != null)
            {
                slotImage.sprite = pickupableItem.icon;
                slotImage.enabled = true;
            }
        }
    }

    void UseOrDropItem(int slotIndex)
    {
        if (itemsInInventory[slotIndex] != null)
        {
            GameObject item = itemsInInventory[slotIndex];

            UseOrDropItem(slotIndex);
        }
    }

    void DropItem(int slotIndex)
    {
        if (itemsInInventory[slotIndex] != null)
        {
            GameObject item = itemsInInventory[slotIndex];

            item.SetActive(true);

            item.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 1.5f;

            itemsInInventory[slotIndex] = null;

            ClearInventorySlotUI(slotIndex);

            Debug.Log("upuszczono item ze slotu" + (slotIndex + 1));
        }
    }

    void ClearInventorySlotUI(int slotIndex)
    {
        Image slotImage = inventorySlots[slotIndex].GetComponentInChildren<Image>();
        if (slotImage != null)
        {
            slotImage.sprite = null;
            slotImage.enabled = false;
        }
    }
}
