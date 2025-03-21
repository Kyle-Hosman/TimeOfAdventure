using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public void UseItem(ItemSO item)
    {
        switch (item.statToChange)
        {
            case ItemSO.StatToChange.Health:
                GameEventsManager.instance.playerEvents.HealthChanged(item.statChangeAmount);
                break;
            case ItemSO.StatToChange.Mana:
                // Implement mana change logic
                break;
            case ItemSO.StatToChange.Stamina:
                // Implement stamina change logic
                break;
            case ItemSO.StatToChange.Strength:
                // Implement strength change logic
                break;
            case ItemSO.StatToChange.Agility:
                // Implement agility change logic
                break;
            case ItemSO.StatToChange.Intelligence:
                // Implement intelligence change logic
                break;
            case ItemSO.StatToChange.Defense:
                // Implement defense change logic
                break;
            default:
                Debug.LogWarning("Unknown stat to change: " + item.statToChange);
                break;
        }
    }
}
