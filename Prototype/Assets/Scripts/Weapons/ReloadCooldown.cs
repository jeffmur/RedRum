using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCooldown : MonoBehaviour
{
    private GameObject reloadMarker;
    private float reloadStartTime;
    private float reloadSpeed;
    public bool reloading;
    // Start is called before the first frame update
    void Start()
    {
        reloadMarker = transform.GetChild(0).gameObject;
        transform.GetComponent<SpriteRenderer>().enabled = false;
        reloadMarker.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            reloadMarker.transform.localPosition += Vector3.right * (0.3f / reloadSpeed) * Time.deltaTime;
            if (Time.time - reloadStartTime >= reloadSpeed)
            {
                transform.GetComponent<SpriteRenderer>().enabled = false;
                reloadMarker.GetComponent<SpriteRenderer>().enabled = false;
                reloading = false;
            }
        }
    }

    public void StartReload(float reloadSpeed)
    {
        reloading = true;
        this.reloadSpeed = reloadSpeed;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        reloadMarker.GetComponent<SpriteRenderer>().enabled = true;
        reloadMarker.transform.localPosition = new Vector3(-0.15f, 0, 0);
        reloadStartTime = Time.time;
    }
}
