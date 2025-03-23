using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private GameObject inventorySlotPrefab;

    private PlayerInventoryManager playerInventoryManager;
    private GameObject inventoryPanel;

    void Start()
    {
        inventoryPanel = gameObject;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += ToggleInventory;

        // Ensure the inventory panel is initially hidden
        inventoryPanel.SetActive(false);

        if (inventorySlotParent == null)
        {
            Debug.LogError("Inventory Slot Parent is not assigned in the Inspector.");
        }

        if (playerInventoryManager == null)
        {
            Debug.LogError("PlayerInventoryManager is not assigned in Start.");
            return;
        }

        // Initial update to populate the inventory with pre-existing items
        UpdateInventoryUI();
    }

    void OnDestroy()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= ToggleInventory;
    }

    public void Initialize(PlayerInventoryManager playerInventoryManager)
    {
        this.playerInventoryManager = playerInventoryManager;
        Debug.Log("PlayerInventoryManager assigned in Initialize.");
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI(ItemSO item = null)
    {
        //Debug.Log("Updating Inventory UI");
        if (inventorySlotParent == null)
        {
            Debug.LogError("Inventory Slot Parent is not assigned.");
            return;
        }

        foreach (Transform child in inventorySlotParent)
        {
            Destroy(child.gameObject);
        }

        if (playerInventoryManager == null)
        {
            Debug.LogError("PlayerInventoryManager is not assigned in UpdateInventoryUI.");
            return;
        }

        foreach (var inventoryItem in playerInventoryManager.inventoryItems)
        {
            //Debug.Log("Adding item to UI: " + inventoryItem.itemName);
            GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotParent);
            slot.SetActive(true); // Ensure the slot is enabled
            Image itemImage = slot.transform.Find("Item").GetComponent<Image>();
            if (itemImage != null)
            {
                if (inventoryItem.itemIcon != null)
                {
                    itemImage.sprite = inventoryItem.itemIcon;
                    //Debug.Log("Item icon set: " + inventoryItem.itemIcon.name);
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

        GameEventsManager.instance.inventoryEvents.InventoryUpdated(playerInventoryManager.inventoryItems.Count);
    }
}
