using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot: MonoBehaviour, IPointerClickHandler {

    public int index {get; private set;}
    public bool interactable;

    [SerializeField]
    private GameObject dragUI_prefab;

    [SerializeField]
    private Image icon;
    private Sprite default_icon;

    private RectTransform rectTransform;

    private static GameObject selected_obj;
    public static Slot selected_slot {get; private set;}

    private static Canvas canvas;
    private static InventoryManager manager;

    public void Awake(){
        if (manager == null){
            manager = FindObjectOfType<InventoryManager>();
        }
        index = transform.GetSiblingIndex();
        default_icon = icon.sprite;
        rectTransform = GetComponent<RectTransform>();
        interactable = true;
    }

    public string GetParentListName(){
        return transform.parent.name;
    }

    public void UpdateDisplay(Sprite ico){
        if (ico != null){
            icon.sprite = ico;
        }else{
            icon.sprite = default_icon;
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        if (interactable){
            if (selected_obj == null){
                if (icon.sprite != default_icon){
                    if (canvas == null){
                        canvas = FindObjectOfType<Canvas>();
                    }
                    selected_obj = Instantiate(dragUI_prefab, canvas.transform);
                    selected_obj.GetComponent<RectTransform>().position = rectTransform.position;
                    selected_obj.GetComponent<Image>().sprite = icon.sprite;
                    selected_slot = this;
                    manager.DisplaySlotInfo(this);
                }
            }else{
                TryCommitToBackend();
                selected_slot = null;
                Destroy(selected_obj);
                selected_obj = null;
                // Refresh the display
                manager.DisplaySlotInfo(null);
                manager.DisplayUpdate();  
            }
        }
    }

    //assuming selected stuff isn't null
    public void TryCommitToBackend(){
        if (selected_slot.GetParentListName() == manager.GetBoardName()){
            if (GetParentListName() == manager.GetBoardName()){
                Debug.Log(selected_slot.index);
                Debug.Log(index);
                Inventory.instance.board.Swap(selected_slot.index, index); // Swap theoretically works even if one object is null
            }
            if (GetParentListName() == manager.GetBenchName()){
                Inventory.instance.BoardToBench(selected_slot.index); 
            }
            if (GetParentListName() == manager.GetUnitsName()){
                Inventory.instance.BoardToInventory(selected_slot.index); 
            }
        }

        if (selected_slot.GetParentListName() == manager.GetBenchName()){
            if (GetParentListName() == manager.GetBoardName()){
                Inventory.instance.BenchToBoard(selected_slot.index); 
            }
            if (GetParentListName() == manager.GetBenchName()){
                Inventory.instance.bench.Swap(selected_slot.index, index); 
            }           
            if (GetParentListName() == manager.GetUnitsName()){
                Inventory.instance.BenchToInventory(selected_slot.index); 
            }                     
        }

        if (selected_slot.GetParentListName() == manager.GetUnitsName()){
            if (GetParentListName() == manager.GetBenchName()){
                Inventory.instance.InventoryToBench(selected_slot.index); 
            }           
            if (GetParentListName() == manager.GetUnitsName()){
                Inventory.instance.units.Swap(selected_slot.index, index); 
            }
            if (GetParentListName() == manager.GetBoardName()){
                Inventory.instance.InventoryToBoard(selected_slot.index); 
            } 
        }

        if (selected_slot.GetParentListName() == manager.GetItemName()){
            if (GetParentListName() == manager.GetItemName()){
                Inventory.instance.items.Swap(selected_slot.index, index); 
            }  
        }
    }
}
