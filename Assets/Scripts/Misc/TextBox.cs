using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(CanvasGroup))]
public class TextBox : MonoBehaviour
{
    /*
    Sorry. This class's interface is a little confusing. You have to manually show and hide the box, separately from displaying text from it.
    This was intentional, because the box doesn't know when it's finished talking, or when to display something else.
    */
    
    public Text boxText;
    //public AudioClip click;

    private CanvasGroup fader;
    private bool boxActive; // is the box currently being displayed? 

    private Coroutine showingText;
    
    
    private static readonly List<char> pauseIfInText = new List<char>{'.', ',','?','!'};

    void Awake(){
        fader = GetComponent<CanvasGroup>();
        fader.alpha = 0;
        boxActive = false;
    }

    public void ShowBox(){
        boxActive = true;
        fader.alpha = 1;
    }

    public void HideBox(){
        boxActive = false;
        boxText.text = "";
        fader.alpha = 0;
    }

    public void DisplayTextSlowly(string text){
        showingText = StartCoroutine(ShowText(text));
    }
    public bool ShowTextDone(){
        return (showingText == null);
    }
    public bool BoxActive(){
        return boxActive;
    }

    public void DisplayTextNow(string text){
        if (showingText != null){
            StopShowText();
        }
        boxText.text = text;
    }

    private IEnumerator ShowText(string text){
        boxText.text = "";
        string outputText = "";
        List<string> openTags = new List<string>();

        for (int i = 0; i < text.Length; i++){
            char letter = text[i];

            if (letter == '<'){ // richtext. Please, please use proper format OR IT WILL NOT WORK.
                string richText = "<";
                string tagName = "";
                bool tagNameEnded = false;
                while (text[i] != '>'){ //gets tag
                    i++;
                    if (tagNameEnded == false && ( text[i] == '=' || text[i] == '>')){
                        tagName = richText.Substring(1, richText.Length - 1);
                        tagNameEnded = true;
                        }
                    richText += text[i];
                }

                if (openTags.Contains(tagName.Substring(1, tagName.Length - 1))){ //if tag closes another tag. substring removes /
                    openTags.Remove(tagName.Substring(1, tagName.Length - 1));
                }else{
                    openTags.Add(tagName); //if new tag
                }

                outputText += richText;

            }else{
                //AudioBoss.playOneShot(click, 0.1f);
                outputText += letter;
                boxText.text = outputText;
                for(int j = openTags.Count; j > 0; j--){
                    boxText.text += "</" + openTags[j - 1] + ">";
                }

                if (pauseIfInText.Contains(letter)){
                    yield return new WaitForSeconds(0.25f);    
                }else if (openTags.Count > 0){
                    yield return new WaitForSeconds(0.015f); 
                }else{
                    yield return new WaitForSeconds(0.03f); 
                }
            }
        }
        showingText = null;
    }
 
    private void StopShowText(){
        StopCoroutine(showingText);
        showingText = null;
    }
}