using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper
{
    public delegate void onHealthChangeDelegate(int value);
    public event onHealthChangeDelegate onHealthChange, onMaxHealthChange;
    public event CasperEventDelegate CasperDamageEvent, CasperHealedEvent;

    public void changeMaxHealth(int value)
    {
        if (value == 0)
        {
            return;
        }

        MaxHealth += value;
        int healthChanged;

        if (value > 0)
        {
            if (MaxHealth > 20)
            {
                MaxHealth = 20;
            }
            changeHealth(value);
        }
        if (value < 0)
        {
            if (MaxHealth < 1)
            {
                MaxHealth = 1;
                changeHealth(-(CurrentHealth - 1));
            }
            else
            {
                int previousMaxHealth = MaxHealth - value;
                int missingHealth = previousMaxHealth - CurrentHealth;
                healthChanged = (missingHealth + value >= 0) ? 0 : value + missingHealth;
                healthChanged = (CurrentHealth - healthChanged < 1) ? 1 : healthChanged;
                changeHealth(-healthChanged);
            }
        }
        onMaxHealthChange?.Invoke(MaxHealth);
    }

    public void changeHealth(int value)
    {
        int prevCurrentHealth = CurrentHealth;
        if (value == 0)
        {
            return;
        }

        if (value < 0 && (IsInvincible || IsEtherial))
        {
            print("Casper is invincible");
            return;
        }
        CurrentHealth += value;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        if (CurrentHealth == 1) { StartCoroutine(SlowMotion.DoSlowMotion(5, 0.05f)); }
        if (CurrentHealth <= 0)
        {
            StartCoroutine(SlowMotion.DoSlowMotion(2, 0.05f));
            StartCoroutine(Die());
        }
        onHealthChange?.Invoke(CurrentHealth);
        if (CurrentHealth > prevCurrentHealth)
        {
            CasperHealedEvent?.Invoke();
        }
        else if (CurrentHealth < prevCurrentHealth)
        {
            StartCoroutine(ToggleInvincibility(1));
            FlashDamage();
            CasperDamageEvent?.Invoke();
        }
    }
}
