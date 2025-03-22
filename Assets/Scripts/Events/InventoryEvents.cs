using System;
using UnityEngine;

public class InventoryEvents
{
    public event Action<ItemSO> onItemAdded;
    public event Action<ItemSO> onItemRemoved;

    public void ItemAdded(ItemSO item)
    {
        onItemAdded?.Invoke(item);
    }

    public void ItemRemoved(ItemSO item)
    {
        onItemRemoved?.Invoke(item);
    }
}
