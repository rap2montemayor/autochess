using UnityEngine;
public class ActivatorSpeaker : ActivatorDialogue
{
    public string[] speakerName;
    public Color[] nameColor;

    public override void Activate(){
        Dialogue.StartDialogue(this);
    }
}