using UnityEngine;
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemID;
    public int itemAmount;
    public bool isQuestItem;
    public bool isStackable;
    public StatToChange statToChange = new StatToChange();
    public int statChangeAmount;

    public enum StatToChange {
        Health,
        Mana,
        Stamina,
        Strength,
        Agility,
        Intelligence,
        Defense
    };

    // Add any other properties or methods you need for your item
}
