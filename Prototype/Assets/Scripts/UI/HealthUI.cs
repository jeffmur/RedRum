﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject healthPanel;
    public Image[] hearts, emptyHearts;
    public int maxHP, currentHP;

    public int MaxHP
    {
        set
        {
            maxHP = value;
            setHealthUI();
        }
    }
    public int CurrentHP
    {
        set
        {
            currentHP = value;
            setHealthUI();
        }
    }

    public void setStartingHealth(int activeHearts)
    {
        currentHP = activeHearts;
        maxHP = activeHearts;
        setHealthUI();
    }

    private void setHealthUI()
    {
        for (int i = 0; i < 10; i++)
        {
            hearts[i].enabled = false;
            emptyHearts[i].enabled = false;
        }
        for (int i = 0; i < currentHP; i++)
        {
            hearts[i].enabled = true;
        }
        for (int i = currentHP; i < maxHP; i++)
        {
            emptyHearts[i].enabled = true;
        }
    }
}