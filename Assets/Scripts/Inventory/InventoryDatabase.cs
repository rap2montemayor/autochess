using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Inventory Database", menuName = "ScriptableObjects/Inventory Database")]
public class InventoryDatabase : ScriptableObject {
    public Unit[] units = new Unit[Inventory.MAX_INV_ITEMS];
    public Unit[] bench = new Unit[Inventory.MAX_BENCH_UNITS];
    public Unit[] board = new Unit[Inventory.BOARD_PRESET_W * Inventory.BOARD_PRESET_H];
    public Item[] items = new Item[Inventory.MAX_INV_ITEMS];
}
