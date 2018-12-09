using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToExit : MonoBehaviour {

    private void OnMouseUp() {
        Application.Quit();
    }
}
