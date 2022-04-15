using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Fade))]
public class VisibleInEditorOnly : MonoBehaviour
{
    void Awake(){
        Fade fader = gameObject.GetComponent(typeof(Fade)) as Fade;
        if (fader == null){
            fader = gameObject.AddComponent(typeof(Fade)) as Fade;
        }
        fader.FadeOut(0f);
    }
}