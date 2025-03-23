using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager instance;

    public List<ItemSO> inventoryItems = new List<ItemSO>();
    public InventoryUI inventoryUI;

    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private GameObject inventorySlotPrefab;

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

    void OnEnable()
    {
        if (inventoryUI != null)
        {
            inventoryUI.Initialize(this);
        }
        else
        {
            Debug.LogError("InventoryUI is not assigned in the Inspector.");
        }

        GameEventsManager.instance.inventoryEvents.onItemAdded += AddItem;
        GameEventsManager.instance.inventoryEvents.onItemRemoved += RemoveItem;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += ToggleInventory;

        if (inventorySlotParent == null)
        {
            Debug.LogError("Inventory Slot Parent is not assigned in the Inspector.");
        }

        // Initial update to populate the inventory with pre-existing items
        UpdateInventoryUI();
    }

    void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onItemAdded -= AddItem;
        GameEventsManager.instance.inventoryEvents.onItemRemoved -= RemoveItem;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= ToggleInventory;
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
        UpdateInventoryUI();
        Debug.Log("Inventory UI updated after adding item: " + item.itemName);
    }

    public void RemoveItem(ItemSO item)
    {
        inventoryItems.Remove(item);
        Debug.Log("Item removed from inventory: " + item.itemName);
        // Update the UI to reflect the removed item
        UpdateInventoryUI();
        Debug.Log("Inventory UI updated after removing item: " + item.itemName);
    }

    public void ToggleInventory()
    {
        inventoryUI.ToggleInventory();
    }

    public void UpdateInventoryUI(ItemSO item = null)
    {
        if (inventorySlotParent == null)
        {
            Debug.LogError("Inventory Slot Parent is not assigned.");
            return;
        }

        foreach (Transform child in inventorySlotParent)
        {
            Destroy(child.gameObject);
        }

        if (inventoryItems == null)
        {
            Debug.LogError("Inventory items list is null.");
            return;
        }

        foreach (var inventoryItem in inventoryItems)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotParent);
            slot.SetActive(true); // Ensure the slot is enabled
            Image itemImage = slot.transform.Find("Item").GetComponent<Image>();
            if (itemImage != null)
            {
                if (inventoryItem.itemIcon != null)
                {
                    itemImage.sprite = inventoryItem.itemIcon;
                }
                else
                {
                    Debug.LogError("Item icon is null for item: " + inventoryItem.itemName);
                }
            }
            else
            {
                Debug.LogError("Image component not found in inventory slot prefab.");
            }

            // Ensure the InventoryItem script is added to the slot
            InventoryItem inventoryItemComponent = slot.GetComponent<InventoryItem>();
            if (inventoryItemComponent == null)
            {
                inventoryItemComponent = slot.AddComponent<InventoryItem>();
            }
        }

        GameEventsManager.instance.inventoryEvents.InventoryUpdated(inventoryItems.Count);
    }
}
