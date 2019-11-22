using UnityEngine;

public class MainController : MonoBehaviour
{
    //declare all UI elements and the model
    public HealthUI healthInfo;
    public BulletUI ammoInfo;
    public ActiveItemUI activeItemInfo;
    public NotificationUI notificationPanel;
    public DeathPanel deathPanel;
    public GameWorld gameWorld;


    private void Awake()
    {
        notificationPanel.gameObject.SetActive(false);
        EventManager.OnNotifyChange += sendNotification;
        gameWorld.eventManager.onAmmoChange += updateAmmoUI;
        gameWorld.eventManager.onHealthTrigger += updatehealthUI;
        gameWorld.eventManager.onMaxHealthTrigger += updateMaxHealthUI;
        gameWorld.eventManager.onItemPickupTrigger += updateActiveItemPickupUI;
        gameWorld.eventManager.onItemActivateTrigger += updateActiveItemUseUI;
    }

    // Start is called before the first frame update
    private void Start()
    {
        healthInfo.setStartingHealth(gameWorld.getStartingHealth());
        ammoInfo.setStartingRounds(gameWorld.getStartingAmmo());
        updateActiveItemPickupUI(GlobalControl.Instance.savedCasperData.CurrentActiveItem);
    }

    private void sendNotification(string notification)
    {
        if(this != null)
            StartCoroutine(notificationPanel.displayMessage(notification));
    }

    private void updateAmmoUI(int value)
    {
        ammoInfo.CurrentAmmo = value;
    }

    private void updatehealthUI(int value)
    {
        healthInfo.CurrentHP = value;
        if(value <= 0)
        {
            deathPanel.showHidePanel();
        }
    }

    private void updateMaxHealthUI(int value)
    {
        healthInfo.MaxHP = value;
    }

    private void updateActiveItemPickupUI(Item item)
    {
        activeItemInfo.displayActiveItem(item);
    }

    private void updateActiveItemUseUI(ActivatedItem item)
    {
        activeItemInfo.updateItemOnActivate(item);
    }
}