using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper
{
    public delegate void onHealthChangeDelegate(int value);
    public event onHealthChangeDelegate onHealthChange, onMaxHealthChange;

    public delegate void onHealthCheckDelegate();
    public event onHealthCheckDelegate onDamaged, onHealed;

    public void changeMaxHealth(int value)
    {
        if (value == 0)
        {
            return;
        }

        localCasperData.MaxHealth += value;
        int healthChanged;

        if (value > 0)
        {
            if (localCasperData.MaxHealth > 20)
            {
                localCasperData.MaxHealth = 20;
            }
            changeHealth(value);
        }
        if (value < 0)
        {
            if (localCasperData.MaxHealth < 1)
            {
                localCasperData.MaxHealth = 1;
                changeHealth(-(localCasperData.CurrentHealth - 1));
            }
            else
            {
                int previousMaxHealth = localCasperData.MaxHealth - value;
                int missingHealth = previousMaxHealth - localCasperData.CurrentHealth;
                healthChanged = (missingHealth + value >= 0) ? 0 : value + missingHealth;
                healthChanged = (localCasperData.CurrentHealth - healthChanged < 1) ? 1 : healthChanged;
                changeHealth(-healthChanged);
            }
        }
        onMaxHealthChange?.Invoke(localCasperData.MaxHealth);
    }

    public void changeHealth(int value)
    {
        if (value == 0)
        {
            return;
        }

        if (value < 0 && (localCasperData.isInvincible || IsEtherial))
        {
            print("Casper is invincible");
            return;
        }
        localCasperData.CurrentHealth += value;
        if (localCasperData.CurrentHealth > localCasperData.MaxHealth)
        {
            localCasperData.CurrentHealth = localCasperData.MaxHealth;
        }
        if (CurrentHealth == 1) { StartCoroutine(SlowMotion.DoSlowMotion(5, 0.05f)); }
        if (localCasperData.CurrentHealth <= 0)
        {
            StartCoroutine(SlowMotion.DoSlowMotion(2, 0.05f));
            StartCoroutine(Die());
        }
        onHealthChange?.Invoke(localCasperData.CurrentHealth);
        if (value > 0)
        {
            onHealed?.Invoke();
        }
        else
        {
            StartCoroutine(ToggleInvincibility(1));
            onDamaged?.Invoke();
        }
    }
}
