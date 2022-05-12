using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attach this to board
public class SetupBattle : MonoBehaviour {
    [SerializeField]
    private GameObject AllyUnitPrefab;

    // I can probably use the fancy grid thing but idk how.
    private Vector2 upperLeft;
    private Vector2 spriteSize;

    void Start() {
        upperLeft = new Vector2(0f, 0f);
        spriteSize = new Vector2(36.25f, 36.25f);

        // get inventory and board
        Inventory inventory = Inventory.instance;
        InvList<Unit> board = inventory.board;
        for (int i = 0; i < board.length; ++i) {
            if (board.At(i) == null) continue;

            // get position on the board
            Vector2Int boardPos = inventory.IndexToBoardCoord(i);
            Debug.Log(boardPos);
            // get position on screen
            Vector2 pos = new Vector2(
                upperLeft.x + (boardPos.x * spriteSize.x),
                upperLeft.y - ((boardPos.y+4) * spriteSize.y)
            );

            // instantiate, set sprite
            GameObject obj = Instantiate(AllyUnitPrefab, transform);
            obj.GetComponent<Image>().sprite = board.At(i).icon;
            obj.GetComponent<AllyUnitBehavior>().unitDataReference = board.At(i);

            obj.transform.localPosition = pos;
        }
    }
}
