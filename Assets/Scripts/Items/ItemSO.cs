using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon; // Ensure this property exists
    public enum StatToChange { Health, Mana, Stamina, Strength, Agility, Intelligence, Defense }
    public StatToChange statToChange;
    public int statChangeAmount;
}
