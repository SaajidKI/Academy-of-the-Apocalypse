using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BulletDamage = 20;
    public float knockBackForce = 20f;
    public Rigidbody2D rb2D;    
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "bullet") {
            gameObject.GetComponent<Renderer>().enabled = false;
            StartCoroutine(DestroyObject(gameObject));
        }

        if (other.gameObject.tag == "Enemy") {
            other.GetComponent<EnemyMeleeDamage>().TakeDamage(BulletDamage);
            Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
            pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
            StartCoroutine(EndKnockBack(pushRB));
        }

        if (other.gameObject.tag == "S_Enemy") {
            other.GetComponent<EnemyMeleeDamage>().TakeDamage(BulletDamage);
            Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
            pushRB.AddForce(moveDirectionPush.normalized * (knockBackForce / 2f) * - 1f, ForceMode2D.Impulse);
            StartCoroutine(EndKnockBack(pushRB));
        }
    }

    private IEnumerator EndKnockBack(Rigidbody2D otherRB){
        yield return new WaitForSeconds(0.2f);
        otherRB.velocity= Vector3.zero;
    }

    private IEnumerator DestroyObject(GameObject bullet) {
        yield return new WaitForSeconds(0.5f);
        Destroy(bullet);
    }
}

