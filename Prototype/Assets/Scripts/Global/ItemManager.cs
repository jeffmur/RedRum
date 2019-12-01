using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> listOfItems = null;
    // Start is called before the first frame update
    private void Awake()
    {
        Object[] items = Resources.LoadAll("Textures/Prefabs/Items/PickupItems");
        Object[] weapons = Resources.LoadAll("Textures/Prefabs/Guns/SpawnableGuns");

        foreach (Object t in items)
        {
            GameObject item = (GameObject)t;
            //listOfItems.Add(item);
        }
        foreach (Object t in weapons)
        {
            GameObject weapon = (GameObject)t;
            listOfItems.Add(weapon);
        }
    }
    public GameObject SpawnRandomItem()
    {
        if (listOfItems.Count > 0)
        {
            int index = Random.Range(0, listOfItems.Count);
            GameObject selectedItem = listOfItems[index];
            listOfItems.RemoveAt(index);
            return selectedItem;
        }
        return null;
    }
}
