using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;
    public int ClipSize;
    public float BulletSpeed;
    public float FireRate;
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
        reloadCooldown = GameObject.Find("ReloadCooldown").GetComponent<ReloadCooldown>();
        random = new Random();
        bulletsInClip = ClipSize;
        timeSinceLastShot = Time.time + FireRate;
        reloadStartTime = -1;
    }

    void Update()
    {
        // check for reload
        if (reloadStartTime != -1)
        {
            if (Time.time - reloadStartTime >= reloadSpeed)
            {
                bulletsInClip = ClipSize;
                reloadStartTime = -1;
            }
        }
    }

    public void FireWeapon(Vector2 direction, float fireRateMultiplier)
    {
        if (bulletsInClip > 0)
        {
            if (Time.time - timeSinceLastShot >= FireRate * fireRateMultiplier)
            {
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
