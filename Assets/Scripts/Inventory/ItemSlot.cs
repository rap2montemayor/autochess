using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {
    ItemUI item;
    [HideInInspector] public RectTransform rectTransform;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Received Drop");
        if (eventData.pointerDrag.TryGetComponent(out ItemUI newItem)) {
            if (newItem.slot != null) {
                (item, newItem.slot.item) = (newItem.slot.item, item);
                if (item.slot.item != null) {
                    item.slot.item.slot = item.slot;
                    item.slot.item.FixPosition();
                }
            }
            newItem.slot = this;
        }
    }
}