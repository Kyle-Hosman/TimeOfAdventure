using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public Button button { get; private set; }
    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;

    // because we're instantiating the button and it may be disabled when we
    // instantiate it, we need to manually initialize anything here.
    public void Initialize(string displayName, UnityAction selectAction) 
    {
        this.button = this.GetComponent<Button>();
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();

        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }
    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }

}
