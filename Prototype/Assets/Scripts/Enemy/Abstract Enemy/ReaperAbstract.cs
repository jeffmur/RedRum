using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAbstract : Enemy
{
    // Start is called before the first frame update

    private Animator anim;
    private float BulletCooldown = 2f;
    private GameObject chest;
    protected override void Start()
    {
        base.Start();
        enemyHealth = 3000;
        anim = GetComponent<Animator>();
        chest = Resources.Load("Textures/Prefabs/Map Misc/chest") as GameObject;
        // CHRIS CHANGE IT TO PURPLE FLAMES :)
        BulletPrefab = Resources.Load<GameObject>("Textures/Projectiles/ReaperBullet");

        setHealthbarMaxValue(enemyHealth);
    }

    public IEnumerator beginFight()
    {
        // Wait
        yield return new WaitForSeconds(0.2f);
        // animate up
        anim.SetTrigger("show");
        // start shooting
        StartCoroutine(FireRoutine());
    }

    private void Die()
    {
        anim.SetTrigger("hide");
        GameObject.Find("Puzzle").GetComponent<SpawnWaves>().BossDied();
        // camera shake
        // spawn a chest
        var c = Instantiate(chest);
        c.transform.parent = transform.parent;
        c.transform.localPosition = new Vector3(-1.5f, -1.3f, 0);
        Destroy(gameObject);
    }

    IEnumerator HideMove()
    {
        yield return new WaitForSeconds(2f); // buffer
        // ----------- hide -----------
        anim.SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().enabled = false;
        // ----------- rand loc -----------
        transform.localPosition = Vector3.zero; // stay in bounds
        Vector3 loc = new Vector3(
            transform.localPosition.x + Random.Range(-4, 4),
            transform.localPosition.y + Random.Range(-4, 4), 0);

        transform.localPosition = loc;
        // ----------- show -----------
        GetComponent<SpriteRenderer>().enabled = true;
        anim.SetTrigger("show");
    }

    IEnumerator FireRoutine()
    {
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(BulletCooldown);
            Attack(i);
            i++;
        }
    }

    protected override void Attack(int index)
    {
        int rand = Random.Range(0, index);
        switch (rand % 4)
        {
            case 0: // lower nipple
                StartCoroutine(nippleShot(10));
                break;
            case 1: // upper nipple
                StartCoroutine(nippleShot(100));
                break;
            case 2: // target casper
                StartCoroutine(bombSpread());
                break;
            case 3: // target
                StartCoroutine(HideMove());
                StartCoroutine(bombSpread());
                break;

        }
    }

    IEnumerator bombSpread()
    {
        yield return new WaitForSeconds(5f);
        // GET A SPAWNPOINT OBJECT
        var sp = Instantiate(EnemyManager.Instance.getSpawnPoint());
        // ASSIGN POS TO CASPER
        sp.transform.position = casper.transform.position;
        Destroy(sp, 4f);
        // WAIT FOR 3 SECONDS
        yield return new WaitForSeconds(3f);
        // SPRAY
        bulletSpray(0, 360, 30, sp.transform.position);
    }

    IEnumerator FireWave(int numOfWaves, float duration)
    {
        float hardStop = Time.time + duration;
        int offset = 360 / numOfWaves;
        int i = 0;
        while (true)
        {
            yield return new WaitForSeconds(0f);
            for(int j = 0; j < numOfWaves*offset; j+=offset)
                waveShot(i+j);
            i++;
            if (Time.time > hardStop)
            {
                isFiringWave = false;
                yield break;
            }

        }

    }

    /**
     * Shoots four massive waves of bullets that rotate 360
     * Degrees based on index (increments by counter in FireSlimes
     */
    private void waveShot(int index)
    {
        int shots = 3;
        int val = index % 360;
        bulletSpray(val, val - 20, shots, transform.position);
    }

    /**
     * Looks like a nippe ;)
     * Shoots a full circle
     */
    IEnumerator nippleShot(int numberOfBullets)
    {
        yield return new WaitForSeconds(2f);
        bulletSpray(0, 360, numberOfBullets, transform.position);
    }

    private void bulletSpray(float endAngle, float startAngle, int numOfBullets, Vector3 loc)
    {
        EnemyBullet[] bullets = CreateBullets(numOfBullets);
        float angleStep = (endAngle - startAngle) / bullets.Length;
        float angle = startAngle;
        for (int i = 0; i < bullets.Length; i++)
        {
            float bulDirX = loc.x + Mathf.Sin((angle * Mathf.PI) / 180F);
            float bulDiry = loc.y + Mathf.Cos((angle * Mathf.PI) / 180F);
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDiry, 0f);
            Vector2 bulDir = (bulMoveVector - loc);
            bullets[i].transform.position = loc;
            bullets[i].SetBulletDirection(bulDir);
            bullets[i].bulletSpeed = 10f;

            angle += angleStep;
        }
    }
    private EnemyBullet[] CreateBullets(int amt)
    {
        EnemyBullet[] bullets = new EnemyBullet[amt];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = Instantiate(BulletPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
        }
        return bullets;
    }

    protected override void DecreaseHealth(int damage)
    {
        base.DecreaseHealth(damage);
        updateHeathBar(damage);

        if (enemyHealth < 0)
        {
            Die();
        }
    }

    private bool isFiringWave = false;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<Casper>().changeHealth(-1);

        if (collision.CompareTag("HeroBullet") && !isFiringWave && enemyHealth <= 1000)
        {
            // Number of waves, duration
            // Seperate from bullet Cooldown
            StartCoroutine(FireWave(Random.Range(2, 5), Random.Range(5, 10)));
            isFiringWave = true;
        }
    }
}
