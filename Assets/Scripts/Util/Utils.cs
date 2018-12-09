using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    public static HomeGrid GetHomeGrid() {
        GameObject go = GameObject.FindGameObjectWithTag("Grid");
        if (go && go.activeInHierarchy) {
            if (go.GetComponent<HomeGrid>()) {
                return go.GetComponent<HomeGrid>();
            }
        }
        return null;
    }

    public static Clock GetClock() {
        GameObject go = GameObject.FindGameObjectWithTag("Clock");
        if (go && go.activeInHierarchy) {
            if (go.GetComponent<Clock>()) {
                return go.GetComponent<Clock>();
            }
        }
        return null;
    }
}
