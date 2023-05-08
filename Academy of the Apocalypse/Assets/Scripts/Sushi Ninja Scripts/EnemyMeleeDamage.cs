using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {
       // private Renderer rend;
    //    public Animator anim;
    //    public GameObject healthLoot;
       public float maxHealth = 100;
       public float currentHealth;
       // public GameObject NextEnemy;
       // public GameHandler gameHandler;

       public float damagePerSecond = 50f; // how much damage to apply per second
       public float burnDuration = 5f; // how long the enemy should burn
       public float tickRate = 1f; // how often to apply damage (in seconds)

       private float nextTickTime;
       private bool isBurning;
       private float burnTimeLeft;

       private SpriteRenderer spriteRenderer;

       void Start(){
            //   rend = GetComponentInChildren<Renderer> ();
            //   anim = GetComponentInChildren<Animator> ();
              spriteRenderer = GetComponent<SpriteRenderer>();
              // redShade = new Color(1f, 0.635f, 0.635f);
              currentHealth = maxHealth;
              // gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
       }

       void Update() {
              if (isBurning) {
                     burnTimeLeft -= Time.deltaTime;
                     ColorChange(new Color(1f, 0.635f, 0.635f));

                     if (burnTimeLeft <= 0f) {
                            isBurning = false;
                            ColorChange(Color.white);

                     } else {
                            if (Time.time >= nextTickTime) {
                            ApplyDamage(damagePerSecond * tickRate);
                            nextTickTime = Time.time + tickRate;

                            }
                     }
              }
       }

       public void ColorChange(Color color) {
              spriteRenderer.color = color;
       }

       public void ColorReset() {
              StartCoroutine(ResetColor());
       }

       public void ApplyBurningDamage() {
              isBurning = true;
              burnTimeLeft = burnDuration;
              nextTickTime = Time.time + tickRate;
       }

       public void ApplyDamage(float damage) {
              damage += 20;
              currentHealth -= damage;
              if (currentHealth <= 0) {
                     Die();
              }
       }

       public void ApplyIceDamage(float damage) {
              damage += 20;
              currentHealth -= damage;
              if (currentHealth <= 0) {
                     Die();
              }
       }

       public void TakeDamage(int damage){
              damage += 20;
              currentHealth -= damage;
              //rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
              //StartCoroutine(ResetColor());
              //anim.SetTrigger ("Hurt");
              if (currentHealth <= 0){
                     Die();
              }
       }

       // public void TakeFlameDamage(int damage) {
       //        currentHealth -= damage;
       // }
       

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
              // Debug.Log("You Killed a baddie. You deserve loot!");
              Destroy(gameObject);

       }

       IEnumerator ResetColor(){
              yield return new WaitForSeconds(4f);
              spriteRenderer.color = Color.white;
       }
}
