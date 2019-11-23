using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int Damage;
    public int ClipSize;
    public float BulletSpeed;
    public float FireRate = 0.3f;
    private PlayerStats stats;
    private GameObject crosshairs;
    public float Accuracy;
    public float reloadSpeed;
    public GameObject BulletPrefab;
    public float barrelOffset = 0;
    public float heightOffset = 0;

    private Animator animator;
    private ReloadCooldown reloadCooldown;
    private int bulletsInClip;
    private float timeSinceLastShot;
    private float reloadStartTime;
    private Random random;

    IEnumerator GetStats()
    {
        yield return new WaitForSeconds(1);
        ClipSize = stats.MaxAmmo;
        bulletsInClip = stats.CurrentAmmo;
    }
    void Start()
    {
        reloadCooldown = GameObject.Find("ReloadCooldown").GetComponent<ReloadCooldown>();
        stats = GameObject.Find("Casper").GetComponent<PlayerStats>();
        animator = transform.GetComponent<Animator>();
        random = new Random();
        timeSinceLastShot = Time.time + FireRate;
        reloadStartTime = -1;
        StartCoroutine(GetStats());
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
                stats.changeAmmo(bulletsInClip);
                reloadStartTime = -1;
            }
        }
    }

    public void FireWeapon(Vector2 direction, float fireRateMultiplier)
    {
        if (bulletsInClip > 0 && !reloadCooldown.reloading)
        {
            if (Time.time - timeSinceLastShot >= FireRate * fireRateMultiplier)
            {
                // trigger fire animation
                animator.SetTrigger("Fire");

                // play audio
                gameObject.GetComponent<AudioSource>().Play();

                // spawn bullet and set position
                GameObject bullet = Instantiate(BulletPrefab) as GameObject;
                Vector2 bulletPosition = transform.position;
                bulletPosition += direction * barrelOffset;
                Vector2 gunUp = transform.up;
                bulletPosition += gunUp * heightOffset; 
                bullet.transform.position = bulletPosition;
                bullet.GetComponent<bullet>().bulletDamage = Damage;

                // accuracy handling
                float spread = Random.Range(-Accuracy, Accuracy);
                Quaternion angle = Quaternion.FromToRotation(bullet.transform.up, direction);
                bullet.transform.rotation *= angle;
                bullet.transform.rotation = Quaternion.AngleAxis(spread, transform.forward) * bullet.transform.rotation;
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * BulletSpeed, ForceMode2D.Impulse);  

                // update variables
                timeSinceLastShot = Time.time;
                bulletsInClip--;
                stats.changeAmmo(-1);
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
}
