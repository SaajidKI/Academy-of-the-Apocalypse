using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      public GameHandler gameHandler;
    //   public GameObject player;
    //   public PlayerAttackMelee Attack_Script;
      //public playerVFX playerPowerupVFX;
      public bool isHealthPickUp = true;
      public bool isAttackPickUp = false;
    //   public bool isSpeedBoostPickUp = false;

      public int healthBoost = 50;
    //   public float speedBoost = 2f;
    //   public float speedTime = 2f;

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                //   GetComponent<AudioSource>().Play();
                  StartCoroutine(DestroyThis());

                  if (isHealthPickUp == true) {
                        gameHandler.playerGetHit(healthBoost * -1);
                        //playerPowerupVFX.powerup();
                  }

                  if (isAttackPickUp == true) {
                    //   Attack_Script = player.GetComponent<PlayerAttackMelee>().AttackUp();
                    other.gameObject.GetComponent<PlayerAttackMelee>().AttackUp();
                  }

                //   if (isSpeedBoostPickUp == true) {
                //         other.gameObject.GetComponent<PlayerMove>().speedBoost(speedBoost, speedTime);
                //         //playerPowerupVFX.powerup();
                //   }
            }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}