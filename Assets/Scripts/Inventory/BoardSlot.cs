using UnityEngine;
using UnityEngine.EventSystems;

public class BoardSlot : MonoBehaviour, IDropHandler {
    public UnitUI unit;
    [HideInInspector] public RectTransform rectTransform;

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Received Drop");
        if (eventData.pointerDrag.TryGetComponent(out UnitUI newUnit)) {
            // Least buggy and confusing code
            if (newUnit.slot != null) {
                (unit, newUnit.slot.unit) = (newUnit.slot.unit, unit);
                if (unit.slot.unit != null) {
                    unit.slot.unit.slot = unit.slot;
                    unit.slot.unit.FixPosition();
                }
            } else if (newUnit.benchSlot != null) {
                (unit, newUnit.benchSlot.unit) = (newUnit.benchSlot.unit, unit);
                if (unit.benchSlot.unit != null) {
                    unit.benchSlot.unit.benchSlot = unit.benchSlot;
                    unit.benchSlot.unit.FixPosition();
                }
            } else if (newUnit.boardSlot != null) {
                (unit, newUnit.boardSlot.unit) = (newUnit.boardSlot.unit, unit);
                if (unit.boardSlot.unit != null) {
                    unit.boardSlot.unit.boardSlot = unit.boardSlot;
                    unit.boardSlot.unit.FixPosition();
                }
            }

            unit.slot = null;
            unit.benchSlot = null;
            unit.boardSlot = this;
        }
    }
}