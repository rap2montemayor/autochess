using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorHealer : Activator
{

    private bool doOnce = false;

    public override void Activate()
    {
        if (!doOnce){
            Dialogue.SayLine("Your units have been healed");
            HealAllUnits();
            doOnce = true;
        } 
    }

    private void HealAllUnits(){
        foreach (Unit unit in Inventory.instance.board){
            if (unit != null){
                unit.hp_current = unit.hp_max;
            }
        }
        foreach (Unit unit in Inventory.instance.bench){
            if (unit != null){
                unit.hp_current = unit.hp_max;
            }
        }
        foreach (Unit unit in Inventory.instance.units){
            if (unit != null){
                unit.hp_current = unit.hp_max;
            }
        }
    }
}
