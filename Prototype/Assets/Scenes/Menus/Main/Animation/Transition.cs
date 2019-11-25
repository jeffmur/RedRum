using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    Animator myAnim;
    public GameObject next;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myAnim.SetTrigger("flip");
            StartCoroutine(nextPage(next));
        }
    }

    IEnumerator nextPage(GameObject page)
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        page.SetActive(true);
    }
}
