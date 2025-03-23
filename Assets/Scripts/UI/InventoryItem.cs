using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, ISelectHandler
{
    [Header("Components")]
    [SerializeField] private Button button;
    [SerializeField] private Image itemImage;

    private int slotIndex;

    private void Awake()
    {
        if (itemImage == null)
        {
            itemImage = transform.Find("Item").GetComponent<Image>();
            if (itemImage == null)
            {
                Debug.LogError("Image component not found in InventoryItem.");
            }
        }
    }

    public void SetItem(Sprite itemSprite, int index)
    {
        if (itemImage == null)
        {
            Debug.LogError("itemImage is null in SetItem.");
            return;
        }

        itemImage.sprite = itemSprite;
        slotIndex = index;
    }

    public void OnSelect(BaseEventData eventData)
    {
        // Handle selection logic
        GameEventsManager.instance.inventoryEvents.UpdateSelectedSlot(slotIndex);
    }

    public void UseItem()
    {
        // Implement the logic to use the item here
    }
}
