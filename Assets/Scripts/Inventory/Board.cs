using System.Collections.Generic;
using UnityEngine;

public class Board {
    Dictionary<Unit, Vector2Int> unitPositions;
    Unit[,] board;

    public Board() {
        board = new Unit[8, 8];
    }

    void MoveUnit(Vector2Int from, Vector2Int to) {
        (board[from.x, from.y], board[to.x, to.y])
            = (board[to.x, to.y], board[from.x, from.y]);
        unitPositions[board[from.x, from.y]] = from;
        unitPositions[board[to.x, to.y]] = to;
    }

    void AddUnit(Unit unit, Vector2Int pos) {
        unitPositions[unit] = pos;
        board[pos.x, pos.y] = unit;
    }

    void RemoveUnitAt(Vector2Int pos) {
        unitPositions.Remove(board[pos.x, pos.y]);
        board[pos.x, pos.y] = null;
    }

    Vector2Int GetPosition(Unit unit) {
        if (unitPositions.ContainsKey(unit)) {
            return unitPositions[unit];
        } else {
            return new Vector2Int(-1, -1);
        }
    }
}