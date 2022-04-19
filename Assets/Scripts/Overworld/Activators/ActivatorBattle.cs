using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorBattle : Activator
{
    public override void Activate()
    {
        SceneLoader.LoadScene("AutoChess");
    }
}
