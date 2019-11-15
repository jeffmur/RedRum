using UnityEngine;

public class EventManager : MonoBehaviour
{
    private GameObject player;
    private PlayerStats stats;
    private float Timer;
    private bool FlashingBegan;

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

    public void triggerHealthChange(int CurentHealth)
    {
        //Debug.Log("Changing health by " + CurentHealth);
        onHealthTrigger?.Invoke(CurentHealth);
        FlashDamage();

    }

    private void triggerItemPickup(Item item)
    {
        onItemPickupTrigger?.Invoke(item);
    }

    private void triggerItemActivate(HeldItem item)
    {
        onItemActivateTrigger?.Invoke(item);
    }
    private void FlashDamage()
    {
        FlashingBegan = true;
    }
    private void Update()
    {
        if (FlashingBegan)
        {
            Timer += Time.deltaTime;
            player.GetComponentInChildren<Light>().color = Color.Lerp(Color.red, Color.white, 0.1f);
            player.GetComponentInChildren<Light>().range = 2f;
            player.GetComponentInChildren<Light>().intensity = 10f;
            if (Timer > 1f)
            {
                FlashingBegan = false;
                Timer = 0f;
            }
        }
        else
        {
            player.GetComponentInChildren<Light>().intensity = 1f;
            player.GetComponentInChildren<Light>().range = 3f;
            player.GetComponentInChildren<Light>().color = Color.Lerp(player.GetComponentInChildren<Light>().color, Color.white, 0.001f);
        }

    }
}