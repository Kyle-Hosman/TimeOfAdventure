using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryScrollingList : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform contentParent;

    [Header("Rect Transforms")]
    [SerializeField] private RectTransform scrollRectTransform;
    [SerializeField] private RectTransform contentRectTransform;

    private List<InventoryButton> inventoryButtons = new List<InventoryButton>();

    public InventoryButton CreateButton(ItemSO item, UnityAction onSelectAction)
    {
        InventoryButton inventoryButton = InstantiateInventoryButton(item, onSelectAction);
        inventoryButtons.Add(inventoryButton);
        return inventoryButton;
    }

    private InventoryButton InstantiateInventoryButton(ItemSO item, UnityAction onSelectAction)
    {
        // create the button
        InventoryButton inventoryButton = Instantiate(buttonPrefab, contentParent).GetComponent<InventoryButton>();
        // game object name in the scene
        inventoryButton.gameObject.name = item.name + " (" + item.id + ")";
        // initialize and set up function for when the button is selected
        RectTransform buttonRectTransform = inventoryButton.GetComponent<RectTransform>();
        inventoryButton.Initialize(item.itemName, () => {
            onSelectAction();
            UpdateScrolling(buttonRectTransform);
        });
        return inventoryButton;
    }

    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        // calculate the min and max for the selected button
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        // calculate the min and max for the content area
        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTransform.rect.height;

        // handle scrolling down
        if (buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(
                contentRectTransform.anchoredPosition.x,
                buttonYMax - scrollRectTransform.rect.height
            );
        }
        // handle scrolling up
        else if (buttonYMin < contentYMin) 
        {
            contentRectTransform.anchoredPosition = new Vector2(
                contentRectTransform.anchoredPosition.x,
                buttonYMin
            );
        }
    }

    public void RemoveButton(InventoryButton button)
    {
        if (inventoryButtons.Contains(button))
        {
            Destroy(button.gameObject);
            inventoryButtons.Remove(button);
        }
    }

    public void ClearList()
    {
        Debug.Log("Clearing inventory list...");
        foreach (var button in inventoryButtons)
        {
            Destroy(button.gameObject);
        }
        inventoryButtons.Clear();
    }
}
