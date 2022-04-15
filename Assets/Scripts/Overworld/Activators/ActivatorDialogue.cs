public class ActivatorDialogue : Activator
{
    public string inkDialogueEntry;
    
    public override void Activate(){
        Dialogue.StartDialogue(inkDialogueEntry);
    }
}