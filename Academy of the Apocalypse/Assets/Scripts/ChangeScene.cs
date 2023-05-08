using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load
    public float timer;

    void Start()
    {
        
        Invoke("ChangeToNewScene", timer); // Start the timer to change the scene after 6 seconds
    }

    void ChangeToNewScene()
    {
        SceneManager.LoadScene(nextSceneName); // Load the next scene
    }
}
