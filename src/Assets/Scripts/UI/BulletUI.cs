using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    public RectTransform bulletPanel;
    public Sprite bullet;
    public int maxAmmo, curAmmo;
    private float xDisplacement;
    private List<GameObject> rounds;
    public int CurrentAmmo
    {
        set
        {
            curAmmo = value;
            setBulletUI();
        }
    }

    public void setStartingRounds(Tuple<int,int> ammo)
    {
        rounds = new List<GameObject>();
        CurrentAmmo = ammo.Item1;
        maxAmmo = ammo.Item2;
        setBulletUI();
    }

    private void setBulletUI()
    {
        xDisplacement = 50f;
        if (rounds == null) { return; }
        foreach (GameObject b in rounds)
        {
            GameObject.Destroy(b);
        }
        rounds.Clear();

        for (int i = 0; i < curAmmo; i++)
        {
            generateBullet(Casper.Instance.weaponInventory.GetSelectedWeapon().BulletPrefab.GetComponent<SpriteRenderer>().sprite);
        }
    }

    private void generateBullet(Sprite bullet)
    {
        GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewImage.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        NewImage.sprite = bullet; //Set the Sprite of the Image Component on the new GameObject
        NewObj.GetComponent<RectTransform>().SetParent(bulletPanel.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        NewObj.SetActive(true); //Activate the GameObject
        NewObj.transform.localPosition = bulletPanel.anchoredPosition + new Vector2(xDisplacement, 100);
        rounds.Add(NewObj);
        xDisplacement += 50f;
    }
}