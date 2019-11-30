using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text text;
    public BulletUI bulletui;
    // Update is called once per frame
    void Update()
    {
        string currentHealth = Casper.Instance.localCasperData.CurrentHealth.ToString();
        string maxHealth = Casper.Instance.localCasperData.MaxHealth.ToString();
        string currentAmmo = Casper.Instance.localCasperData.CurrentAmmo.ToString();
        string maxAmmo = Casper.Instance.localCasperData.MaxAmmo.ToString();
        string uiCurrentAmmo = bulletui.curAmmo.ToString();
        string uiMaxAmmo = bulletui.maxAmmo.ToString();
        text.text = "Current Health: " + currentHealth + '\n' +
            "Max Health: " + maxHealth + '\n' +
            "Current Ammo: " + currentAmmo + '\n' +
            "Current Ammo UI: " + uiCurrentAmmo + '\n' +
            "Max Ammo: " + maxAmmo + '\n' +
            "Max Ammo UI: " + uiMaxAmmo + '\n';
    }
}
