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

    private Dictionary<ItemSO, InventoryButton> itemToButtonMap = new Dictionary<ItemSO, InventoryButton>();

    public InventoryButton CreateButtonIfNotExists(ItemSO item, UnityAction onSelectAction)
    {
        if (!itemToButtonMap.ContainsKey(item))
        {
            InventoryButton inventoryButton = InstantiateInventoryButton(item, onSelectAction);
            itemToButtonMap[item] = inventoryButton;
        }
        return itemToButtonMap[item];
    }

    private InventoryButton InstantiateInventoryButton(ItemSO item, UnityAction onSelectAction)
    {
        // create the button
        InventoryButton inventoryButton = Instantiate(buttonPrefab, contentParent).GetComponent<InventoryButton>();
        // game object name in the scene
        inventoryButton.gameObject.name = item.id + "_button";
        // initialize and set up function for when the button is selected
        RectTransform buttonRectTransform = inventoryButton.GetComponent<RectTransform>();
        inventoryButton.Setup(item.itemName, () => {
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

    public void RemoveButton(ItemSO item)
    {
        if (itemToButtonMap.ContainsKey(item))
        {
            Destroy(itemToButtonMap[item].gameObject);
            itemToButtonMap.Remove(item);
        }
    }

    public void ClearList()
    {
        foreach (var button in itemToButtonMap.Values)
        {
            Destroy(button.gameObject);
        }
        itemToButtonMap.Clear();
    }
}
