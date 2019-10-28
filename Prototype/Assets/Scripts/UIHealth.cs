using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public GameObject healthPanel;
    public GameWorld gameWorld;
    public Image[] hearts, emptyHearts;
    public int maxHP, currentHP;
    private GameObject player;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStats>().onHealthChange += healthChanged;
        player.GetComponent<PlayerStats>().onMaxHealthChange += maxHealthChanged;
    }

    private void Update()
    {
        testHP();
    }
    public void setStartingHealth(int activeHearts)
    {
        currentHP = activeHearts;
        maxHP = activeHearts;
        for (int i = 0; i < 10; i++)
        {
            hearts[i].enabled = false;
            emptyHearts[i].enabled = false;
        }
        for (int i = 0; i < activeHearts; i++)
        {
            hearts[i].enabled = true;
        }
    }

    public void echoMaxHPUp()
    {
        if (maxHP == 10)
        {
            // if at cap of 10 hearts, 
            //instead of getting another max heart, heal for 1
            echoHeal();
            return;
        }
        if (maxHP == currentHP)
        {
            hearts[maxHP].enabled = true;
            currentHP++;
        }
        else
        {
            emptyHearts[maxHP].enabled = true;
            echoHeal();
        }
        maxHP++;
    }

    public void echoMaxHPDown()
    {
        if (maxHP == 1)
        {
            return; //we should be at least a little nice.
        }

        hearts[maxHP - 1].enabled = false;
        emptyHearts[maxHP - 1].enabled = false;

        if (currentHP != maxHP && currentHP != 1)
        {
            hearts[currentHP - 1].enabled = false;
            emptyHearts[currentHP - 1].enabled = true;
        }
        if (currentHP != 1)
        {
            currentHP--;
        }
        maxHP--;
    }

    private void echoHeal()
    {
        //if fully healed already
        if (currentHP == maxHP)
        {
            return;
        }
        // set the heart after current health to full
        hearts[currentHP].enabled = true;
        emptyHearts[currentHP].enabled = false;
        currentHP++;
    }

    private void echoHit()
    {     
        if (currentHP == 1)
        {
            // u ded (current hp = 0)
            // do stuff
            return;
        }
        hearts[currentHP - 1].enabled = false;
        emptyHearts[currentHP - 1].enabled = true;
        currentHP--;
    }

    private void maxHealthChanged(int value)
    {
        if (value == 1)
        {
            echoMaxHPUp();
        }
        else if (value == -1)
        {
            echoMaxHPDown();
        }
        else
        {
            Debug.LogError("Max health change is not 1 or -1");
        }
    }

    private void healthChanged(int value)
    {
        if (value > 0)
        {
            for (int i = 0; i < value; i++) { echoHeal(); }
        }
        else
        {
            value = Mathf.Abs(value);
            for (int i = 0; i < value; i++) { echoHit(); }
        }
    }


    public void testHP()
    {
        if (Input.GetMouseButtonDown(0))
        {
            echoMaxHPUp();
        }
        if (Input.GetMouseButtonDown(1))
        {
            echoMaxHPDown();
        }
        if (Input.GetKeyDown("1"))
        {
            echoHeal();
        }
        if (Input.GetKeyDown("2"))
        {
            echoHit();
        }
    }
}
