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

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        if(Input.GetButtonDown("Fire2")) {
            Shoot();
        }
        
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation = lookAngle + 180f;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
