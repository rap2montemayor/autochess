using System;
using UnityEngine;

[Serializable]
public abstract class Data : ScriptableObject {
    public string name_data;
    public Sprite icon;
    public string description;

    public abstract string GetSpecialDescription();
}