using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : Data {
    public override string GetSpecialDescription(){
        return ("ITEM STATS: [pls edit GetSpecialDescription to display stats]");
    }
}