using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler {
    public Canvas canvas;
    [HideInInspector] public Item item;
    [HideInInspector] public ItemSlot slot;
    [HideInInspector] public RectTransform rectTransform;
    [HideInInspector] public CanvasGroup canvasGroup;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Beginning Drag");
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("Ending Drag");
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

    public void FixPosition() {
        if (slot != null) {
            rectTransform.position = slot.rectTransform.position;
        }
    }

    // Pass event to slot
    public void OnDrop(PointerEventData eventData) {
        if (slot != null) {
            slot.OnDrop(eventData);
        }
    }
}