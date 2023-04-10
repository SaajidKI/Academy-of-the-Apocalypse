using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    private Vector2 mousePos;
    private float lookAngle;

    // public Animator animator;

    // void Start(){
    //        animator = gameObject.GetComponent<Animator>();
    // }


    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        if(Input.GetButtonDown("Fire2")) {
            // animator.SetBool("Fire", true);
            Shoot();
        } else {
            // animator.SetBool("Fire", false);
        }
        
    }

    void Shoot() {
        // animator.SetTrigger ("Fire"); 
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation = lookAngle + 180f;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
