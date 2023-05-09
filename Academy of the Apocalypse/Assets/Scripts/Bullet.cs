using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BulletDamage = 20;
    public float knockBackForce = 20f;
    public Rigidbody2D rb2D;    
    public Animator animator;
    // private ParticleSystem testParticleSystem = default;
    public GameObject hitParticles;
    public Vector3 spwnPoint;



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
            
            
            spwnPoint = other.transform.position;
            GameObject particleSys = Instantiate (hitParticles, spwnPoint, other.transform.rotation);
            StartCoroutine(destroyParticles(particleSys));
        }

        if (other.gameObject.tag == "S_Enemy") {
            other.GetComponent<BossMeleeDamage>().TakeDamage_B(BulletDamage);
            Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
            pushRB.AddForce(moveDirectionPush.normalized * (knockBackForce / 10f) * - 1f, ForceMode2D.Impulse);
            StartCoroutine(EndKnockBack(pushRB));


            spwnPoint = other.transform.position;
            GameObject particleSys = Instantiate (hitParticles, spwnPoint, other.transform.rotation);
            StartCoroutine(destroyParticles(particleSys));
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
    
    
    
    
//     public void OnTriggerEnter2D(Collider2D other){
//     //if the impact has enough force
//     if (other.gameObject.tag == "Enemy") {
//         //get impact location
//        spwnPoint = other.contacts[0].point;
//         //make particles
//        GameObject particleSys = Instantiate (hitParticles, spwnPoint, other.transform.rotation);
//        StartCoroutine(destroyParticles(particleSys));
//     }
// }

    private IEnumerator destroyParticles(GameObject pSys){
           yield return new WaitForSeconds(2f);
           Destroy(pSys);
    }
}

