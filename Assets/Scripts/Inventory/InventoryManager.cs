using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] List<Item> Items = new();

    [SerializeField] Transform ItemContent;
    [SerializeField] private GameObject inventoryItem;
    [SerializeField] private Text text;
    [SerializeField] private Image img;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        ///VerifyPickup();
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        Debug.Log("In here");
        //Debug.Log(Items[0].itemName);
        foreach (var item1 in Items)
        {
            Debug.Log(inventoryItem);
            GameObject obj = Instantiate(inventoryItem, ItemContent);

            //var itemName = obj.transform.Find("itemName").GetComponent<Text>();
            //var itemIcon = obj.transform.Find("icon").GetComponent<Image>();

            //itemName.text = item1.itemName;
            //itemIcon.sprite = item1.icon;
            var itemNameObj = obj.transform.Find("ItemName");
            var itemIconObj = obj.transform.Find("ItemIcon");

            if (itemNameObj == null || itemIconObj == null)
            {
                Debug.LogError("Prefab structure is incorrect. Ensure ItemName and ItemIcon exist.");
                return;
            }

            var itemName = itemNameObj.GetComponent<Text>();
            var itemIcon = itemIconObj.GetComponent<Image>();

            if (itemName == null || itemIcon == null)
            {
                Debug.LogError("ItemName or ItemIcon does not have the required components.");
                return;
            }

            Debug.Log(inventoryItem);
        }
    }
    public void VerifyPickup()
    {
        // Access the Pickup script
        Pickup pickup = FindObjectOfType<Pickup>();

        if (pickup == null)
        {
            Debug.LogError("No Pickup instance found in the scene.");
            return;
        }

        // Check if x is true
        if (pickup.x)
        {
            Debug.Log("Pickup is holding an item. Verifying...");

            // Access the heldItem directly from Pickup
            GameObject heldItem = pickup.heldItem;

            if (heldItem == null)
            {
                Debug.LogError("Held item is null.");
                return;
            }

            // Access the ItemController on the held item
            ItemController itemController = heldItem.GetComponent<ItemController>();
            if (itemController == null || itemController.Item == null)
            {
                Debug.LogError("Held item does not have a valid ItemController or Item.");
                return;
            }

            // Verify ID
            if (pickup.id == itemController.Item.id)
            {
                Debug.Log($"IDs match! Pickup ID: {pickup.id}, Item ID: {itemController.Item.id}");
                img.sprite = itemController.Item.icon;
                text.text = itemController.Item.name;
            }
            else
            {
                Debug.LogWarning($"IDs do not match. Pickup ID: {pickup.id}, Item ID: {itemController.Item.id}");
            }
        }
        else
        {
            Debug.Log("Pickup is not holding any item.");
        }
    }
}
