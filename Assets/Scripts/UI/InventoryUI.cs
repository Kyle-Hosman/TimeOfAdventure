using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GridLayoutGroup inventoryGrid;
    [SerializeField] private GameObject selectionBorderPrefab;
    [SerializeField] private Transform inventoryMainParent; // Reference to InventoryMainParent

    private GameObject selectionBorder;
    private int selectedIndex = 0;
    private List<InventoryItem> inventoryItems;

    private void Start()
    {
        inventoryItems = new List<InventoryItem>(inventoryGrid.GetComponentsInChildren<InventoryItem>());
        
        // Instantiate the selection border prefab and set its parent to the InventoryMainParent GameObject
        selectionBorder = Instantiate(selectionBorderPrefab, inventoryMainParent);
        selectionBorder.transform.SetAsLastSibling(); // Ensure it is rendered on top

        Debug.Log("Selection border instantiated: " + selectionBorder.name);
        Debug.Log("Selection border parent: " + selectionBorder.transform.parent.name);

        UpdateSelection();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onNavigateInventory += OnNavigateInventory;
        GameEventsManager.instance.inputEvents.onSelectInventoryItem += OnSelectInventoryItem;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onNavigateInventory -= OnNavigateInventory;
        GameEventsManager.instance.inputEvents.onSelectInventoryItem -= OnSelectInventoryItem;
    }

    private void OnNavigateInventory(Vector2 direction)
    {
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
        inventoryItems[selectedIndex].UseItem();
    }

    private void UpdateSelection()
    {
        if (inventoryItems.Count == 0)
        {
            selectionBorder.SetActive(false);
            return;
        }

        selectionBorder.transform.SetParent(inventoryItems[selectedIndex].transform, false);
        selectionBorder.transform.localPosition = Vector3.zero;
        selectionBorder.transform.localScale = Vector3.one;
        selectionBorder.SetActive(true); // Show the selection border

        Debug.Log("Selection border updated: " + selectionBorder.name);
        Debug.Log("Selection border parent: " + selectionBorder.transform.parent.name);

        // Ensure the selection border matches the size of the inventory slot
        RectTransform borderRect = selectionBorder.GetComponent<RectTransform>();
        RectTransform slotRect = inventoryItems[selectedIndex].GetComponent<RectTransform>();
        if (borderRect != null && slotRect != null)
        {
            borderRect.sizeDelta = slotRect.sizeDelta;
        }

        // Change the color of the selection border to yellow
        Image borderImage = selectionBorder.GetComponent<Image>();
        if (borderImage != null)
        {
            borderImage.color = Color.yellow;
        }
    }
}
