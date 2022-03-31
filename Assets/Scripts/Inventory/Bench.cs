using UnityEngine;
using System.Collections.Generic;

public class Bench {
    public const int MAX_BENCH_UNITS = 8;

    List<Unit> units;

    public Bench() {
        units = new List<Unit>();
        for (int i = 0; i < MAX_BENCH_UNITS; ++i) {
            units.Add(null);
        }
    }

    // works the same as inventory but with less slots
    public void BenchUnit(Unit unit) {
        int idx = units.IndexOf(null);
        if (idx != -1) {
            units[idx] = unit;
            Debug.Log(string.Format("Unit benched at {0}", idx));
        } else {
            Debug.Log("Unable to bemch unit");
        }
    }

    public void RemoveUnit(Unit unit) {
        int idx = units.IndexOf(unit);
        if (idx != -1) {
            units[idx] = null;
            Debug.Log(string.Format("Unit removed at {0} from bench", idx));
        } else {
            Debug.Log("Unable to remove Unit from bench");
        }
    }
    
    public bool IsBenched(Unit unit) {
        return units.Contains(unit);
    }
}