using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//private GameObject ThrowingHat;

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
    private void Update()
    {
        if (activated)
        {
            boomerangTimer += Time.deltaTime;
            if (boomerangTimer >= 1f)
            {
                returning = true;
                if ((ThrowingHat.transform.position - player.transform.position).magnitude < 1f)
                {
                    Destroy(ThrowingHat); //destory if back
                    activated = false;
                }
            }              

            if (!returning)
            {
                ThrowingHat.transform.Translate(player.transform.up * velocity * Time.deltaTime);
            }
            else
            {
                ThrowingHat.transform.up = player.transform.position - ThrowingHat.transform.position;
                ThrowingHat.transform.Translate(Vector2.up * velocity * Time.deltaTime);
            }
        }     
    }
    public override void activateItem()
        {
        if(!activated)
        {
           ThrowingHat = Instantiate(ThrowingHatPrefab) as GameObject;
            ThrowingHat.transform.position = player.transform.position;
        }
        activated = true;
    }

    protected override void setItemInfo()
        {
            itemID = 5;
            itemName = "SrBellhopHat";
            caption = "Huh, wonder where Gavin went?";
        }
    }
