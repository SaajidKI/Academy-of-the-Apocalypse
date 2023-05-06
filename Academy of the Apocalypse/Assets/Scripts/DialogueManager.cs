using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    private Queue<string> sentences;
    public GameObject button;
    public GameObject StartWalkingButton;
    public GameObject dialogBox;


    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        //Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        dialogText.text = sentence;
    }

    void EndDialogue(){
        Debug.Log("Ending conversation");
        button.SetActive(false);
        dialogBox.SetActive(false);
        StartWalkingButton.SetActive(true);
    }
}
