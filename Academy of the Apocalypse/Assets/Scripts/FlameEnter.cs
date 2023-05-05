using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEnter : MonoBehaviour
{
    public int BulletDamage = 20;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(DestroyObject(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "S_Enemy") {
            other.GetComponent<EnemyMeleeDamage>().ApplyBurningDamage();
        }
    }

    private IEnumerator DestroyObject(GameObject bullet) {
        yield return new WaitForSeconds(4f);
        Destroy(bullet);
    }
}
