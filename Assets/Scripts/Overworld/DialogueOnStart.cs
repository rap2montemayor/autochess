using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DialogueOnStart : MonoBehaviour
{
    public string inkStoryDialogue;
    void Start()
    {
        Dialogue.StartDialogue(inkStoryDialogue);
    }
}
