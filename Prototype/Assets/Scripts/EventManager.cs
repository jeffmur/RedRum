using UnityEngine;

public class EventManager : MonoBehaviour
{
    private GameObject player;
    private PlayerStats stats;

    public delegate void onNotifyChangeDelegate(string notification);
    public static event onNotifyChangeDelegate OnNotifyChange;

    public delegate void onHealthTriggerDelegate(int value);
    public event onHealthTriggerDelegate onHealthTrigger, onMaxHealthTrigger;

    public delegate void onItemPickupTriggerDelegate(Item item);
    public event onItemPickupTriggerDelegate onItemPickupTrigger;

    public delegate void onItemActivateTriggerDelegate(HeldItem item);
    public event onItemActivateTriggerDelegate onItemActivateTrigger;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();

        stats.onHealthChange += triggerHealthChange;
        stats.onMaxHealthChange += triggerMaxHealthChange;
        stats.onItemPickup += triggerItemPickup;
        stats.onItemUse += triggerItemActivate;
    }

    public static void TriggerNotification(string notification)
    {
        OnNotifyChange?.Invoke(notification);
    }

    public void triggerMaxHealthChange(int value)
    {
        onMaxHealthTrigger?.Invoke(value);
    }

    public void triggerHealthChange(int value)
    {
        onHealthTrigger?.Invoke(value);
    }

    private void triggerItemPickup(Item item)
    {
        onItemPickupTrigger?.Invoke(item);
    }

    private void triggerItemActivate(HeldItem item)
    {
        onItemActivateTrigger?.Invoke(item);
    }
}