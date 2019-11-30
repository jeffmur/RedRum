using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasperData
{ 
    public float FireRate = 1f;
    public int CurrentHealth = 5;
    public int MaxHealth = 5;
    public float Speed = 5f;
    public float damageModifier = 1;
    public Vector3 Scale = new Vector3(6, 6, 1);
    public bool isInvincible = false;
    public bool isEtherial = false;
    public ActivatedItem CurrentActiveItem;
}
