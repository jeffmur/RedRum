using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrBellhopHat : ActivatedItem
    {
    public bool activated = false;
    private GameObject crosshairs;
    private float velocity = 15f;
    bool returning = false;
    private float boomerangTimer;

    Rigidbody2D rb;

    private Object ThrowingHatPrefab;
    private GameObject ThrowingHat;

    private void Start()
    {
        ThrowingHatPrefab = Resources.Load("Textures/Prefabs/Items/SrBellhopHat");
        crosshairs = GameObject.Find("crossHairs");
    }
    public override void activateItem()
    {
        if (!activated) //if q is pressed
        {
            gameObject.SetActive(true);
            transform.position = player.transform.position; //start at the player
            activated = true;
        }
    }
    private void Update()
    {
        if (activated)
        {
            gameObject.SetActive(false);
            boomerangTimer += Time.deltaTime;
            if (boomerangTimer >= 1f)
            {
                returning = true;
                if ((transform.position - player.transform.position).magnitude < 1f)
                {
                    transform.position = player.transform.position;
                    gameObject.SetActive(false); ; //Sets to false if back
                    activated = false;
                }
            }
            if (!returning)
            {
                Vector2 direction = Vector3.Normalize(crosshairs.transform.localPosition - transform.localPosition);
                transform.Translate(direction * velocity * Time.deltaTime);
            }
            else
            {
                transform.up = player.transform.position - transform.position;
                transform.Translate(Vector2.up * 5f * Time.deltaTime);
            }
        }
    }

    protected override void setItemInfo()
        {
            itemID = 5;
            itemName = "SrBellhopHat";
            caption = "Huh, wonder where Gavin went?";
        }
    }
