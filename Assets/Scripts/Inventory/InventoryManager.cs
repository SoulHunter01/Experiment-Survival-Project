using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] List<Item> Items = new List<Item>();

    [SerializeField] Transform ItemContent;
    [SerializeField] public GameObject inventoryItem;

    private void Awake()
    {
        Instance = this;
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
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item1.itemName;
            itemIcon.sprite = item1.icon;
            Debug.Log(inventoryItem);
        }
    }
}
