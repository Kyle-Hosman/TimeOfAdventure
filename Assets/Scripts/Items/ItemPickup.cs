using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemSO item;
    private CircleCollider2D circleCollider;
    private SpriteRenderer visual;

    private void Awake() 
    {
        circleCollider = GetComponent<CircleCollider2D>();
        visual = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter called. Trigger entered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected. Adding item to inventory: " + item.itemName);

            if (item == null)
            {
                Debug.LogError("ItemSO is not set on the ItemPickup script.");
                return;
            }

            GameEventsManager.instance.inventoryEvents.ItemAdded(item);
            Debug.Log("Picked up item: " + item.itemName);

            // Check if the item is a mushroom and call CollectMushroom
            Mushroom_Red mushroom = GetComponent<Mushroom_Red>();
            if (mushroom != null)
            {
                Debug.Log("Mushroom_Red component found. Collecting mushroom.");
                mushroom.CollectMushroom();
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Trigger entered by non-player object.");
        }
    }
}
