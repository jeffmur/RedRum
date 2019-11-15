using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrBellhopHat : ActivatedItem
    {
    public bool activated = false;
    private float velocity = 5f;
    bool returning = false;
    private float boomerangTimer;

    Rigidbody2D rb;

    private Object ThrowingHatPrefab;
    private GameObject ThrowingHat;
    //private TimeLerped sTimerLerp = new TimeLerped(2f, 2f);

    private void Start()
    {
        ThrowingHatPrefab = Resources.Load("Textures/Prefabs/Items/SrBellhopHat");
        // rb = GetComponent<Rigidbody2D>();
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
                transform.Translate(player.transform.up * velocity * Time.deltaTime);
            }
            else
            {
                transform.up = player.transform.position - transform.position;
                transform.Translate(Vector2.up * velocity * Time.deltaTime);
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
