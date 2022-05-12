using System.Collections;
using UnityEngine;

/*
PRESS SPACEBAR TO ENTER DIALOGUE / NEXT DIALOGUE

*/

public class Dialogue : MonoBehaviour
{
    // ------------------------------------
    // SINGLETON PATTERN
    // ------------------------------------
    private static Dialogue dialogue;

    void Awake(){                           //SINGLETONS
        if (dialogue == null){
            dialogue = this;
            //setup
            story = InkStory.GetInkStory();
            lastPressTime = Time.fixedTime;
            character = null;
        }else{
            Destroy(this);
        }
    }

    // ------------------------------------
    // VARIABLES
    // ------------------------------------
    //Dialogue State

    private bool inUse = false;
    private bool disabledSkip = false;
    private bool inOneLiner = false;

    //Choice State
    private bool inChoice = false;
    private int currentChoice = 0;

    //Speaker character data
    private ActivatorSpeaker character;
    private int currentSpeakerID = 0;

    //Delay between input
    private float lastPressTime;

    //Next line of dialogue to display
    private string nextLine;

    //Externals
    private InkStory story;
    public TextBox textBox;
    
    
    void Start(){

    }

    void Update(){
        if (inUse){
            if (Input.GetKeyDown(KeyCode.Space) && InputDelayIsDone()){
                NextDialogue();
            }else if (inChoice && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && InputDelayIsDone()){
                Choice();
            }
        }
        else if (inOneLiner){
            if (Input.GetKeyDown(KeyCode.Space) && textBox.ShowTextDone() && InputDelayIsDone()){
                inOneLiner = false;
                EndDialogue();
            }
        }
    }
    // ------------------------------------
    // SAY ONE-LINER
    // ------------------------------------
    public static void SayLine(string line){
        dialogue.ResetInputDelay();
        dialogue.inOneLiner = true;
        dialogue.textBox.ShowBox();
        PlayerController.deactivate();
        dialogue.textBox.DisplayTextSlowly(line);
    }

    // ------------------------------------
    // START DIALOGUE
    // ------------------------------------
    public static void StartDialogue(string inkDialogueEntry){
        if (!dialogue.inUse){
            dialogue.story.SelectSection(inkDialogueEntry);            
            dialogue.inUse = true;
            PlayerController.deactivate();
            dialogue.textBox.ShowBox();
            dialogue.NextDialogue(); 
        }
    }
    
    public static void StartDialogue(ActivatorSpeaker speaker){
        dialogue.character = speaker;
        dialogue.currentSpeakerID = 0;
        StartDialogue(speaker.inkDialogueEntry);
    }

    // ------------------------------------
    // CONTINUE DIALOGUE
    // ------------------------------------


    private void NextDialogue(){
        //IF DIALOGUE NOT FINISHED DISPLAYING, DISPLAY NOW
        if (!textBox.ShowTextDone()){ 
            if (!disabledSkip){textBox.DisplayTextNow(nextLine);}
            return;
        }
        //IF STORY HAS CHOICES
        if (story.CanChoose()){ 
            if (!inChoice){
                inChoice = true;
                currentChoice = 0;
                textBox.DisplayTextNow(story.GetChoiceText(currentChoice) + GetChoiceInstructions());
                return;
            }else{
                story.ChooseChoice(currentChoice);
                inChoice = false;
            }
        }
        //IF NEXT DIALOGUE CAN SHOW
        if (story.CanContinue()){
            disabledSkip = false;
            nextLine = story.Continue();
            ProcessTags();
            if (character!= null && !inChoice){nextLine = GetSpeakerName() + nextLine;} //speaker name is added to next.
            textBox.DisplayTextSlowly(nextLine);
            return;
        }
        // IF NONE OF THE ABOVE, END DIALOGUE    
        EndDialogue();
    }

    // ------------------------------------
    // END DIALOGUE
    // ------------------------------------
    private void EndDialogue(){
        textBox.HideBox();
        inUse = false;
        PlayerController.activate();
    }

    // ------------------------------------
    // PROCESS TAGS
    // ------------------------------------
    private void ProcessTags(){
        //TAG ANATOMY
        //FNAME:FPARAM^FPARAM
        foreach (string rawTag in story.GetTags()){
            string[] tag = rawTag.Split(':');
            switch (tag[0]){
                case "NOSKIP":
                    disabledSkip = true;
                break;
                case "SETSPEAKER":
                    currentSpeakerID = int.Parse(tag[1]);
                break;
                default:
                    Debug.Log("INVALID TAG NAME " + tag[0]);
                break;
            }
            
        }
    }
    // ------------------------------------
    // CHOICE INPUTS (pls replace asap)
    // ------------------------------------
    public void Choice(){
        Vector2 input = new Vector2();
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x == 1 || input.y == 1){
            currentChoice = ((currentChoice + 1) % story.GetChoicesCount());           
        }else if (input.x == -1 || input.y == -1){
            currentChoice = ((currentChoice - 1 + story.GetChoicesCount()) % story.GetChoicesCount()); 
        }
        textBox.DisplayTextNow(story.GetChoiceText(currentChoice) + GetChoiceInstructions());
        
    }

    // ------------------------------------
    // INPUT SPACER (ensures a minimum 0.1 seconds between inputs)
    // do not call in update loop, unless it is after an input check
    // do not use as exit condition in normal loop. ever.
    // ------------------------------------
    private bool InputDelayIsDone(){
        return InputDelayIsDone(0.1f);
    }
    private bool InputDelayIsDone(float delay_time){
        if (Time.fixedTime - lastPressTime >= delay_time){
            lastPressTime = Time.fixedTime;
            return true;
        }else{
            return false;
        }
    }
    private void ResetInputDelay(){
        lastPressTime = Time.fixedTime;
    }

    // ------------------------------------
    // TEXT STYLING FUNCTIONS
    // ------------------------------------
    private string GetSpeakerName(){
        return "<b><color=#" + ColorUtility.ToHtmlStringRGB(character.nameColor[currentSpeakerID]) + ">" + character.speakerName[currentSpeakerID] + "</color></b>" + ": ";
    }

    private string GetChoiceInstructions(){
        return " <i> (use WASD to select a different option) </i>";
    }
}