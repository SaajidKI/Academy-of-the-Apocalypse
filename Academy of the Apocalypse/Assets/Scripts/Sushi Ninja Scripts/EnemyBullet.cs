using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int BulletDamage = 50;

    public Rigidbody2D rb2D;

    private Transform player;
    private GameObject gamehandle;
    private Vector2 target;


    // public GameObject bulletPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gamehandle = GameObject.FindGameObjectWithTag("GameHandler");
        target = new Vector2(player.position.x, player.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // if (Vector2.Distance(transform.position, target) < 0.3f)
        // {
        //     DestroyObject(gameObject);
        // }

        // DestroyObject(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Enemy" || other.gameObject.tag != "S_Enemy" || other.gameObject.tag != "bullet")
        {
            // gameObject.GetComponent<Renderer>().enabled = false;
            StartCoroutine(DestroyObject(gameObject));
        }

        if (other.gameObject.tag == "Player")
        {
            gamehandle.GetComponent<GameHandler>().playerGetHit(BulletDamage);
            StartCoroutine(DestroyObject(gameObject));
        }


    }

    private IEnumerator DestroyObject(GameObject bullet)
    {
        Debug.Log("Destroying bullet");
        yield return new WaitForSeconds(0.5f);
        Destroy(bullet);
    }
}
