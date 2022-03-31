using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public abstract class InvWindow<T> : MonoBehaviour where T: Data{

    private CanvasGroup canvasGroup;
    public InvList<T> list_data{get; private set;}

    public GameObject slot_prefab;

    public void Awake(){
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetData(InvList<T> list){
        list_data = list;
    }

    public void SetData(T[] list){
        list_data = InvList<T>.LoadFrom(list, list.Length);
    }

    public void GenerateSlots(){
        for (int i = 0; i < list_data.length; ++i){
            GameObject new_slot = Instantiate(slot_prefab, transform);
        }
        DisplayUpdate();
    }

    public void SetInteractable(bool state){
        foreach (Slot slot in GetComponentsInChildren<Slot>()){
            slot.interactable = state;
        }
    }
    

    public void DisplayUpdate(){
        int idx = 0;
        foreach (Slot slot in GetComponentsInChildren<Slot>()){
            T entry = list_data.At(idx);
            if (entry != null){
                slot.UpdateDisplay(entry.icon);
            }else{
                slot.UpdateDisplay(null);
            }
            ++idx;
        }
    }

    public void Show(){
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void Hide(){
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}