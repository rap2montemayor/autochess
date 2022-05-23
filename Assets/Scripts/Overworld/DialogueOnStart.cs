using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DialogueOnStart : MonoBehaviour
{
    public string inkStoryDialogue;

    private static bool doOnce = false;
    void Start()
    {
        if (!doOnce){
            Dialogue.StartDialogue(inkStoryDialogue);
            doOnce = true;
        }
    }
}
