using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerStats stats;
    public int Damage;
    public int ClipSize;
    public float BulletSpeed;
    public float FireRate = 0.3f;
    public float Accuracy;
    public float reloadSpeed;
    public GameObject BulletPrefab;

    private ReloadCooldown reloadCooldown;
    private int bulletsInClip;
    private float timeSinceLastShot;
    private float reloadStartTime;
    private Random random;

    void Start()
    {
        stats = GameObject.Find("Casper").GetComponent<PlayerStats>();
        reloadCooldown = GameObject.Find("ReloadCooldown").GetComponent<ReloadCooldown>();
        random = new Random();
        ClipSize = stats.maxAmmo;
        bulletsInClip = ClipSize;
        timeSinceLastShot = Time.time + FireRate;
        reloadStartTime = -1;
    }

    void Update()
    {
        // Hot reload
        if (Input.GetKeyDown(KeyCode.R) && bulletsInClip != ClipSize && !reloadCooldown.reloading) {
            reloadCooldown.StartReload(reloadSpeed);
            reloadStartTime = Time.time;
        }
        // check for reload
        if (reloadStartTime != -1)
        {
            if (Time.time - reloadStartTime >= reloadSpeed)
            {
                bulletsInClip = ClipSize;
                stats.changeAmmo(bulletsInClip);
                reloadStartTime = -1;
            }
        }
    }

    public void FireWeapon(Vector2 direction, float rotationZ)
    {
        if (bulletsInClip > 0 && !reloadCooldown.reloading)
        {
            if (Time.time - timeSinceLastShot >= FireRate)
            {
                gameObject.GetComponent<AudioSource>().Play();
                GameObject bullet = Instantiate(BulletPrefab) as GameObject;
                bullet.transform.position = transform.position;
                // accuracy 
                float spread = Random.Range(-Accuracy, Accuracy);
                Quaternion angle = Quaternion.FromToRotation(bullet.transform.up, direction);
                bullet.transform.rotation *= angle;
                bullet.transform.rotation = Quaternion.AngleAxis(spread, transform.forward) * bullet.transform.rotation;
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * BulletSpeed, ForceMode2D.Impulse);
                bullet.GetComponent<bullet>().bulletDamage = Damage;
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
            }
        }
    }
}
