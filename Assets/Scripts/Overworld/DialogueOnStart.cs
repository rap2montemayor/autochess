using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DialogueOnStart : MonoBehaviour
{
    public string inkStoryDialogue;
<<<<<<< Updated upstream
    void Start()
    {
        Dialogue.StartDialogue(inkStoryDialogue);
=======
    static bool doOnce = false;
    void Start()
    {
        if(!doOnce){
            Dialogue.StartDialogue(inkStoryDialogue);
            doOnce = true;
        }
>>>>>>> Stashed changes
    }
}
