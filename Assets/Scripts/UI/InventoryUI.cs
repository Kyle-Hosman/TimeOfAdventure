using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private InventoryScrollingList scrollingList;
    [SerializeField] private TextMeshProUGUI itemDisplayNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI statText1;
    [SerializeField] private TextMeshProUGUI statText2;
    [SerializeField] private TextMeshProUGUI levelRequirementsText;
    [SerializeField] private TextMeshProUGUI questRequirementsText;
    [SerializeField] private InventorySO inventorySO;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += InventoryTogglePressed;
        GameEventsManager.instance.inventoryEvents.onItemAdded += ItemAdded;
        GameEventsManager.instance.inventoryEvents.onItemRemoved += ItemRemoved;
        GameEventsManager.instance.inventoryEvents.onInventoryUpdated += RefreshInventoryList;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= InventoryTogglePressed;
        GameEventsManager.instance.inventoryEvents.onItemAdded -= ItemAdded;
        GameEventsManager.instance.inventoryEvents.onItemRemoved -= ItemRemoved;
        GameEventsManager.instance.inventoryEvents.onInventoryUpdated -= RefreshInventoryList;
    }

    private void InventoryTogglePressed()
    {
        if (contentParent.activeInHierarchy)
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
        contentParent.SetActive(true);
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        PopulateInventoryList();
        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        EventSystem.current.SetSelectedGameObject(null);
        scrollingList.ClearList();
    }

    private void PopulateInventoryList()
    {
        scrollingList.ClearList();
        foreach (ItemSO item in inventorySO.inventoryItems)
        {
            ItemAdded(item);
        }
    }

    private void ItemAdded(ItemSO item)
    {
        InventoryButton inventoryButton = scrollingList.CreateButton(item, () =>
        {
            SetInventoryInfo(item);
        });


        if (firstSelectedButton == null)
        {
            firstSelectedButton = inventoryButton.button;
        }

        SetInventoryInfo(item);
    }

    private void ItemRemoved(ItemSO item)
    {
        InventoryButton removedButton = scrollingList.GetButtonFromItem(item);
        scrollingList.RemoveButton(removedButton);
        PopulateInventoryList(); // Refresh the list after removing an item

        // Update the firstSelectedButton to the next available button
        if (scrollingList.HasButtons())
        {
            firstSelectedButton = scrollingList.GetFirstButton()?.button;
            if (firstSelectedButton != null)
            {
                EventSystem.current.SetSelectedGameObject(firstSelectedButton.gameObject); // Explicitly set the selection
            }
        }
        else
        {
            firstSelectedButton = null; // Clear selection if no buttons remain
            EventSystem.current.SetSelectedGameObject(null); // Clear UI selection
        }
    }

    private void SetInventoryInfo(ItemSO item)
    {
        itemDisplayNameText.text = item.itemName;
        itemDescriptionText.text = item.itemName; //placeholder until I add description
        levelRequirementsText.text = "Level " + item.itemName; //placeholder
        questRequirementsText.text = "Quest Requirements: " + item.itemName; //placeholder
        statText1.text = "stat1"; //placeholder
        statText2.text = "stat2"; //placeholder
    }

    private void RefreshInventoryList(int itemCount)
    {
        PopulateInventoryList();
    }
}