using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Button button;

    public Button Button => button;

    public void Setup(string itemName, UnityEngine.Events.UnityAction onSelectAction)
    {
        if (itemNameText == null)
        {
            Debug.LogError("itemNameText is not assigned in the inspector.");
            return;
        }

        if (button == null)
        {
            Debug.LogError("button is not assigned in the inspector.");
            return;
        }

        itemNameText.text = itemName;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onSelectAction);
    }

    public Button GetButton()
    {
        return button;
    }
}
