using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public void Setup(int score){
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartButton(){
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuButton(){
        SceneManager.LoadScene("Menu");
    }

    public void QuitButton(){
        Debug.Log("Quitting");
        Application.Quit();

    }
}
