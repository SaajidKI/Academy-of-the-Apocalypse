using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHitBlue : MonoBehaviour
{
    public float enemy_speed;
    private float waitTime;
    public float startWaitTime;

    private float elapsedTime = 0f;
    public Transform moveSpot;
    public Vector3 minValues, maxValues;

    public Rigidbody2D rb2D;
    public float speed = 4f;
    private Transform target;
    public int damage = 10;

    public int EnemyLives = 3;
    private GameHandler gameHandler;

    public float attackRange = 10;
    public bool isAttacking = false;
    private float scaleX;

    public float knockBackForce = 20f;

    void Start()
    {
        // anim = GetComponentInChildren<Animator> ();
        rb2D = GetComponentInChildren<Rigidbody2D> ();
        scaleX = gameObject.transform.localScale.x;

        if (GameObject.FindGameObjectWithTag ("Player") != null) {
                target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        }

        if (GameObject.FindWithTag ("GameHandler") != null) {
            gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
        }
        moveSpot.position = new Vector3(Random.Range(minValues.x, maxValues.x), Random.Range(minValues.y, maxValues.y), Random.Range(minValues.z, maxValues.z));
        transform.LookAt(moveSpot);
    }

    void Update()
    {
        float DistToPlayer = Vector3.Distance(transform.position, target.position);

        if ((target != null) && (DistToPlayer <= attackRange))
        {
            // Player is in attack range, move towards player and attack
            isAttacking = true;
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
        else
        {
            // Player is not in attack range, move randomly
            isAttacking = false;
            elapsedTime += Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, enemy_speed * Time.deltaTime);

            if (elapsedTime >= waitTime)
            {
                waitTime = Random.Range(startWaitTime, startWaitTime + 1f);
                elapsedTime = 0f;
                moveSpot.position = new Vector3(Random.Range(minValues.x, maxValues.x), Random.Range(minValues.y, maxValues.y), Random.Range(minValues.z, maxValues.z));
                transform.LookAt(moveSpot);
            }
        }
    }


    

       public void OnCollisionEnter2D(Collision2D other){
            if (other.gameObject.tag == "Player") {
                isAttacking = true;
                // Debug.Log("Enemy hit player");
                //anim.SetBool("Attack", true);
                gameHandler.playerGetHit(damage);
                //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
                //StartCoroutine(HitEnemy());

                //This method adds force to the player, pushing them back without teleporting (choose above or below).
                Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
                Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
                pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
                StartCoroutine(EndKnockBack(pushRB));
            }
       }

       public void IceEffect() {
            speed = 1f;
            StartCoroutine(Delay());
       }

       // public void TakePushback(float KB) {
              
       // }


       public void OnCollisionExit2D(Collision2D other){
              if (other.gameObject.tag == "Player") {
                     isAttacking = false;
                     //anim.SetBool("Attack", false);
              }
       }

       //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }

       IEnumerator EndKnockBack(Rigidbody2D otherRB){
              yield return new WaitForSeconds(0.2f);
              otherRB.velocity= new Vector3(0,0,0);
       }

       IEnumerator Delay() {
              yield return new WaitForSeconds(4f);
              speed = 4f;
        }
}

