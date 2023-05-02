using System.Collections;
using UnityEngine;

public class EnemyMoveHitGreen : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed = 4f;
    public int damage = 10;
    public int bulletDamage = 5;
    public int EnemyLives = 3;
    private GameHandler gameHandler;
    public float attackRange = 10;
    private Transform player;
    public float enemy_speed = 3;
    public float stoppingDistance = 7;
    public float retreatDistance = 4;
    public float knockBackForce = 20f;
    private float scaleX;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime = 0f;

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
    }

    void Update()
    {
        float DistToPlayer = Vector3.Distance(transform.position, player.position);

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance && player != null && DistToPlayer <= attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemy_speed * Time.deltaTime);

            if (Time.time >= nextFireTime)
            {
                FireBullet();
                nextFireTime = Time.time + fireRate;
            }
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -enemy_speed * Time.deltaTime);
            gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(firePoint.right * 10f, ForceMode2D.Impulse);

        // Check if the bullet hits the player and deal damage
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            gameHandler.playerGetHit(bulletDamage);
            Destroy(bullet);
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
        speed = 4f;

    }
}
