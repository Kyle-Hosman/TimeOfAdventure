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

    public InventoryButton CreateButton(ItemSO item)
    {
        // Check if the item already exists in the list
        InventoryButton existingButton = inventoryButtons.Find(button => button.item == item);
        if (existingButton != null)
        {
            existingButton.IncrementQuantity();
            return existingButton;
        }

        // Create a new button if the item doesn't exist
        InventoryButton inventoryButton = InstantiateInventoryButton(item);
        inventoryButtons.Add(inventoryButton);
        return inventoryButton;
    }

    private InventoryButton InstantiateInventoryButton(ItemSO item)
    {
        // create the button
        InventoryButton inventoryButton = Instantiate(buttonPrefab, contentParent).GetComponent<InventoryButton>();
        // game object name in the scene
        inventoryButton.gameObject.name = item.name + " (" + item.id + ")";
        // initialize with a default action
        RectTransform buttonRectTransform = inventoryButton.GetComponent<RectTransform>();
        inventoryButton.Initialize(item, () => {
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
    public InventoryButton GetButtonFromItem(ItemSO item)
    {
        foreach (var button in inventoryButtons)
        {
            if (button.item == item)
            {
                Debug.Log("Button found: " + button.name);
                return button;
            }
        }
        Debug.Log("Button not found for item: " + item.itemName);
        return null;
    }

    public void UseItem(ItemSO item)
    {
        InventoryButton button = GetButtonFromItem(item);
        if (button != null)
        {
            button.DecrementQuantity();
            if (button.Quantity <= 0)
            {
                RemoveButton(button);
            }
        }
    }

    public void RemoveButton(InventoryButton button)
    {
        if (inventoryButtons.Contains(button))
        {
            Debug.Log("DESTROYING button: " + button.name);
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