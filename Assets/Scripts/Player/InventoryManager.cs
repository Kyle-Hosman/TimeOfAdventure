using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private InventorySO inventorySO;

    private Dictionary<string, ItemSO> itemMap;

    private void Awake()
    {
        itemMap = CreateItemMap();
    }

    private void OnEnable()
    {
        if (GameEventsManager.instance == null)
        {
            Debug.LogError("GameEventsManager instance is null.");
            return;
        }

        if (GameEventsManager.instance.inventoryEvents == null)
        {
            Debug.LogError("GameEventsManager inventoryEvents is null.");
            return;
        }

        GameEventsManager.instance.inventoryEvents.onItemAdded += HandleItemAdded;
        GameEventsManager.instance.inventoryEvents.onItemRemoved += HandleItemRemoved;
        GameEventsManager.instance.inventoryEvents.onUseItem += HandleUseItem;
   
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance == null)
        {
            Debug.LogError("GameEventsManager instance is null.");
            return;
        }

        if (GameEventsManager.instance.inventoryEvents == null)
        {
            Debug.LogError("GameEventsManager inventoryEvents is null.");
            return;
        }

        GameEventsManager.instance.inventoryEvents.onItemAdded -= HandleItemAdded;
        GameEventsManager.instance.inventoryEvents.onItemRemoved -= HandleItemRemoved;
        GameEventsManager.instance.inventoryEvents.onUseItem -= HandleUseItem;
    }


    private Dictionary<string, ItemSO> CreateItemMap()
    {
        //ItemSO[] allItems = Resources.LoadAll<ItemSO>("Items");
        ItemSO[] allItems = inventorySO.inventoryItems.ToArray();
        Dictionary<string, ItemSO> idToItemMap = new Dictionary<string, ItemSO>();
        foreach (ItemSO item in allItems)
        {
            if (idToItemMap.ContainsKey(item.id))
            {
                Debug.LogWarning("Duplicate ID found when creating item map: " + item.id);
            }
            idToItemMap.Add(item.id, item);
        }
        return idToItemMap;
    }

    private ItemSO GetItemById(string id)
    {
        if (itemMap.TryGetValue(id, out ItemSO item))
        {
            return item;
        }
        else
        {
            Debug.LogError("ID not found in the Item Map: " + id);
            return null;
        }
    }

    public void AddItem(ItemSO item)
    {
        if (item != null)
        {
            Debug.Log("Adding item: " + item.itemName);
            inventorySO.inventoryItems.Add(item);
        }
        else
        {
            Debug.LogError("Attempted to add a null item.");
        }
    }

    public void RemoveItem(ItemSO item)
    {
        if (item != null && inventorySO.inventoryItems.Contains(item))
        {
            Debug.Log("Removing item: " + item.itemName);
            inventorySO.inventoryItems.Remove(item);
            GameEventsManager.instance.inventoryEvents.ItemRemoved(item);
        }
        else
        {
            Debug.LogError("Attempted to remove an item that is null or not in the inventory.");
        }
    }

    private void HandleItemAdded(ItemSO item)
    {
        AddItem(item);
    }

    private void HandleItemRemoved(ItemSO item)
    {
        RemoveItem(item);
    }

    private void HandleUseItem(ItemSO item)
    {
        //Debug.Log("HandleUseItem called for item: " + (item != null ? item.itemName : "null"));
        if (item != null)
        {
            UseItem(item);
            Debug.Log("Used and removed item: " + item.itemName);
            HandleItemRemoved(item);

        }
        else
        {
            Debug.LogError("HandleUseItem: Item is null.");
        }
    }

    public List<ItemSO> GetInventoryItems()
    {
        return new List<ItemSO>(inventorySO.inventoryItems);
    }

    public void UseItem(ItemSO item)
    {
        switch (item.statToChange)
        {
            case ItemSO.StatToChange.Health:
                GameEventsManager.instance.playerEvents.HealthChanged(item.statChangeAmount);
                Debug.Log("Health changed by: " + item.statChangeAmount);
                break;
            case ItemSO.StatToChange.Mana:
                // Implement mana change logic
                break;
            case ItemSO.StatToChange.Stamina:
                // Implement stamina change logic
                break;
            case ItemSO.StatToChange.Strength:
                // Implement strength change logic
                break;
            case ItemSO.StatToChange.Agility:
                // Implement agility change logic
                break;
            case ItemSO.StatToChange.Intelligence:
                // Implement intelligence change logic
                break;
            case ItemSO.StatToChange.Defense:
                // Implement defense change logic
                break;
            default:
                Debug.LogWarning("Unknown stat to change: " + item.statToChange);
                break;
        }
    }
}