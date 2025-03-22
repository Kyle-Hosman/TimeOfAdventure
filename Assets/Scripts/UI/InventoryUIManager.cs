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
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }

    public void UpdateInventoryUI()
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

        foreach (var item in playerInventoryManager.inventoryItems)
        {
            //Debug.Log("Adding item to UI: " + item.itemName);
            GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotParent);
            slot.SetActive(true); // Ensure the slot is enabled
            Image itemImage = slot.transform.Find("Image").GetComponent<Image>();
            if (itemImage != null)
            {
                if (item.itemIcon != null)
                {
                    itemImage.sprite = item.itemIcon;
                    //Debug.Log("Item icon set: " + item.itemIcon.name);
                }
                else
                {
                    Debug.LogError("Item icon is null for item: " + item.itemName);
                }
            }
            else
            {
                Debug.LogError("Image component not found in inventory slot prefab.");
            }

            // Ensure the InventoryItem script is added to the slot
            InventoryItem inventoryItem = slot.GetComponent<InventoryItem>();
            if (inventoryItem == null)
            {
                inventoryItem = slot.AddComponent<InventoryItem>();
            }
        }
    }
}
