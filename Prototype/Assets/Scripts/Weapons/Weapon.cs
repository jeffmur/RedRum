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
    public float Accuracy;
    public float reloadSpeed;
    public GameObject BulletPrefab;
    public float barrelOffset = 0;
    public float heightOffset = 0;
    public float equipScale;

    private Animator animator;
    private ReloadCooldown reloadCooldown;
    public int bulletsInClip;
    private float timeSinceLastShot;
    private float reloadStartTime;

    public delegate void onAmmoChangeDelegate();
    public event onAmmoChangeDelegate onAmmoChange;

    public delegate void weaponEventDelegate();
    public event weaponEventDelegate onWeaponFired, onWeaponReload;

    void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.tag = "Weapon";
        reloadCooldown = GameObject.Find("ReloadCooldown").GetComponent<ReloadCooldown>();
        animator = transform.GetComponent<Animator>();
        timeSinceLastShot = Time.time + FireRate;
        reloadStartTime = -2; //nice one, daddy
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
                reloadStartTime = -1;
                onAmmoChange?.Invoke();
                onWeaponReload?.Invoke();
            }
        }
    }

    public bool FireWeapon(Vector2 direction)
    {
        if (bulletsInClip > 0 && !reloadCooldown.reloading)
        {
            if (Time.time - timeSinceLastShot >= FireRate)
            {
                Shoot(direction);

                // update variables
                timeSinceLastShot = Time.time;
                bulletsInClip--;
                onAmmoChange?.Invoke();
                onWeaponFired?.Invoke();
                return true;
            }
        }
        else // reload
        {
            if (reloadStartTime == -1)
            {
                reloadStartTime = Time.time;
                reloadCooldown.StartReload(reloadSpeed);
                animator.SetBool("Reload", true);
                onAmmoChange?.Invoke();
                onWeaponReload?.Invoke();
            }
        }
        return false;
    }

    public void Shoot(Vector2 direction)
    {
        // trigger fire animation
        animator.SetTrigger("Fire");

        // play audio
        gameObject.GetComponent<AudioSource>().Play();
        for (int i = 0; i < BulletsPerShot; i++)
        {
            GameObject bullet = SpawnBullet(direction);
            CalculateAccuracy(direction, bullet);
        }
    }

    public GameObject SpawnBullet(Vector2 direction)
    {
        // spawn bullet and set position
        GameObject bullet = Instantiate(BulletPrefab) as GameObject;
        Vector2 bulletPosition = transform.position;
        bulletPosition += direction * barrelOffset;
        Vector2 gunUp = transform.up;
        bulletPosition += gunUp * heightOffset;
        bullet.transform.position = bulletPosition;
        bullet.GetComponent<bullet>().bulletDamage = (int)(Damage * Casper.Instance.localCasperData.damageModifier);
        return bullet;
    }

    private void CalculateAccuracy(Vector2 direction, GameObject bullet)
    {
        // accuracy handling
        float spread = Random.Range(-Accuracy, Accuracy);
        Quaternion angle = Quaternion.FromToRotation(bullet.transform.up, direction);
        bullet.transform.rotation *= angle;
        bullet.transform.rotation = Quaternion.AngleAxis(spread, transform.forward) * bullet.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * BulletSpeed, ForceMode2D.Impulse);
    }

    public bool IsReloading()
    {
        return reloadCooldown.reloading;
    }
}
