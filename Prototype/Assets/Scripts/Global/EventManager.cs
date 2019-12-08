using UnityEngine;
public class EventManager : SceneSingleton<EventManager>
{
    public delegate void onNotifyChangeDelegate(string notification);
    public event onNotifyChangeDelegate OnNotifyChange;

    public delegate void onHealthTriggerDelegate(int value);
    public event onHealthTriggerDelegate onHealthTrigger, onMaxHealthTrigger;

    public delegate void onAmmoChangeDelegate(int val);
    public event onAmmoChangeDelegate onAmmoChange;

    public delegate void onItemPickupTriggerDelegate(Item item);
    public event onItemPickupTriggerDelegate onItemPickupTrigger;

    public delegate void onItemActivateTriggerDelegate(ActivatedItem item);
    public event onItemActivateTriggerDelegate onItemActivateTrigger;

    public delegate void gameEventListener();
    public event gameEventListener onWeaponAdded, onWeaponFired, onWeaponReloaded, onHealed, onDamaged, onItemUse, onItemPickup,
        onRoomCompleted, onNewRoomEntered, onBossRoomEnter, onBossRoomCompleted, onWaveStart, onWaveEnd;

    private void Start()
    {
        GetRoomDelegates();

        Casper.Instance.onHealthChange += TriggerHealthChange;
        Casper.Instance.onMaxHealthChange += TriggerMaxHealthChange;
        Casper.Instance.weaponInventory.onWeaponUse += TriggerAmmoChange;
        Casper.Instance.onItemPickup += TriggerItemPickup;
        Casper.Instance.onItemUse += TriggerItemActivate;

        //generic event listeners
        Casper.Instance.CasperHealedEvent += TriggerOnHealed;
        Casper.Instance.CasperDamageEvent += TriggerOnDamaged;
        Casper.Instance.weaponInventory.WeaponAddedEvent += TriggerWeaponAdded;
        Casper.Instance.weaponInventory.WeaponFiredEvent += TriggerWeaponFired;
        Casper.Instance.weaponInventory.WeaponReloadEvent += TriggerWeaponReloaded;
        Casper.Instance.ItemUseEvent += TriggerItemUse;
        Casper.Instance.ItemPickupEvent += TriggerItemPickup;
    }

    public void GetRoomDelegates()
    {
        RoomHandler.Instance.onBossDefeated += TriggerBossRoomCompleted;
        RoomHandler.Instance.onBossRoomEnter += TriggerBossRoomEntered;
        RoomHandler.Instance.onRoomCompleted += TriggerRoomCompleted;
        RoomHandler.Instance.onNewRoomEnter += TriggerRoomEntered;
        RoomHandler.Instance.onNewWave += TriggerWaveStart;
        RoomHandler.Instance.onWaveComplete += TriggerWaveEnd;
    }

    public void TriggerNotification(string notification) { OnNotifyChange?.Invoke(notification); }
    private void TriggerAmmoChange(int value) { onAmmoChange?.Invoke(value); }
    private void TriggerWeaponAdded() { onWeaponAdded?.Invoke(); }
    private void TriggerWeaponFired() { onWeaponFired?.Invoke(); }
    private void TriggerWeaponReloaded() { onWeaponReloaded?.Invoke(); }
    private void TriggerMaxHealthChange(int value) { onMaxHealthTrigger?.Invoke(value); }
    private void TriggerHealthChange(int CurentHealth) { onHealthTrigger?.Invoke(CurentHealth); }
    private void TriggerOnHealed() { onHealed?.Invoke(); }
    private void TriggerOnDamaged() { onDamaged?.Invoke(); }
    private void TriggerItemPickup(Item item) { onItemPickupTrigger?.Invoke(item); }
    private void TriggerItemPickup() { onItemPickup?.Invoke(); }
    private void TriggerItemActivate(ActivatedItem item) { onItemActivateTrigger?.Invoke(item); }
    private void TriggerItemUse() { onItemUse?.Invoke(); }
    private void TriggerRoomEntered() { onNewRoomEntered?.Invoke(); }
    private void TriggerRoomCompleted() { onRoomCompleted?.Invoke(); }
    private void TriggerBossRoomEntered() { onBossRoomEnter?.Invoke(); }
    private void TriggerBossRoomCompleted() { onBossRoomCompleted?.Invoke(); }
    private void TriggerWaveStart() { onWaveStart?.Invoke(); }
    private void TriggerWaveEnd() { onWaveEnd?.Invoke(); }
}
