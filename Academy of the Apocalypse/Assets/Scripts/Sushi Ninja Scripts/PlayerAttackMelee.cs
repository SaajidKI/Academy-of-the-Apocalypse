using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour{

      // public Animator animator;
      public Transform attackPt;
      public float attackRange = 0.5f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;
      public int attackDamage = 40;
      public LayerMask enemyLayers;
      public float knockBackForce = 20f;
      public Rigidbody2D rb2D;

      // public GameObject hitVFX;

      void Start(){
      //      animator = gameObject.GetComponent<Animator>();
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                  if (Input.GetKeyDown(KeyCode.Space)) {
                        // animator.SetBool("Melee", true); // added
                        if (Input.GetAxis("Attack") > 0){
                              Attack();
                              nextAttackTime = Time.time + 1f / attackRate;
                        }
                  } else {
                        // animator.SetBool("Melee", false);
                  }
            }
      }

      void Attack(){
            // animator.SetTrigger ("Melee"); // added
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPt.position, attackRange, enemyLayers);
           
            foreach(Collider2D enemy in hitEnemies){
                  Debug.Log("We hit " + enemy.name);
                  enemy.GetComponent<EnemyMeleeDamage>().TakeDamage(attackDamage);

                  Rigidbody2D pushRB = enemy.gameObject.GetComponent<Rigidbody2D>();
                  Vector2 moveDirectionPush = rb2D.transform.position - enemy.transform.position;
                  pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
                  StartCoroutine(EndKnockBack(pushRB));

                  // hit_SFX
                  // gameObject.GetComponent<AudioSource>().Play();

                  // added hit_VFX
                  Vector3 attackPosition = transform.position;
                  if (transform.localScale.x < 0) {
                        attackPosition.x += 2;
                  } else {
                        attackPosition.x -= 2;
                  }

                  // GameObject boomFX = Instantiate(hitVFX, attackPosition, Quaternion.identity);
                  // StartCoroutine(DestroyVFX(boomFX));

                  

            }
      }

      // IEnumerator DestroyVFX(GameObject theEffect) {
      //       yield return new WaitForSeconds(0.5f);
      //       Destroy(theEffect);
      //       gameObject.GetComponent<AudioSource>().Stop();
      // }

      public void AttackUp() {
            attackDamage = 60;
            knockBackForce = 12;
      }

      //NOTE: to help see the attack sphere in editor:
      void OnDrawGizmosSelected(){
           if (attackPt == null) {return;}
            Gizmos.DrawWireSphere(attackPt.position, attackRange);
      }

      IEnumerator EndKnockBack(Rigidbody2D otherRB){
              yield return new WaitForSeconds(0.2f);
              otherRB.velocity= new Vector3(0,0,0);
       }
}