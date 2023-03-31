using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {
       private Renderer rend;
    //    public Animator anim;
    //    public GameObject healthLoot;
       public float maxHealth = 100;
       public float currentHealth;
       // public GameObject NextEnemy;
       // public GameHandler gameHandler;

       void Start(){
            //   rend = GetComponentInChildren<Renderer> ();
            //   anim = GetComponentInChildren<Animator> ();
              currentHealth = maxHealth;
              // gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
       }

       public void TakeDamage(int damage){
              currentHealth -= damage;
              //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
              //StartCoroutine(ResetColor());
              //anim.SetTrigger ("Hurt");
              if (currentHealth <= 0){
                     Die();
              }
       }

       void Die(){
            //   Instantiate (healthLoot, transform.position, Quaternion.identity);
              
              //anim.SetBool ("isDead", true);
              GetComponent<Collider2D>().enabled = false;
              StartCoroutine(Death());
              // NextEnemy.SetActive(true);
              // gameHandler.DeathCount();
       }

       IEnumerator Death(){
              yield return new WaitForSeconds(0.5f);
              Debug.Log("You Killed a baddie. You deserve loot!");
              Destroy(gameObject);

       }

       IEnumerator ResetColor(){
              yield return new WaitForSeconds(0.5f);
              rend.material.color = Color.white;
       }
}