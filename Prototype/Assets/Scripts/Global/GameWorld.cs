using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameWorld : SceneSingleton<GameWorld>
{
    private Casper casper;
    private GameObject crosshairs;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        casper = Casper.Instance;
        Debug.Assert(casper != null);

        Cursor.visible = false;
        crosshairs = GameObject.Find("crossHairs");

    }

    // Update is called once per frame
    private void Update()
    {
        PositionCrosshair();
        TestController();
    }

    public Tuple<int, int> getStartingHealth()
    {
        return Tuple.Create(GlobalControl.Instance.savedCasperData.CurrentHealth, 
            GlobalControl.Instance.savedCasperData.MaxHealth);
    }

    public Tuple<int,int> getStartingAmmo()
    {
        print("GETSTARTINGAMMO " + Tuple.Create(GlobalControl.Instance.savedCasperData.CurrentAmmo,
            GlobalControl.Instance.savedCasperData.MaxAmmo));
        return Tuple.Create(GlobalControl.Instance.savedCasperData.CurrentAmmo, 
            GlobalControl.Instance.savedCasperData.MaxAmmo);
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
            casper.localCasperData.print();
            //SlowMotion.DoSlowMotion(5, 0.1f);
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
