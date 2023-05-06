using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

      public Animator anim;
      public AudioSource WalkSFX;
      public Rigidbody2D rb2D;
      private bool FaceRight = true; // determine which way player is facing.
      public static float runSpeed = 10f;
      public float startSpeed = 10f;
      public bool isAlive = true;
      private bool turned = true;
      public bool walking = true;
      public float dashSpeed = 5.0f;
      public float dashDuration = 0.1f;
      public float dashCooldown = 0.5f;

      private bool isDashing = false;
      private float dashTimer;
      private float dashCooldownTimer;

      public Camera cam;

      Vector2 mousePos;

      void Start(){
           anim = gameObject.GetComponent<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
           dashTimer = 0f;
           dashCooldownTimer = 0f;
      }

      void Update(){

            if (walking == true) {
                  Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
                  if (isAlive == true) {
                        if (!isDashing) {
                              transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;
                        }

                        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)) {
                              anim.SetBool ("isWalking", true);
                              if (!WalkSFX.isPlaying){
                                    WalkSFX.Play();
                              }
                        } else {
                              anim.SetBool ("isWalking", false);
                              WalkSFX.Stop();
                        }
                  }

                  if (Input.GetButtonDown("Dash") && !isDashing && dashCooldownTimer <= 0) {
                        StartCoroutine(Dash());
                  }

                  if (isDashing) {
                        dashTimer -= Time.deltaTime;
                        if (dashTimer <= 0) {
                              isDashing = false;
                        }
                  }
                  else if (dashCooldownTimer > 0) {
                        dashCooldownTimer -= Time.deltaTime;
                  }


                  mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

                  Vector2 lookDir = mousePos - rb2D.position;
                  float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                  angle = Mathf.RoundToInt(angle);

                  if ((angle > 0 && angle < 90) || (angle > -270 && angle < -180)) {
                        if (turned == false) {
                              playerTurn();
                              turned = true; 
                        }
                  }

                  if (angle < 0 && angle > -180) {
                        if (turned == true) {
                              playerTurn();
                              turned = false;
                        }
                  }
            

                  if (Input.GetButtonDown("Fire2")) {
                        //anim.SetBool("shooting", true);
                        anim.SetTrigger("shooting");
                  } else {
                        //anim.SetBool("shooting", false);
                  }
            }
      }

      public void enable_Walking() {
            walking = true;
      }

      public void disable_Waling() {
            walking = false;
      }

      public void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1f;
            transform.localScale = theScale;
      }

      public bool playerSide() {
            return FaceRight;
      }

      IEnumerator Dash() {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;

            Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            if (dashDirection == Vector2.zero)
            {
            dashDirection = FaceRight ? Vector2.left : Vector2.right;
            }

            rb2D.velocity = dashDirection * dashSpeed;
            yield return new WaitForSeconds(dashDuration);

            rb2D.velocity = Vector2.zero;
            isDashing = false;
      }

}