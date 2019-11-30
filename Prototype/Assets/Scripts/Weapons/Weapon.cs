using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int Damage;
    public int ClipSize;
    public float BulletSpeed;
    public float FireRate;
    public int BulletsPerShot = 1;
    private Casper casper;
    public float Accuracy;
    public float reloadSpeed;
    public GameObject BulletPrefab;
    public float barrelOffset = 0;
    public float heightOffset = 0;

    private Animator animator;
    private ReloadCooldown reloadCooldown;
    public int bulletsInClip;
    private float timeSinceLastShot;
    private float reloadStartTime;
    private Random random;
    private GameObject pickupUI;
    private WeaponInventory WI;

    public IEnumerator GetStats()
    {
        yield return new WaitForSeconds(0.001f);
        casper.MaxAmmo = ClipSize;
        casper.changeAmmo(bulletsInClip);
    }
    void Start()
    {
        WI = GameObject.Find("WeaponInventory").GetComponent<WeaponInventory>();
        pickupUI = Resources.Load<GameObject>("UI/flotingText");
        reloadCooldown = GameObject.Find("ReloadCooldown").GetComponent<ReloadCooldown>();
        casper = GameObject.Find("Casper").GetComponent<Casper>();
        animator = transform.GetComponent<Animator>();
        random = new Random();
        timeSinceLastShot = Time.time + FireRate;
        reloadStartTime = -2;
        //StartCoroutine(GetStats());
    }

    void Update()
    {
        // Hot reload
        if (Input.GetKeyDown(KeyCode.R) && bulletsInClip != ClipSize && !reloadCooldown.reloading) {
            animator.SetBool("Reload", true);
            reloadCooldown.StartReload(reloadSpeed);
            reloadStartTime = Time.time;
        }
        if (reloadStartTime != -1)
        {
            if (Time.time - reloadStartTime >= reloadSpeed)
            {
                animator.SetBool("Reload", false);
                bulletsInClip = ClipSize;
                casper.changeAmmo(bulletsInClip);
                reloadStartTime = -1;
            }
        }
    }

    public void FireWeapon(Vector2 direction)
    {
        if (bulletsInClip > 0 && !reloadCooldown.reloading)
        {
            if (Time.time - timeSinceLastShot >= FireRate)
            {
                Shoot(direction);

                // update variables
                timeSinceLastShot = Time.time;
                bulletsInClip--;
                casper.changeAmmo(-1);
                // Successful shot
                casper.localPlayerData.totalShots += 1;
            }
        }
        else // reload
        {
            if (reloadStartTime == -1)
            {
                reloadStartTime = Time.time;
                reloadCooldown.StartReload(reloadSpeed);
                animator.SetBool("Reload", true);
            }
        }
    }

    public void Shoot(Vector2 direction)
    {
        // trigger fire animation
        animator.SetTrigger("Fire");

        // play audio
        gameObject.GetComponent<AudioSource>().Play();
        for (int i = 0; i < BulletsPerShot; i++)
        {
            // spawn bullet and set position
            GameObject bullet = Instantiate(BulletPrefab) as GameObject;
            Vector2 bulletPosition = transform.position;
            bulletPosition += direction * barrelOffset;
            Vector2 gunUp = transform.up;
            bulletPosition += gunUp * heightOffset;
            bullet.transform.position = bulletPosition;
            bullet.GetComponent<bullet>().bulletDamage = (int)(Damage * casper.localCasperData.damageModifier);

            // accuracy handling
            float spread = Random.Range(-Accuracy, Accuracy);
            Quaternion angle = Quaternion.FromToRotation(bullet.transform.up, direction);
            bullet.transform.rotation *= angle;
            bullet.transform.rotation = Quaternion.AngleAxis(spread, transform.forward) * bullet.transform.rotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * BulletSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                WI.AddWeaponToInventory(gameObject);
            }
        }
    }
}
