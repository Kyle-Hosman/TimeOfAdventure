using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public void UseItem()
    {
        // Implement the logic to use the item here
        Debug.Log("Item used: " + gameObject.name);
    }
}
