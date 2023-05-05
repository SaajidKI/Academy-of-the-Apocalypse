using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTornadoSkill : MonoBehaviour
{
    public float knockBackForce = 20f;
    public Rigidbody2D rb2D;  

    private float time = 0.0f;
    private float nextSwitchTime = 0.0f;
    private float xPos;
    private float yPos;
    private bool Side; 
    private float add = 0.01f;
    private float subtract = -0.01f;
    private float yAxis = 0.001f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObject(gameObject));
        player = GameObject.FindGameObjectWithTag("Player");
        Side = player.GetComponent<PlayerMovement>().playerSide();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (Side == true) {
            xPos = transform.position.x + add;
            yPos = transform.position.y - yAxis;
        }

        if (Side == false) {
            xPos = transform.position.x + subtract;
            yPos = transform.position.y - yAxis;
        }

        if (time > nextSwitchTime && Side == true) {
            add = -(add);
            nextSwitchTime = time + 1f;
        }

        if (time > nextSwitchTime && Side == false) {
            subtract = -(subtract);
            nextSwitchTime = time + 1f;
        }

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            other.GetComponent<EnemyMeleeDamage>().TakeDamage(20);

            Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
            pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
            StartCoroutine(EndKnockBack(pushRB));
        }

        if (other.gameObject.tag == "S_Enemy") {
            other.GetComponent<EnemyMeleeDamage>().TakeDamage(20);

            Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
            pushRB.AddForce(moveDirectionPush.normalized * (knockBackForce / 3f) * - 1f, ForceMode2D.Impulse);
            StartCoroutine(EndKnockBack(pushRB));
        }
    }

    private IEnumerator DestroyObject(GameObject bullet) {
        yield return new WaitForSeconds(4f);
        Destroy(bullet);
    }

    private IEnumerator EndKnockBack(Rigidbody2D otherRB) {
        yield return new WaitForSeconds(0.2f);
        otherRB.velocity= Vector3.zero;
    }
}
