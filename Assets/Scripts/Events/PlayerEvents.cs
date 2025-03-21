using System;
using UnityEngine;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null) 
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null) 
        {
            onEnablePlayerMovement();
        }
    }

    public event Action<int> onExperienceGained;
    public void ExperienceGained(int experience) 
    {
        if (onExperienceGained != null) 
        {
            onExperienceGained(experience);
        }
    }

    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level) 
    {
        if (onPlayerLevelChange != null) 
        {
            onPlayerLevelChange(level);
        }
    }

    public event Action<int> onPlayerExperienceChange;
    public void PlayerExperienceChange(int experience) 
    {
        if (onPlayerExperienceChange != null) 
        {
            onPlayerExperienceChange(experience);
        }
    }

    public event Action<int> onHealthChanged;
    public void HealthChanged(int healthChange) 
    {
        if (onHealthChanged != null) 
        {
            onHealthChanged(healthChange);
        }
    }

    public event Action<int> onPlayerHealthChange;
    public void PlayerHealthChange(int health) 
    {
        if (onPlayerHealthChange != null) 
        {
            onPlayerHealthChange(health);
        }
    }

    public event Action onPlayerDeath;
    public void PlayerDeath() 
    {
        if (onPlayerDeath != null) 
        {
            onPlayerDeath();
        }
    }
}
