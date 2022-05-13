using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorBattle : Activator
{
    public string battle_name = "Battle";
    private bool doOnce = false;

    public override void Activate()
    {
        if (!doOnce){
            SceneLoader.LoadScene(battle_name);
            doOnce = true;
        }
    }
}
