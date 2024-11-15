using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject
{
    [SerializeField] public int id;
    [SerializeField] public string itemName;
    [SerializeField] public int value;
    [SerializeField] public Sprite icon;
}
