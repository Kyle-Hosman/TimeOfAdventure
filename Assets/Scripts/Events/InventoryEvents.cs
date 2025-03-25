using System;
using UnityEngine;
using System.Collections.Generic;

public class InventoryEvents
{
    public event Action<ItemSO> onItemAdded;
    public event Action<ItemSO> onItemRemoved;
    public event Action<int> onUpdateSelectedSlot;
    public event Action<int> onInventoryUpdated;
    public event Action<ItemSO> onUseItem;

    public void ItemAdded(ItemSO item)
    {
        Debug.Log("InventoryEvents: ItemAdded called for item: " + item.itemName);
        onItemAdded?.Invoke(item);
    }

    public void ItemRemoved(ItemSO item)
    {
        Debug.Log("InventoryEvents: ItemRemoved called for item: " + item.itemName);
        onItemRemoved?.Invoke(item);
    }

    public void UpdateSelectedSlot(int slotIndex)
    {
        if (onUpdateSelectedSlot != null)
        {
            onUpdateSelectedSlot(slotIndex);
        }
    }

    public void InventoryUpdated(int itemCount)
    {
        if (onInventoryUpdated != null)
        {
            onInventoryUpdated(itemCount);
        }
    }

    public void UseItem(ItemSO item)
    {
        //Debug.Log("InventoryEvents: UseItem called for item: " + (item != null ? item.itemName : "null"));
        if (item != null)
        {
            onUseItem?.Invoke(item);
        }
        else
        {
            Debug.LogError("InventoryEvents: Item is null when trying to use it.");
        }
    }
}
