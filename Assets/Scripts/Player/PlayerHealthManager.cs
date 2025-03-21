using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int startingHealth = 100;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onHealthChanged += HealthChanged;
    }

    private void OnDisable() 
    {
        GameEventsManager.instance.playerEvents.onHealthChanged -= HealthChanged;
    }

    private void Start()
    {
        GameEventsManager.instance.playerEvents.PlayerHealthChange(currentHealth);
    }

    private void HealthChanged(int healthChange) 
    {
        currentHealth += healthChange;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);
        GameEventsManager.instance.playerEvents.PlayerHealthChange(currentHealth);

        if (currentHealth <= 0)
        {
            // Handle player death
            Debug.Log("Player has died.");
            GameEventsManager.instance.playerEvents.PlayerDeath();
        }
    }
}
