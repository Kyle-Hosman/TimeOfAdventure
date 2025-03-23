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

    public void AddItem(ItemSO item)
    {
        inventoryItems.Add(item);
        // Trigger the event to update the UI
        GameEventsManager.instance.inventoryEvents.ItemAdded(item);
        //inventoryUIManager.UpdateInventoryUI(item);
    }

    public void RemoveItem(ItemSO item)
    {
        inventoryItems.Remove(item);
        // Trigger the event to update the UI
        GameEventsManager.instance.inventoryEvents.ItemRemoved(item);
        //inventoryUIManager.UpdateInventoryUI(item);
    }
}
