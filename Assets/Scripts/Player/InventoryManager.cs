using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<ItemSO> inventoryItems = new List<ItemSO>();

    private Dictionary<string, ItemSO> itemMap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        itemMap = CreateItemMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onItemAdded += AddItem;
        GameEventsManager.instance.inventoryEvents.onItemRemoved += RemoveItem;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onItemAdded -= AddItem;
        GameEventsManager.instance.inventoryEvents.onItemRemoved -= RemoveItem;
    }

    private Dictionary<string, ItemSO> CreateItemMap()
    {
        ItemSO[] allItems = Resources.LoadAll<ItemSO>("Items");
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
        ItemSO item = itemMap[id];
        if (item == null)
        {
            Debug.LogError("ID not found in the Item Map: " + id);
        }
        return item;
    }

    public void AddItem(ItemSO item)
    {
        if (item != null)
        {
            inventoryItems.Add(item);
            GameEventsManager.instance.inventoryEvents.ItemAdded(item);
        }
    }

    public void RemoveItem(ItemSO item)
    {
        if (item != null)
        {
            inventoryItems.Remove(item);
            GameEventsManager.instance.inventoryEvents.ItemRemoved(item);
        }
    }

    public void UseItem(ItemSO item)
    {
        switch (item.statToChange)
        {
            case ItemSO.StatToChange.Health:
                GameEventsManager.instance.playerEvents.HealthChanged(item.statChangeAmount);
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
