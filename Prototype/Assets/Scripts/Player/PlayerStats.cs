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
    //private float isInvincible;
    private bool isInvincible;
    public List<Item> heldItems;
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
        onHealthChange(value);
    }

    public void loseHealth(int value) 
    { 
        currentHealth -= value;
        onHealthChange(value);

        if (isInvincible) //logic only works on damage events
        {
            return;
        }
        currentHealth -= value;
        onHealthChange(-value);
        //isInvincible = true;
    }

    //belongs in different class
    public void processInvincibility()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "egg")
        {
            GameObject.Destroy(collision.gameObject);
        }
    }
}
