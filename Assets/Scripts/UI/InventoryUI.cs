using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GridLayoutGroup inventoryGrid;
    [SerializeField] private GameObject selectionBorderPrefab;
    [SerializeField] private Transform inventoryMainParent; // Reference to InventoryMainParent
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private GameObject inventorySlotPrefab;
    //[SerializeField] private TextMeshProUGUI selectedItemDescription;

    private GameObject selectionBorder;
    private int selectedIndex = 0;
    private List<InventoryItem> inventoryItems;
    private PlayerInventoryManager playerInventoryManager;
    private GameObject inventoryPanel;
    private int selectedItemIndex = -1;

    private void Start()
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
            playerInventoryManager = PlayerInventoryManager.instance;
            if (playerInventoryManager == null)
            {
                Debug.LogError("PlayerInventoryManager is not assigned in Start.");
                return;
            }
        }

        // Initial update to populate the inventory with pre-existing items
        UpdateInventoryUI();

        UpdateInventoryItems();
        
        // Instantiate the selection border prefab and set its parent to the InventoryMainParent GameObject
        selectionBorder = Instantiate(selectionBorderPrefab, inventoryMainParent);
        selectionBorder.transform.SetAsLastSibling(); // Ensure it is rendered on top

        // Ensure the RectTransform properties are correctly set
        RectTransform borderRect = selectionBorder.GetComponent<RectTransform>();
        if (borderRect != null)
        {
            borderRect.anchorMin = new Vector2(0.5f, 0.5f);
            borderRect.anchorMax = new Vector2(0.5f, 0.5f);
            borderRect.pivot = new Vector2(0.5f, 0.5f);
            borderRect.sizeDelta = new Vector2(100, 100);
            borderRect.anchoredPosition = new Vector2(132.9422f, 363.2809f);
        }

        UpdateSelection();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onNavigateInventory += OnNavigateInventory;
        GameEventsManager.instance.inputEvents.onSelectInventoryItem += OnSelectInventoryItem;
        GameEventsManager.instance.inventoryEvents.onUpdateSelectedSlot += OnUpdateSelectedSlot;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += ToggleInventory; // Subscribe to the event
        GameEventsManager.instance.inputEvents.ChangeInputEventContext(InputEventContext.INVENTORY); // Set context to INVENTORY
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onNavigateInventory -= OnNavigateInventory;
        GameEventsManager.instance.inputEvents.onSelectInventoryItem -= OnSelectInventoryItem;
        GameEventsManager.instance.inventoryEvents.onUpdateSelectedSlot -= OnUpdateSelectedSlot;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= ToggleInventory; // Unsubscribe from the event
        GameEventsManager.instance.inputEvents.ChangeInputEventContext(InputEventContext.DEFAULT); // Reset context to DEFAULT
    }

    private void OnNavigateInventory(Vector2 direction)
    {
        if (inventoryItems == null || inventoryItems.Count == 0)
        {
            Debug.Log("Inventory is empty, cannot navigate.");
            return;
        }

        int columns = inventoryGrid.constraintCount;
        int rows = inventoryItems.Count / columns;

        int newIndex = selectedIndex;

        if (direction.y > 0) // Up
        {
            newIndex -= columns;
        }
        else if (direction.y < 0) // Down
        {
            newIndex += columns;
        }
        else if (direction.x < 0) // Left
        {
            newIndex -= 1;
        }
        else if (direction.x > 0) // Right
        {
            newIndex += 1;
        }

        if (newIndex >= 0 && newIndex < inventoryItems.Count)
        {
            selectedIndex = newIndex;
            UpdateSelection();
        }
    }

    private void OnSelectInventoryItem()
    {
        if (inventoryItems == null || selectedIndex < 0 || selectedIndex >= inventoryItems.Count)
        {
            Debug.LogError("Invalid inventory item selection.");
            return;
        }

        inventoryItems[selectedIndex].UseItem();
        GameEventsManager.instance.inventoryEvents.ItemUsed(inventoryItems[selectedIndex]);
    }

    private void OnUpdateSelectedSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventoryItems.Count)
        {
            Debug.LogError("Invalid slot index.");
            return;
        }

        selectedIndex = slotIndex;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        if (inventoryItems == null || selectedIndex < 0 || selectedIndex >= inventoryItems.Count)
        {
            Debug.LogError("Invalid inventory item selection.");
            return;
        }

        selectionBorder.transform.SetParent(inventoryItems[selectedIndex].transform, false);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionBorder.transform.localScale = Vector3.one;
        selectionBorder.SetActive(true); // Show the selection border

        // Ensure the selection border matches the size of the inventory slot
        RectTransform borderRect = selectionBorder.GetComponent<RectTransform>();
        RectTransform slotRect = inventoryItems[selectedIndex].GetComponent<RectTransform>();
        if (borderRect != null && slotRect != null)
        {
            borderRect.anchorMin = slotRect.anchorMin;
            borderRect.anchorMax = slotRect.anchorMax;
            borderRect.pivot = slotRect.pivot;
            borderRect.sizeDelta = slotRect.sizeDelta;
            borderRect.anchoredPosition = slotRect.anchoredPosition;

            // Explicitly reset the RectTransform properties
            borderRect.localPosition = Vector3.zero;
            borderRect.localScale = Vector3.one;
        }

        // Change the color of the selection border to yellow
        Image borderImage = selectionBorder.GetComponent<Image>();
        if (borderImage != null)
        {
            borderImage.color = Color.yellow;
        }
    }

    public void UpdateInventoryItems()
    {
        PlayerInventoryManager playerInventoryManager = PlayerInventoryManager.instance;
        if (playerInventoryManager != null)
        {
            inventoryItems = new List<InventoryItem>(inventoryGrid.GetComponentsInChildren<InventoryItem>());
            GameEventsManager.instance.inventoryEvents.InventoryUpdated(inventoryItems.Count);
        }
        else
        {
            Debug.LogError("PlayerInventoryManager instance is null.");
        }
    }

    void OnDestroy()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= ToggleInventory; // Unsubscribe from the event
    }

    public void Initialize(PlayerInventoryManager playerInventoryManager)
    {
        this.playerInventoryManager = playerInventoryManager;
        Debug.Log("PlayerInventoryManager assigned in Initialize.");
    }

    public void ToggleInventory()
    {
        if (inventoryPanel.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    private void ShowUI()
    {
        inventoryPanel.SetActive(true);
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        // Ensure the inventory UI is updated when shown
        playerInventoryManager.UpdateInventoryUI();
    }

    private void HideUI()
    {
        inventoryPanel.SetActive(false);
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
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

    public void UpdateSelection(int index)
    {
        if (index < 0 || index >= playerInventoryManager.inventoryItems.Count)
        {
            Debug.LogError("Invalid inventory item selection.");
            return;
        }

        selectedItemIndex = index;
        ItemSO selectedItem = playerInventoryManager.inventoryItems[selectedItemIndex];
        //selectedItemDescription.text = selectedItem.itemDescription;
        Debug.Log("Selected item: " + selectedItem.itemName);
    }
}
