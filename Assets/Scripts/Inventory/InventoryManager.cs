using UnityEngine;

// I hate this code, it needs to be refactored to split inventory and enemy and eventually the shop

// controls inventory ui
[RequireComponent (typeof (CanvasGroup))]
public class InventoryManager : MonoBehaviour {

    // Standard Inventory
    public UnitInvWindow inventory_grid;
    public UnitInvWindow bench_grid;
    public UnitInvWindow board_grid;
    public ItemInvWindow item_grid;
    public TextBox inventory_info; 

    //Enemy Preview Mode. Just saying I don't like this, this should be implemented as a derived class instead of an all-in-one
    public UnitInvWindow enemy_grid;
    public EnemyData enemyData;

    //Other Stuff
    public bool isVisible {get; private set;}
    public bool canToggleVisibility;
    private CanvasGroup canvasGroup;


    public void Start() {
        canToggleVisibility = true;
        canvasGroup = GetComponent<CanvasGroup>();
        if (inventory_grid != null){
            inventory_grid.SetData(Inventory.instance.units);
            inventory_grid.GenerateSlots();
        }
        if (bench_grid != null){
            bench_grid.SetData(Inventory.instance.bench);
            bench_grid.GenerateSlots();   
        }   
        if (board_grid != null){
            board_grid.SetData(Inventory.instance.board);
            board_grid.GenerateSlots();   
        }   
        if (item_grid != null){
            item_grid.SetData(Inventory.instance.items);
            item_grid.GenerateSlots();
        }

        if(enemy_grid != null){
            enemy_grid.SetData(enemyData.units);
            enemy_grid.GenerateSlots();
            enemy_grid.SetInteractable(false);
            canToggleVisibility = false;
        }

        Hide();
    }

    //don't really like this but oh well
    public void Update(){
        if (Input.GetKeyDown(KeyCode.Tab) && canToggleVisibility){
            if (isVisible){
                Hide();
            }else{
                Show();
            }
            
        }
    }

    public void DisplayUpdate(){
        if (inventory_grid != null){
            inventory_grid.DisplayUpdate();
        }
        if (bench_grid != null){
            bench_grid.DisplayUpdate();
        }
        if (board_grid != null){
            board_grid.DisplayUpdate();
        }        
        if (item_grid != null){
            item_grid.DisplayUpdate();
        }
    }

    public void Hide(){
        if (canToggleVisibility){
            isVisible = false;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;

            foreach (Transform child in transform){
                child.gameObject.SetActive(false);
            }
        }
    }

    public void Show(){
        if (canToggleVisibility){
            isVisible = true; 
            foreach (Transform child in transform){
                child.gameObject.SetActive(true);
            }
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            DisplayUpdate();
        }
    }

    public void DisplaySlotInfo(Slot slot){
        if (slot != null && inventory_info != null){
            // we're checking to see what list the slot came from
            string lname = slot.GetParentListName();
            string entry_name = "";
            string entry_desc = "";
            if (lname == GetUnitsName()){
                entry_name = Inventory.instance.units.At(slot.index).name_data;
                entry_desc = Inventory.instance.units.At(slot.index).description;
            }
            else if (lname == GetBenchName()){
                entry_name = Inventory.instance.bench.At(slot.index).name_data;
                entry_desc = Inventory.instance.bench.At(slot.index).description;
            }
            else if (lname == GetBoardName()){
                entry_name = Inventory.instance.board.At(slot.index).name_data;
                entry_desc = Inventory.instance.board.At(slot.index).description;
            }
            else if (lname == GetItemName()){
                entry_name = Inventory.instance.items.At(slot.index).name_data;
                entry_desc = Inventory.instance.items.At(slot.index).description;
            }
            else{
                Debug.Log("you should never see this, even in debug mode");
            }
            Debug.Log(entry_name + " " + entry_desc);
            inventory_info.DisplayTextSlowly(entry_name + " " + entry_desc);
        }
    }

    public string GetUnitsName(){
        if (inventory_grid != null){
            return inventory_grid.transform.name;
        }else{
            return "";
        }
    }
    public string GetBenchName(){
        if (bench_grid != null){
            return bench_grid.transform.name;
        }else{
            return "";
        }
    }

    public string GetBoardName(){
        if (board_grid != null){
            return board_grid.transform.name;
        }else{
            return "";
        }
    }

    public string GetItemName(){
        if (item_grid != null){
            return item_grid.transform.name;
        }else{
            return "";
        }
    }
}
