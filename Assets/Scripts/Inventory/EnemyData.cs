using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject {

    public const int ENEMY_DATA_LEN = 32;
    public Unit[] units = new Unit[Inventory.MAX_INV_ITEMS];
}