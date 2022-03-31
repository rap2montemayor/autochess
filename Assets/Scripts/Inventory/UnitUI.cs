using UnityEngine;
using UnityEngine.EventSystems;

// For now, units can only be in ONE of the ff:
// - unit inventory (slot)
// - bench (benchSlot)
// - board (boardSlot)
public class UnitUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler {
    public Canvas canvas;
    [HideInInspector] public Unit unit;
    public UnitSlot slot;
    public BenchSlot benchSlot;
    public BoardSlot boardSlot;
    [HideInInspector] public RectTransform rectTransform;
    [HideInInspector] public CanvasGroup canvasGroup;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
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
        FixPosition();
    }

    public void FixPosition() {
        if (slot != null) {
            transform.position = slot.transform.position;
        } else if (benchSlot != null) {
            transform.position = benchSlot.transform.position;
        } else if (boardSlot != null) {
            transform.position = boardSlot.transform.position;
        }
    }

    // Pass event to slot
    public void OnDrop(PointerEventData eventData) {
        if (slot != null) {
            slot.OnDrop(eventData);
        } else if (benchSlot != null) {
            benchSlot.OnDrop(eventData);
        } else if (boardSlot != null) {
            boardSlot.OnDrop(eventData);
        }
    }
}