﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public RectTransform healthPanel;
    public Sprite fullheart, emptyHeart;
    public int maxHP, currentHP;
    private float xDisplacement, yDisplacement;
    private List<GameObject> currentHearts;
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
        currentHearts = new List<GameObject>();
        currentHP = activeHearts;
        maxHP = activeHearts;
        setHealthUI();
    }

    private void setHealthUI()
    {
        xDisplacement = 50f;
        yDisplacement = 25f;

        foreach (GameObject heart in currentHearts)
        {
            GameObject.Destroy(heart);
        }
        currentHearts.Clear();

        for (int i = 0; i < currentHP; i++)
        {
            generateHeart(fullheart);
        }
        for (int i = currentHP; i < maxHP; i++)
        {
            generateHeart(emptyHeart);
        }
    }

    private void generateHeart(Sprite heart)
    {
        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewImage.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        NewImage.sprite = heart; //Set the Sprite of the Image Component on the new GameObject

        if (healthPanel == null)
            healthPanel = GameObject.Find("PlayerHealth").GetComponent<RectTransform>();

        NewObj.GetComponent<RectTransform>().SetParent(healthPanel.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        NewObj.SetActive(true); //Activate the GameObject
        NewObj.transform.localPosition = healthPanel.anchoredPosition + new Vector2(xDisplacement, yDisplacement);
        currentHearts.Add(NewObj);
        xDisplacement += 50f;
    }
}