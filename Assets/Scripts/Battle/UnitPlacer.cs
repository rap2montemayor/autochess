using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacer : MonoBehaviour
{

    private int unit_size = 1;
    private int board_width = 8;
    private int board_height = 4;

    public GameObject unit_prefab;

    // Start is called before the first frame update
    void Start()
    {
        int unit_num = 0;
        foreach (Unit player_unit in Inventory.instance.board){
            if (player_unit != null){
                float x_pos = unit_size * ((unit_num % board_width) - (board_width / 2));
                float y_pos = unit_size * (Mathf.Floor(unit_num / board_height) - board_height / 2);
                Vector3 position = new Vector3(x_pos, y_pos, 0);
                GameObject new_unit = Instantiate(unit_prefab, position, Quaternion.identity, transform);
                
                // load the appropriate unit data from the inventory

                UnitData unit_data = GetComponent<UnitData>();
                if (unit_data == null){
                    unit_data = new_unit.AddComponent<UnitData>();
                } 
                unit_data.unit_data = player_unit;

                // then load the sprite
                unit_data.LoadSprite();
                unit_num++;
            }
            //unit_num++;
        }
    }
}
