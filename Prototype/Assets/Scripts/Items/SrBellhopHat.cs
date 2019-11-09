using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrBellhopHat : ActivatedItem
    {
        public override void activateItem()
        {
        //can throw hat as a Projectile (boomerang style)
        }

        protected override void setItemInfo()
        {
            itemID = 5;
            itemName = "SrBellhopHat";
            caption = "Huh, wonder where Gavin went?";
        }
    }
