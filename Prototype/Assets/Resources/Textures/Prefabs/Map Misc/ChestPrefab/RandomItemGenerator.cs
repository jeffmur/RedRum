using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemGenerator : MonoBehaviour
{
    public struct ItemFromChestInfo
    {
        public Object item;
        public short chance; //0-99%

    }
    GameWorld GWInstance;
    //not working atm
    Object GetRandomItemFromChest(ItemFromChestInfo[] items)
    {
        float percentage = Random.Range(0, 100); //randomises from 0 to 99%

        //foreach (ItemFromChestInfo item in GWInstance.itemPool)
        //{
        //    if (percentage < item.chance)
        //    {
        //        //return GWInstance.itemPool[percentage];
        //    }
        //}
        //percentage -= (int)items.chance;
        return null;
    }
}