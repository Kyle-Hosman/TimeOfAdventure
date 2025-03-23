using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<ItemSO> onItemAdded;
    public event Action<ItemSO> onItemRemoved;
    public event Action<int> onUpdateSelectedSlot;

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
}
