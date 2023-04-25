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

      public Camera cam;

      Vector2 mousePos;

      void Start(){
           anim = gameObject.GetComponent<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
           if (isAlive == true){
                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

                  if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
                      anim.SetBool ("isWalking", true);
                      if (!WalkSFX.isPlaying){
                            //WalkSFX.Play();
                      }
                  } else {
                      anim.SetBool ("isWalking", false);
                      WalkSFX.Stop();
                 }

            }

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            // Vector3 playerPos = transform.localPosition
            // float playerPosX = playerPos.x;

            // Vector3 ScaleVal = transform.localScale;
            // float ScaleDeter = ScaleVal.x;

            // Debug.Log(mousePos.x);

            // if (mousePos.x > 0 && ScaleDeter > 0) {
            //       playerTurn();
            // }

            // if (mousePos.x < 0 && ScaleDeter < 0) {
            //       playerTurn();
            // }

            Vector2 lookDir = mousePos - rb2D.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            angle = Mathf.RoundToInt(angle);
            // // rb2D.rotation = angle;

            // if (angle == 0 || angle == -180) {
            //       if (turned == false) {
            //             playerTurn();
            //             turned = true;
            //       }
            // }

            // if (angle == -1 || angle == -179) {
            //       turned = false;
            // }

            if ((angle > 0 && angle < 90) || (angle > -270 && angle < -180)) {
                  if (turned == false) {
                        playerTurn();
                        turned = true; 
                  }
                  // if (angle > -270 && angle < -180) {
                  //       Debug.Log("HERE!");
                  //       playerTurn();
                  // }
            }

            if (angle < 0 && angle > -180) {
                  if (turned == true) {
                        playerTurn();
                        turned = false;
                  }
            }



      }

      public void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1f;
            transform.localScale = theScale;
      }

}