using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float BulletCooldown;
    private float timeSinceFire;
    private bool fireDiagonal;
    private readonly Vector3 northWest = Vector3.up + Vector3.left;
    private readonly Vector3 northEast = Vector3.up + Vector3.right;
    private readonly Vector3 southWest = Vector3.down + Vector3.left;
    private readonly Vector3 southEast = Vector3.down + Vector3.right;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(FireSlimes());
    }

    private IEnumerator FireSlimes()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            Fire();
        }
    }

    private void Fire()
    {
        EnemyBullet[] bullets = CreateBullets();
        if (!fireDiagonal)
        {
            bullets[0].SetBulletDirection(Vector3.up);
            bullets[1].SetBulletDirection(Vector3.right);
            bullets[2].SetBulletDirection(Vector3.left);
            bullets[3].SetBulletDirection(Vector3.down);
            fireDiagonal = true;
        }
        else
        {
            bullets[0].SetBulletDirection(northWest);
            bullets[1].SetBulletDirection(northEast);
            bullets[2].SetBulletDirection(southWest);
            bullets[3].SetBulletDirection(southEast);
            fireDiagonal = false;
        }
    }

    private EnemyBullet[] CreateBullets()
    {
        EnemyBullet[] bullets = new EnemyBullet[4];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        }
        return bullets;
    }
}
