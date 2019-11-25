using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehavior : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D[] boxes;
    // Start is called before the first frame update
    void Start()
    {
        // hide
        animator = GetComponent<Animator>();
        boxes = GetComponents<BoxCollider2D>();
        //show();
    }

    public void show()
    {
        animator.SetBool("Show", true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Casper" && Input.GetKeyDown(KeyCode.E))
        {
            collision.GetComponent<PlayerStats>().SaveData();
            collision.gameObject.SetActive(false);
            animator.SetBool("Show", false);
            LevelManager.Complete();
        }
            
    }
}
