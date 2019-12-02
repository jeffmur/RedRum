using System.Collections;
using UnityEngine;

public class MainController : SceneSingleton<MainController>
{
    //declare all UI elements and the model
    private HealthUI healthInfo;
    private BulletUI ammoInfo;
    private ActiveItemUI activeItemInfo;
    private NotificationUI notificationPanel;
    private DeathPanel deathPanel;
    public GameWorld gameWorld;


    protected override void Awake()
    {
        base.Awake();
        healthInfo = GameObject.FindObjectOfType<HealthUI>();
        ammoInfo = GameObject.FindObjectOfType<BulletUI>();
        activeItemInfo = GameObject.FindObjectOfType<ActiveItemUI>();
        notificationPanel = GameObject.FindObjectOfType<NotificationUI>();
        deathPanel = GameObject.FindObjectOfType<DeathPanel>();

        notificationPanel.gameObject.SetActive(false);
        EventManager.Instance.OnNotifyChange += SendNotification;
        EventManager.Instance.onAmmoChange += updateAmmoUI;
        EventManager.Instance.onHealthTrigger += updatehealthUI;
        EventManager.Instance.onMaxHealthTrigger += updateMaxHealthUI;
        EventManager.Instance.onItemPickupTrigger += updateActiveItemPickupUI;
        EventManager.Instance.onItemActivateTrigger += updateActiveItemUseUI;
    }

    // Start is called before the first frame update
    public void Start()
    {
        healthInfo.setStartingHealth(gameWorld.getStartingHealth());
        ammoInfo.setStartingRounds(gameWorld.getStartingAmmo());
        updateActiveItemPickupUI(GlobalControl.Instance.savedCasperData.CurrentActiveItem);
        //StartCoroutine(LateStart(1f));
    }
    public IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    private void SendNotification(string notification)
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
            StartCoroutine(ShowDeathPanel());
        }
    }

    private IEnumerator ShowDeathPanel()
    {
        deathPanel.ShowPanel();
        yield return new WaitForSeconds(2);
        deathPanel.HidePanel();
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