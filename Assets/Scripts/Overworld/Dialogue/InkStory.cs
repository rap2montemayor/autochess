using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkStory : MonoBehaviour
{
    // ------------------------------------
    // SINGLETON PATTERN
    // ------------------------------------
    private static InkStory instance;

    public static InkStory GetInkStory(){
        return instance;
    }
    void Awake(){
        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
        inkStory = new Story(textAsset.text);
    }

    // ------------------------------------
    // VARIABLES
    // ------------------------------------
    public TextAsset textAsset;
    private Story inkStory;

    // ------------------------------------
    // METHODS
    // ------------------------------------
    
    public bool CanContinue(){
        return inkStory.canContinue;
    }
    public bool CanChoose(){
        return (inkStory.currentChoices.Count > 1);
    }
    public int GetChoicesCount(){
        return inkStory.currentChoices.Count;
    }
    public void ContinueMaximally(){ //USE ONLY IF NO MORE CHOICES
        inkStory.ContinueMaximally();
    }
    //REPLACE THIS WITH BETTER SELECT CHOICE SYSTEM
    public string GetChoiceText(int n){
        Choice choice = inkStory.currentChoices[n];
        return "<i>What\'s your response?</i>\n" + (n+1) + ")" + choice.text;
    }
    public void ChooseChoice(int n){
        inkStory.ChooseChoiceIndex(n);
    }
    public void SelectSection(string path){
        inkStory.ChoosePathString(path);
    }
    public string Continue(){
        return inkStory.Continue();
    }
    public List<string> GetTags(){
        return inkStory.currentTags;
    }
    public string GetJSON(){
        return inkStory.state.ToJson();
    }
    public void SetJSON(string json){
        if (json != ""){
            inkStory.state.LoadJson(json);
        }
    }
    
}