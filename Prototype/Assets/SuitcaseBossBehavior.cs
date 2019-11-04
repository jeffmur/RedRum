using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitcaseBossBehavior : MonoBehaviour
{

    public float EnemySpeed = 1f;
    public GameObject BulletPrefab;
    private GameObject Casper;
    private float BulletCooldown = 3f;
    private TimedLerp lerp;
    private SpriteRenderer renderer;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        Casper = GameObject.Find("Casper");
        StartCoroutine(FireShots());
        lerp = new TimedLerp(5f, 45f);
        renderer = transform.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        direction = Vector3.Normalize(Casper.transform.position - transform.position);
        transform.position += EnemySpeed * direction * Time.deltaTime;
        if (direction.x < 0)
        {
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }
    }
    
    private IEnumerator FireShots()
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            Fire();
        }
    }

    private void Fire()
    {
        EnemyBullet[] bullets = CreateBullets(6);
        float spread = 10f;
        for (int i = 0; i < bullets.Length; i++)
        {
            Quaternion angle = Quaternion.FromToRotation(bullets[i].transform.up, direction);
            bullets[i].transform.rotation *= angle;
            bullets[i].transform.rotation = Quaternion.AngleAxis(spread * (i - 1), transform.forward) * bullets[i].transform.rotation;
            bullets[i].SetBulletDirection(bullets[i].transform.up);
            spread *= -1;
        }
    }

    private EnemyBullet[] CreateBullets(int number)
    {
        EnemyBullet[] bullets = new EnemyBullet[number];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        }
        return bullets;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().loseHealth(1);
        }
    }
}
