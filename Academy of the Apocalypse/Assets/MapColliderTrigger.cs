using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColliderTrigger : MonoBehaviour
{
    public GameObject SecondSkillButton;
    public AudioSource triggerSound;
    private int counter = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        // Add your custom logic here
        
        if (other.CompareTag("Player") && counter == 1)
        {
            SecondSkillButton.SetActive(true);
            triggerSound.Play();
            counter --;
        }
        

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger exited by: " + other.gameObject.name);
        // Add your custom logic here
        //SecondSkillButton.SetActive(false);
    }
}
