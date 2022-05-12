using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit")]
public class Unit : Data {

    [SerializeField]
    public int hp_max;
    public int hp_current;

    [SerializeField]
    public int amr_max;
    public int amr_current;

    [SerializeField]
    public int atkphys_max;
    public int atkphys_current;

    [SerializeField]
    public int crt_max;
    public int crt_current;

    [SerializeField]
    public int range;
    
    [SerializeField]
    public int evasion;

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