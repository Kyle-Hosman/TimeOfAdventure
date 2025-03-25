using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText; // Use TextMeshProUGUI for TMP support
    private UnityAction onSelectAction;
    public ItemSO item; // Store the associated item

    public void Initialize(ItemSO item, UnityAction onSelectAction)
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText == null)
            {
                Debug.LogError("buttonText is not assigned and no TextMeshProUGUI component found in children.");
                return;
            }
        }

        buttonText.text = item.itemName;
        this.onSelectAction = onSelectAction;
        this.item = item; // Assign the item

        // Configure the button's OnClick event to invoke the onUseItem event
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => GameEventsManager.instance.inventoryEvents.UseItem(item));
    }

    public void SetOnSelectAction(UnityAction onSelectAction)
    {
        this.onSelectAction = onSelectAction;
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log("InventoryButton.OnSelect");
        onSelectAction();
    }
}