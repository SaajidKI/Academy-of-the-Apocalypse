using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    
    public Animator animator;
    public Transform firePoint;
    public Transform firePointRotate;
    public Transform player;
    public GameObject firePrefab;
    public GameObject icePrefab;
    public GameObject FlameTrapPrefab;
    public GameObject IceMistPrefab;
    public GameObject windPrefab;
    public GameObject WindTornadoPrefab;
    private CameraShake Shaker;
    // public GameObject Indicator;
    public float bulletForce = 20f;
 
    private Vector2 mousePos;
    private Vector2 cursorPos;
    private Vector2[] vecArray;
    private Vector3 push;
    private float lookAngle;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private float distance;

    private bool fireMode = true;
    private bool iceMode = false;
    private bool windMode = false;
    public bool iceUnlock = false;
    public bool windUnlock = false;

    public GameObject cooler;
    public cooldown coolScript;
    public bool isCooldownFire;
    public bool isCooldownIce;
    public bool isCooldownWind;

    public bool shoot_f = true;

    void Start(){
        // animator = gameObject.GetComponent<Animator>();
        fireMode = true;
        coolScript = cooler.GetComponent<cooldown>();
        Shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();

    }


    // Update is called once per frame
    void Update()
    {
        if (shoot_f == true) {
            // animator.SetTrigger ("Fire"); 
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            lookAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            firePointRotate.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

            

            Switch();

            if (Time.time >= nextAttackTime) {
                if (Input.GetButtonDown("Fire2") && fireMode == true) {
                    // animator.SetBool("Fire", true);
                    ShootFire();
                    nextAttackTime = Time.time + 1f / attackRate;
                } 

                if (Input.GetButtonDown("Fire2") && iceMode == true) {
                    // animator.SetBool("Fire", true);
                    ShootIce();
                    nextAttackTime = Time.time + 1f / 8f;
                } 

                if (Input.GetButtonDown("Fire2") && windMode == true) {
                    // animator.SetBool("Fire", true);
                    ShootWind();
                    nextAttackTime = Time.time + 1f / 1f;
                } 
            }

            if (Input.GetButtonDown("Skill") && isCooldownFire == false && fireMode == true) {
                ShootFlameTrap();
            }

            if (Input.GetButtonDown("Skill") && isCooldownIce == false && iceMode == true) {
                ShootIceMist();
            }

            if (Input.GetButtonDown("Skill") && isCooldownWind == false && windMode == true) {
                ShootWindTornado();
            }
        }
    }

    public void enable_shooting() {
        shoot_f = true;
    }

    public void disable_shooting() {
        shoot_f = false;
    }

    public void boolSwitchFire() {
        isCooldownFire = false;
    }

    public void boolSwitchIce() {
        isCooldownIce = false;
    }

    public void boolSwitchWind() {
        isCooldownWind = false;
    }

    void ShootFire() {
        GameObject fireBullet = Instantiate(firePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireBullet.GetComponent<Rigidbody2D>();
        rb.rotation = lookAngle;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Shaker.ShakeCamera(0.05f, 0.1f);
    }

    void ShootWind() {
        GameObject windBullet = Instantiate(windPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = windBullet.GetComponent<Rigidbody2D>();
        rb.rotation = lookAngle;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Shaker.ShakeCamera(0.05f, 0.1f);
    }

    void ShootWindTornado() {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(transform.position, cursorPos);

        if(distance < 5) {
            GameObject WindTornado = Instantiate(WindTornadoPrefab, cursorPos, Quaternion.identity);
            Shaker.ShakeCamera(0.15f, 0.5f);
            coolScript.startingWind();
            isCooldownWind = true;
        }
    }

    

    void ShootFlameTrap() {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(transform.position, cursorPos);

        vecArray = new Vector2[] { new Vector2(0.0f, 1.0f), new Vector2(0.0f, 2.0f), new Vector2(0.0f, 3.0f), new Vector2(0.0f, 4.0f)};

        // Debug.Log("Distance: " + distance);

        if (distance < 5) {
            Instantiate(FlameTrapPrefab, cursorPos, Quaternion.identity);
            Shaker.ShakeCamera(0.15f, 0.5f);
            StartCoroutine(Delay(cursorPos, vecArray[0], 0.2f)); 
            StartCoroutine(Delay(cursorPos, vecArray[1], 0.4f));
            StartCoroutine(Delay(cursorPos, vecArray[2], 0.6f));
            StartCoroutine(Delay(cursorPos, vecArray[3], 0.8f)); 
            coolScript.startingFire();
            isCooldownFire = true;  
        }
    }

    void ShootIce() {
        GameObject iceBullet = Instantiate(icePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = iceBullet.GetComponent<Rigidbody2D>();
        rb.rotation = lookAngle;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Shaker.ShakeCamera(0.05f, 0.1f);
    }

    void ShootIceMist() {

        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(transform.position, cursorPos);

        if (distance < 7) {
            GameObject IceMist = Instantiate(IceMistPrefab, cursorPos, Quaternion.identity);
            Shaker.ShakeCamera(0.15f, 0.5f);
        coolScript.startingIce();
        isCooldownIce = true;
        }
    }

    // Function for switching player mode
    void Switch() {
        if (Input.GetButtonDown("FireMode")) {
            fireMode = true;
            iceMode = false;
            windMode = false;
        }

        if (Input.GetButtonDown("IceMode") && iceUnlock == true) {
            fireMode = false;
            iceMode = true;
            windMode = false;
        }

        if (Input.GetButtonDown("WindMode") && windUnlock == true) {
            fireMode = false;
            iceMode = false;
            windMode = true;
        }


    }

    private IEnumerator Delay(Vector2 skillPos, Vector2 adjust, float wait) {
        yield return new WaitForSeconds(wait);
        Instantiate(FlameTrapPrefab, cursorPos - adjust, Quaternion.identity);
    }
}
