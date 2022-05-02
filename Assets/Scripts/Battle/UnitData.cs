// UNIT DATA & APPEARANCE IN BATTLE

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class UnitData : MonoBehaviour {
    public Unit unit_data;

    public void ModHP(int hp_mod){
        unit_data.ModHP(hp_mod);
    }

    public void LoadSprite(){
        gameObject.GetComponent<SpriteRenderer>().sprite = unit_data.icon;
    }
}