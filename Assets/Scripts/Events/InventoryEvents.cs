using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<ItemSO> onItemAdded;
    public event Action<ItemSO> onItemRemoved;
    public event Action<int> onUpdateSelectedSlot;
    public event Action<int> onInventoryUpdated;
    public event Action<InventoryItem> onItemUsed;

    public void ItemAdded(ItemSO item)
    {
        onItemAdded?.Invoke(item);
    }

    public void ItemRemoved(ItemSO item)
    {
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

    public void ItemUsed(InventoryItem item)
    {
        if (onItemUsed != null)
        {
            onItemUsed(item);
        }
    }
}
