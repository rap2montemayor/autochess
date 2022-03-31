using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit")]
public class Unit : Data {

    [SerializeField]
    private int hp_max;
    private int hp_current;

    [SerializeField]
    private int amr_max;
    private int amr_current;

    [SerializeField]
    private int res_max;
    private int res_current;

    [SerializeField]
    private int atkphys_max;
    private int atkphys_current;

    [SerializeField]
    private int atkmag_max;
    private int atkmag_current;

    [SerializeField]
    private int spd_max;
    private int spd_current;

    [SerializeField]
    private int crt_max;
    private int crt_current;

    [SerializeField]
    public bool isEnemy;

    public void ModHP(int hp_mod){
        hp_current += hp_mod;
        if (hp_current > hp_max){
            hp_current = hp_max;
        }
        if (hp_current < 0){
            // UNIT DIES! DO SOMETHING
        }
    }

    public override string GetSpecialDescription(){
        return ("UNIT STATS: [pls edit GetSpecialDescription to display stats]");
    }
}