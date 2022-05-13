using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit")]
public class Unit : Data {

    public int hp_max;
    [HideInInspector]
    public int hp_current;

    public int amr_max;
    [HideInInspector]
    public int amr_current;

    public int atkphys_max;
    [HideInInspector]
    public int atkphys_current;

    public int crt_max;
    [HideInInspector]
    public int crt_current;

    [SerializeField]
    public int range;
    
    [SerializeField]
    public int evasion;

    public float attack_cooldown = 1;

    [SerializeField]
    public bool isEnemy;



    public void ModHP(int hp_mod){
        hp_current += hp_mod;
        if (hp_current > hp_max){
            hp_current = hp_max;
        }
        if (hp_current < 0){
            hp_current = 0;
        }
    }

    public override string GetSpecialDescription(){
        return ("UNIT STATS: [pls edit GetSpecialDescription to display stats]");
    }

    public Unit Copy() {
        return (Unit) this.MemberwiseClone();
    }
}