using UnityEngine;

public class Inventory : MonoBehaviour{
    public const int MAX_BENCH_UNITS =  8;
    public const int MAX_INV_ITEMS   = 40;
    public const int MAX_BOARD_UNITS = 16;
    public const int BOARD_PRESET_W =  8;
    public const int BOARD_PRESET_H =  4;

    [SerializeField]
    public InvList<Unit> units {get; private set;}
    public InvList<Unit> bench {get; private set;}
    public InvList<Unit> board {get; private set;}
    public InvList<Item> items {get; private set;}

    public InventoryDatabase database;

    // Singleton Pattern
    public static Inventory instance {get; private set;}

    void Awake(){
        
        // "Use Oldest Instance" mode
        if (instance == null){
            instance = this;
            LoadData();
        }else{
            Destroy(this.gameObject);
            Destroy(this);
        }
        

        // Use youngest instance mode
        /*
        Destroy(instance);
        instance = this;
        LoadData();
        */
    }

    /* do not delete
    //Load Data by creating new inventory lists
    private void LoadData{
        units = new InvList<Unit>(MAX_INV_ITEMS);
        bench = new InvList<Unit>(MAX_BENCH_UNITS);
        board = new InvList<Unit>(MAX_BOARD_UNITS);
        items = new InvList<Item>(MAX_INV_ITEMS);
    }

    // Load Data via Save/Load
    // to be implemented
    */

    //Load Data by reading from Inventory Manager
    private void LoadData(){
        units = InvList<Unit>.LoadFrom(database.units, MAX_INV_ITEMS);
        bench = InvList<Unit>.LoadFrom(database.bench, MAX_BENCH_UNITS);
        board = InvList<Unit>.LoadFrom(database.board, BOARD_PRESET_W * BOARD_PRESET_H);
        items = InvList<Item>.LoadFrom(database.items, MAX_INV_ITEMS);
    }

    public bool InventoryToBench(int index){
        return (bench.Add(units.At(index)) && units.Remove(index));
    }

    public bool BenchToInventory(int index){
        return (units.Add(bench.At(index)) && bench.Remove(index));
    }
    
    public bool BenchToBoard(int index){
        return (board.Add(bench.At(index)) && bench.Remove(index));       
    }

    public bool BoardToBench(int index){
        return (bench.Add(board.At(index)) && board.Remove(index));
    }

    public bool InventoryToBoard(int index){
        return (board.Add(units.At(index)) && units.Remove(index));     
    }

    public bool BoardToInventory(int index){
        return (units.Add(board.At(index)) && board.Remove(index));     
    }
    
    public int BoardCoordToIndex(int x, int y){
        return (BOARD_PRESET_W * y) + x;
    }
}
