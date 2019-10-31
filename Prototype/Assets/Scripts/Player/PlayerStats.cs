using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float moveSpeed;
    private int maxHealth, currentHealth;
    private float fireRate;
    private float accuracy;
    private float cooldownrate;
    private bool isInvincible;
    public List<Item> heldItems;
    private ActiveItem currentActiveItem;

    public delegate void onHealthChangeDelegate(int value);
    public event onHealthChangeDelegate onHealthChange, onMaxHealthChange;

    // Start is called before the first frame update
    void Awake()
    {
        moveSpeed = 5f;
        maxHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float Accuracy { get => accuracy; set => accuracy = value; }
    public float Cooldownrate { get => cooldownrate; set => cooldownrate = value; }
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }

    public void incrementMaxHeath() 
    { 
        maxHealth++;
        onMaxHealthChange?.Invoke(1); 
    }

    public void decrementMaxHeath() 
    { 
        maxHealth--; 
        onMaxHealthChange?.Invoke(-1); 
    }

    public void gainHealth(int value) 
    { 
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        onHealthChange(value);
    }

    public void loseHealth(int value) 
    { 
        if (isInvincible) //logic only works on damage events
        {
            return;
        }
        currentHealth -= value;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        onHealthChange(-value);
        //isInvincible = true;
    }

    //belongs in different class
    public void processInvincibility()
    {

    }

    private void setActiveItem(ActiveItem item)
    {
        if (currentActiveItem != null)
        {
            currentActiveItem.gameObject.SetActive(true);
            currentActiveItem.transform.position = transform.position;
        }
        currentActiveItem = item;
        item.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.tag == "Item")
            {
                collision.gameObject.GetComponent<Item>().process();
            }
            if (collision.gameObject.tag == "Heart")
            {
                gainHealth(1);
            }
        }
    }
}
