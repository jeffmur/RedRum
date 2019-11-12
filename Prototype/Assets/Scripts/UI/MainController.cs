﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine;

public class MainController : MonoBehaviour
{
    //declare all UI elements and the model
    public HealthUI healthInfo;
    public ActiveItemUI activeItemInfo;
    public NotificationUI notificationPanel;
    public GameWorld gameWorld;
    public Text roomStatus;


    private void Awake()
    {
        notificationPanel.gameObject.SetActive(false);
        EventManager.OnNotifyChange += sendNotification;
        gameWorld.eventManager.onHealthTrigger += updatehealthUI;
        gameWorld.eventManager.onMaxHealthTrigger += updateMaxHealthUI;
        gameWorld.eventManager.onItemPickupTrigger += updateActiveItemPickupUI;
        gameWorld.eventManager.onItemActivateTrigger += updateActiveItemUseUI;
    }

    // Start is called before the first frame update
    private void Start()
    {
        healthInfo.setStartingHealth(gameWorld.getStartingHealth());
    }

    private void sendNotification(string notification)
    {
        StartCoroutine(notificationPanel.displayMessage(notification));
    }

    private void updatehealthUI(int value)
    {
        healthInfo.CurrentHP = value;
    }

    private void updateMaxHealthUI(int value)
    {
        healthInfo.MaxHP = value;
    }

    private void updateActiveItemPickupUI(Item item)
    {
        activeItemInfo.displayActiveItem(item);
    }

    private void updateActiveItemUseUI(HeldItem item)
    {
        activeItemInfo.updateItemUI(item);
    }

    public void updateRoomStatus(string room)
    {
        roomStatus.text = "Current Room: " + room;
    }
}