using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private TextMeshProUGUI buttonText; // Use TextMeshProUGUI for TMP support
    private UnityAction onSelectAction;

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
    }

    public void SetOnSelectAction(UnityAction onSelectAction)
    {
        this.onSelectAction = onSelectAction;
    }

     public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("InventoryButton.OnSelect");
        onSelectAction();
    }
}