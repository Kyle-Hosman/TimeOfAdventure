using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager instance;

    public List<ItemSO> inventoryItems = new List<ItemSO>();
    public InventoryUIManager inventoryUIManager;

    void Awake()
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
    }

    void Start()
    {
        if (inventoryUIManager != null)
        {
            inventoryUIManager.Initialize(this);
        }
        else
        {
            Debug.LogError("InventoryUIManager is not assigned in the Inspector.");
        }

        GameEventsManager.instance.inventoryEvents.onItemAdded += AddItem;
        GameEventsManager.instance.inventoryEvents.onItemRemoved += RemoveItem;
    }

    void OnDestroy()
    {
        GameEventsManager.instance.inventoryEvents.onItemAdded -= AddItem;
        GameEventsManager.instance.inventoryEvents.onItemRemoved -= RemoveItem;
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

    public void AddItem(ItemSO item)
    {
        Debug.Log("Adding item to inventory: " + item.itemName);
        inventoryItems.Add(item);
        Debug.Log("Item added to inventory: " + item.itemName);
        Debug.Log("Current inventory items count: " + inventoryItems.Count);
        foreach (var invItem in inventoryItems)
        {
            Debug.Log("Inventory item: " + invItem.itemName);
        }
        // Update the UI to reflect the new item
        inventoryUIManager.UpdateInventoryUI();
        Debug.Log("Inventory UI updated after adding item: " + item.itemName);
    }

    public void RemoveItem(ItemSO item)
    {
        inventoryItems.Remove(item);
        Debug.Log("Item removed from inventory: " + item.itemName);
        // Update the UI to reflect the removed item
        inventoryUIManager.UpdateInventoryUI();
        Debug.Log("Inventory UI updated after removing item: " + item.itemName);
    }
}
