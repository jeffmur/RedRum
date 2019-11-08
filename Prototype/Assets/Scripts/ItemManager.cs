using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameWorld : MonoBehaviour
{
    public Object[] itemPool;
    //public delegate void onItemPickupDelegate();
    //public static event onItemPickupDelegate onItemPickup, onHeartPickup;

    private void Start()
    {
        itemPool = Resources.LoadAll("Items");
        //print(itemPool.Length);
        //foreach (var t in itemPool)
        //{
        //    Debug.Log(t.name);
        //}
    }


}
