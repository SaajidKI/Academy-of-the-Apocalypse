using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switching : MonoBehaviour
{
    public Sprite playerFire;
    public Sprite playerIce;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = playerFire;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireMode")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerFire;
        }

        if (Input.GetButtonDown("IceMode")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerIce;
        }
    }
}
