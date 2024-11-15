using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Transform itemHoldPoint;  // Where the player holds the item
    [SerializeField] private GameObject heldItem;  // Currently held item
    [SerializeField] float pickupRange = 3f;
    [SerializeField] LayerMask pickupLayer;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Standing.Pickup.performed += context => HandlePickup();
    }

    private void OnEnable() => playerInput.Enable();
    private void OnDisable() => playerInput.Disable();

    private void HandlePickup()
    {
        if (heldItem)
        {
            DropItem();
        }
        else
        {
            TryPickupItem();
        }
    }

    private void TryPickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange, pickupLayer))
        {
            GameObject item = hit.collider.gameObject;

            ItemController itemController = item.GetComponent<ItemController>();
            if (itemController != null)
            {
                // Add item to inventory
                InventoryManager.Instance.Add(itemController.Item);

                // Give player the choice to hold it or send it directly to the inventory
                if (heldItem == null)
                {
                    HoldItem(item);
                }
                else
                {
                    item.SetActive(false); // Send to inventory without holding
                    Debug.Log($"{itemController.Item.name} added to inventory.");
                }
            }
            else
            {
                Debug.LogWarning("The object does not have an ItemController component!");
            }
        }
    }

    private void HoldItem(GameObject item)
    {
        heldItem = item;
        heldItem.transform.SetParent(itemHoldPoint);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        Rigidbody itemRigidbody = heldItem.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
            itemRigidbody.isKinematic = true;

        Debug.Log($"Holding {heldItem.name}");
    }

    private void DropItem()
    {
        if (heldItem == null) return;

        Rigidbody itemRigidbody = heldItem.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
            itemRigidbody.isKinematic = false;

        heldItem.transform.SetParent(null);
        heldItem = null;

        Debug.Log("Dropped item.");
    }
}