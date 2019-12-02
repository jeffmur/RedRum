using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Casper
{
    private float Timer;
    private bool FlashingBegan;
    private void FlashDamage()
    {
        FlashingBegan = true;
    }
    private void CheckCasperFlash()
    {
        Light casperLight = Casper.Instance.GetComponentInChildren<Light>();
        if (FlashingBegan)
        {
            Timer += Time.deltaTime;
            casperLight.color = Color.red;
            casperLight.range = 2f;
            casperLight.intensity = 20f;

            if (Timer > .25f && Timer <= .5f)
            {
                casperLight.intensity = 0f;
            }
            if (Timer > .5f)
            {
                casperLight.intensity = 20f;
            }
            if (Timer > .75f)
            {
                Timer = 0f;
                FlashingBegan = false;
            }
        }
        else
        {
            casperLight.intensity = 0f;
        }
    }
}
