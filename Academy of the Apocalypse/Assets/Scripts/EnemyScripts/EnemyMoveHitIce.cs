using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHitIce : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public Transform target;
    public float speed = 3f;
    public int damage = 10;
    public int bulletDamage = 5;
    public int EnemyLives = 3;
    private GameHandler gameHandler;
    public BossMeleeDamage healthIndicator;
    public float attackRange = 10;
    private Transform player;
    public float enemy_speed = 3;
    public float knockBackForce = 20f;
    private float scaleX;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    public float bulletSpeed = 30f;

    private float nextFireTime = 0f;
    private float nextSkillTime = 10f;
    private float nextSprayTime = 15f;
    private float stopSprayTime = 0f;
    private float nextShootTime = 0f;
    private bool smallSpray = false;

    private float lookAngle;
    private float SkillAngle;
    private float ToAngle = -10f;

    private Vector2 playerPos;

    public float bulletForce = 20f;

    private float enemy_health;
    private bool rageMode = false;

    void Start()
    {
        rb2D = GetComponentInChildren<Rigidbody2D>();
        scaleX = gameObject.transform.localScale.x;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (GameObject.FindWithTag("GameHandler") != null)
        {
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }

        healthIndicator = this.GetComponent<BossMeleeDamage>();
    }

    void Update()
    {
        enemy_health = healthIndicator.currentHealth;
        float DistToPlayer;
        if (enemy_health > 0) {
            DistToPlayer = Vector3.Distance(transform.position, target.position);
        } else {
            DistToPlayer = 100;
        }
        if (enemy_health > 0) {
            if ((target != null) && (DistToPlayer <= attackRange)) {
                if (enemy_health > 0) {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    if (target.position.x > gameObject.transform.position.x)
                    {
                        gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                    }
                    else
                    {
                        gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                    }
                }
                //anim.SetBool("Walk", true);
                //flip enemy to face player direction. Wrong direction? Swap the * -1.
                if ((Time.time >= nextFireTime) && (enemy_health > 0))
                {
                    FireBullet();
                    nextFireTime = Time.time + fireRate;

                }
                if ((Time.time >= nextSkillTime) && (enemy_health > 0)) {
                    SprayBullet(0f);
                    SprayBullet(40f);
                    SprayBullet(80f);
                    SprayBullet(120f);
                    SprayBullet(160f);
                    SprayBullet(200f);
                    SprayBullet(240f);
                    SprayBullet(280f);
                    SprayBullet(320f);
                    nextSkillTime = Time.time + 10f;
                }

                if (Time.time >= nextSprayTime && rageMode == true) {
                    Debug.Log("Spraytime");
                    // nextShootTime = Time.time + 1f;
                    // Debug.Log("Spraytime done");
                    nextSprayTime = Time.time + 15f;
                    smallSpray = true;
                    nextShootTime = Time.time + 1f;
                    stopSprayTime = Time.time + 3f;
                }

                if (Time.time >= stopSprayTime) {
                    smallSpray = false;
                }

                if (Time.time >= nextShootTime && smallSpray == true) {
                        Debug.Log("In for loop!");
                        portionSpray(ToAngle);
                        nextShootTime = Time.time + 0.2f;
                        ToAngle = ToAngle + 5f;
                } else {
                    ToAngle = -10f;
                }
            }
        }
    
        if (enemy_health < (healthIndicator.maxHealth / 2) && rageMode == false) {

            fireRate = 1f;
            rageMode = true;
            speed = 4f;
        }
    }

    private void FireBullet()
    {
        if (enemy_health > 0) {
            playerPos = target.position - firePoint.position;
            lookAngle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.rotation = lookAngle;
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            // Debug.Log(rb.rotation);
            animator.SetTrigger("Shooting"); 
        }
        
    }

    private void portionSpray(float Angle) {
        playerPos = target.position - firePoint.position;
        lookAngle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, lookAngle + Angle - 90f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation = lookAngle + Angle;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }


    private void SprayBullet(float SkillAngle) {
        if (enemy_health > 0) {
            firePoint.rotation = Quaternion.Euler(0f, 0f, SkillAngle);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.rotation = SkillAngle - 90f;
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            // Debug.Log(rb.rotation);
            animator.SetTrigger("Shooting");
        }
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Enemy hit player");
            gameHandler.playerGetHit(damage);

            Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
            pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * -1f, ForceMode2D.Impulse);
            StartCoroutine(EndKnockBack(pushRB));
        }
    }

    public void IceEffect()
    {
        speed = 1f;
        StartCoroutine(Delay());
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // anim.SetBool("Attack", false);
        }
    }

    // Display the range of enemy's attack when selected in the Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator EndKnockBack(Rigidbody2D otherRB)
    {
        yield return new WaitForSeconds(0.2f);
        otherRB.velocity = new Vector3(0, 0, 0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(4f);
        if (rageMode == true) {
            speed = 4f;
        }
        if (rageMode == false) {
            speed = 3f;
        }

    }

    // IEnumerator SprayDelay() {
    //     yield return new WaitForSeconds(10f);
    //     SprayBullet();
    // }
}
