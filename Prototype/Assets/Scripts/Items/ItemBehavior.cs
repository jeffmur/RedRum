using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private float spinSpeed = 80f;
    private bool pickedUp = false;
    private Vector3 startPos;
    private Transform casper;
    private Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        endPos = new Vector3(startPos.x, startPos.y + 1f, startPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pickedUp)
        {
            if (transform.localPosition.y < endPos.y)
                transform.position += Vector3.up * 1f * Time.deltaTime;
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(casper.position.x + 0.4f, casper.position.y - 0.3f, 0f);
            transform.localScale = new Vector3(3, 3, 1);
            transform.rotation = casper.rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Player")
        //{
        //    pickedUp = true;
        //    casper = collision.transform;
        //}
    }
}
