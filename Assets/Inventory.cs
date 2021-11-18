using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public List<string> items;

    private void Start()
    {
        items = new List<string>(new string[16]);
        var itemSlots = GetComponentsInChildren<ItemSlot>();
        for (var i = 0; i < itemSlots.Length; i++)
        {
            var slot = itemSlots[i];
            slot.index = i;
            slot.onAddItem = OnAddItem;
            slot.onRemoveItem = OnRemoveItem;
        }
    }

    void OnAddItem(int index, string itemName)
    {
        items[index] = itemName;
    }

    void OnRemoveItem(int index)
    {
        items[index] = string.Empty;
    }
}