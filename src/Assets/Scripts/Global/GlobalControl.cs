﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public CasperData savedCasperData = new CasperData();
    public PlayerData savedPlayerData = new PlayerData();

    void Awake()
    {
        // Carry Stat data over scenes
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }      
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResetCasper(bool isAlive)
    {
        if (!isAlive) { 
            savedCasperData = new CasperData();
            Casper.Instance.localCasperData = savedCasperData;
            GameObject.Find("ItemPanel").GetComponent<Image>().sprite = null;
            Casper.Instance.changeHealth(savedCasperData.MaxHealth);
        }
        Casper.Instance.transform.position = Vector3.zero;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Casper.Instance.gameObject.SetActive(true);
    }
    public void saveItem(GameObject obj)
    {
        if (obj.transform.parent != null)
            obj.transform.parent = null;
        DontDestroyOnLoad(obj);
    }
}
