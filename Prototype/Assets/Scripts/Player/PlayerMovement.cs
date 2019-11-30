using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject crosshairs;
    private Rigidbody2D rb;
    private Animator animator;
    private Casper casper;
    private SpriteRenderer playerRenderer;
    private GameObject weaponInventory;
    private Weapon selectedWeapon;
    private SpriteRenderer selectedWeaponRenderer;
    private Vector2 movement;
    private float GUN_PIVOT_X = 0.06f;
    private float GUN_PIVOT_Y = -0.05f;
    private float GUN_SLOT_X = 0.03f;
    private float GUN_SLOT_Y = 0.02f;
        

    private void Start()
    {
        casper = Casper.Instance;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        crosshairs = GameObject.Find("crossHairs");
        playerRenderer = transform.GetComponent<SpriteRenderer>();
        weaponInventory = transform.GetChild(0).gameObject;
        Debug.Assert(weaponInventory);
        selectedWeapon = weaponInventory.GetComponent<WeaponInventory>().GetSelectedWeapon();
        if (selectedWeapon != null)
            selectedWeaponRenderer = selectedWeapon.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {
        Vector2 direction = Vector3.Normalize(crosshairs.transform.localPosition - transform.localPosition);
        movement.x =  Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("HorizontalDirection", direction.x);
        animator.SetFloat("VerticalDirection", direction.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        selectedWeapon = weaponInventory.GetComponent<WeaponInventory>().GetSelectedWeapon();
        if (selectedWeapon)
            selectedWeaponRenderer = selectedWeapon.GetComponent<SpriteRenderer>();

        // flips direction of animation and gun side depending on direction of crosshair
        if (direction.x < 0) // left direction
        {
            playerRenderer.flipX = true;
            if (selectedWeapon != null)
            {
                selectedWeaponRenderer.flipX = true;
                weaponInventory.transform.localPosition = new Vector2(-GUN_PIVOT_X, GUN_PIVOT_Y);
                selectedWeapon.transform.localPosition = new Vector2(-GUN_SLOT_X, GUN_SLOT_Y);
            }

        } 
        else // right direction
        {
            playerRenderer.flipX = false;
            if (selectedWeapon != null)
            {
                selectedWeaponRenderer.flipX = false;
                weaponInventory.transform.localPosition = new Vector2(GUN_PIVOT_X, GUN_PIVOT_Y);
                selectedWeapon.transform.localPosition = new Vector2(GUN_SLOT_X, GUN_SLOT_Y);
            }
        }

        // set gun direction towards crosshair
        if (selectedWeapon != null)
        {
            Vector3 v = Vector3.Cross(Vector3.up, direction);
            Vector3 gunDirection = Vector3.Cross(v, direction);
            weaponInventory.transform.localRotation = Quaternion.FromToRotation(Vector3.up, -gunDirection);
        }
    }
    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * casper.MoveSpeed * Time.fixedDeltaTime);
    }
}
