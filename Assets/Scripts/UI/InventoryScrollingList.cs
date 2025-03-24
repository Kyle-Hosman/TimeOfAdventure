using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryScrollingList : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform contentParent;

    private Dictionary<ItemSO, InventoryButton> itemToButtonMap = new Dictionary<ItemSO, InventoryButton>();

    public InventoryButton CreateButtonIfNotExists(ItemSO item, UnityAction onSelectAction)
    {
        if (!itemToButtonMap.ContainsKey(item))
        {
            GameObject buttonObject = Instantiate(buttonPrefab, contentParent);
            InventoryButton inventoryButton = buttonObject.GetComponent<InventoryButton>();
            if (inventoryButton == null)
            {
                Debug.LogError("InventoryButton component is missing on the button prefab.");
                return null;
            }
            inventoryButton.Setup(item.itemName, onSelectAction);
            itemToButtonMap[item] = inventoryButton;
        }
        return itemToButtonMap[item];
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
