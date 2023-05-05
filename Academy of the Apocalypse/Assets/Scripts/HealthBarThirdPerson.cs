using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBarThirdPerson : MonoBehaviour {

    //private bool isAlive = true;

      public float startHealth = 100;
      private float health;
      //public GameObject deathEffect;
      public Image healthBar;
      public Color healthyColor = new Color(0.3f, 0.8f, 0.3f);
      public Color unhealthyColor = new Color(0.8f, 0.3f, 0.3f);
      public GameHandler gameHandler;


      private void Start () {
        if (GameObject.FindWithTag ("GameHandler") != null) {
            gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler>();
            }
            health = gameHandler.StartPlayerHealth;
            Debug.Log("The starting health is: " + health);
      }

      public void SetColor(Color newColor){
            healthBar.GetComponent<Image>().color = newColor;
      }

      public void TakeDamage (){
            health = gameHandler.playerHealth;
            healthBar.fillAmount = health / startHealth;
            //turn red at low health:
            if (health < 0.3f){
                  if ((health * 100f) % 3 <= 0){
                        SetColor(Color.white);
                        Die();
                  }
                  else {
                        SetColor(unhealthyColor);
                  }
            }
            else {
                  SetColor(healthyColor);
            }
      }



      public void Die(){
            Debug.Log("You Died So Much");
            //isAlive = false;
           // death stuff. change scene? how about a particle effect?
            //Vector3 objPos = this.transform.position
            //Instantiate(deathEffect, objPos, Quaternion.identity) as GameObject;
           //SceneManager.LoadScene ("Scene_lose");
      }

}
