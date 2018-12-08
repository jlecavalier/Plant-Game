using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    public static HomeGrid GetHomeGrid() {
        GameObject go = GameObject.FindGameObjectWithTag("Grid");
        if (go) {
            if (go.GetComponent<HomeGrid>()) {
                return go.GetComponent<HomeGrid>();
            }
        }
        return null;
    }
}
