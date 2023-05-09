using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switching : MonoBehaviour
{
    //public AudioSource _clip;
    public Sprite playerFire;
    public Sprite playerIce;
    public Sprite playerWind;
    public Animator animator;
    public bool iceUnlock = false;
    public bool windUnlock = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = playerFire;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireMode")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerFire;
            animator.SetBool ("toFire", true); 
            animator.SetBool ("toIce", false);
            animator.SetBool ("toWind", false);
        }

        if (Input.GetButtonDown("WindMode") && iceUnlock == true) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerWind;
            animator.SetBool ("toFire", false); 
            animator.SetBool ("toIce", false);
            animator.SetBool ("toWind", true);
        } 

        if (Input.GetButtonDown("IceMode") && windUnlock == true) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerIce;
            animator.SetBool ("toIce", true); 
            //_clip.Play();
            animator.SetBool ("toFire", false);
            animator.SetBool ("toWind", false);
        }
    }
}
