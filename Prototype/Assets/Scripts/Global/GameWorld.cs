using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameWorld : SceneSingleton<GameWorld>
{
    private Casper casper;
    private GameObject crosshairs;
    public ItemFloatingText hint;
    private GameObject mCamera;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Cursor.visible = false;
        crosshairs = GameObject.Find("crossHairs");

    }

    private void Start()
    {
        casper = Casper.Instance;
        Debug.Assert(casper != null);
    }

    private void FixedUpdate()
    {
        
        if(mCamera != null)
        {
            Vector3 desiredPosition = new Vector3(casper.transform.localPosition.x, casper.transform.localPosition.y, -10f);
            Vector3 smoothedPostion = Vector3.Lerp(mCamera.transform.position, desiredPosition, 0.125f);
            mCamera.transform.position = smoothedPostion;
        }
        else
            mCamera = GameObject.Find("Main Camera");
        
    }

    // Update is called once per frame
    private void Update()
    {
        PositionCrosshair();
        TestController();
        Debug.Assert(casper != null);
    }

    public Tuple<int, int> getStartingHealth()
    {
        return Tuple.Create(GlobalControl.Instance.savedCasperData.CurrentHealth, 
            GlobalControl.Instance.savedCasperData.MaxHealth);
    }

    public Tuple<int,int> getStartingAmmo()
    {
        Weapon playerWeapon = Casper.Instance.weaponInventory.GetSelectedWeapon();
        return Tuple.Create(playerWeapon.bulletsInClip, playerWeapon.ClipSize);
    }

    public void TestController()
    {
        if (Input.GetKeyDown("1"))
        {
            casper.changeHealth(1);
        }
        if (Input.GetKeyDown("2"))
        {
            casper.changeHealth(-1);
        }
        if (Input.GetKeyDown("3"))
        {
            casper.changeMaxHealth(1);
        }
        if (Input.GetKeyDown("4"))
        {
            casper.changeMaxHealth(-1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            casper.activateItem();
        }
        if (Input.GetKeyDown("5"))
        {
            SlowMotion.DoSlowMotion(5, 0.1f);
        }
        if (Input.GetMouseButton(0))
        {
            casper.FireEquippedGun(PositionCrosshair());
        }
        if (Input.GetMouseButton(1))
        {
            LevelManager.Complete();
        }
    }

    private Vector3 PositionCrosshair()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector3(target.x, target.y, -9f);
        return crosshairs.transform.localPosition;
    }
}
