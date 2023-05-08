using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTextTrigger : MonoBehaviour
{

    public GameObject text;
    public GameObject trigger;
    public AudioSource triggerSound;
    private int counter = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        // Add your custom logic here
        
        if (other.CompareTag("Player") && counter == 1)
        {
            text.SetActive(true);
            triggerSound.Play();
            counter --;
        }
        

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger exited by: " + other.gameObject.name);

            text.SetActive(false);
            trigger.SetActive(false);

    }
}
