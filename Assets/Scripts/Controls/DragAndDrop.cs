using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    private Vector3 _offset;
    private Vector3 _screenPoint;

    public delegate void OnDroppedHandler(Vector3 dropPosition);
    public event OnDroppedHandler OnDropped;

    private void OnMouseDown() {
        Vector3 mp = Input.mousePosition;
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        _offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(mp.x, mp.y, _screenPoint.z));
    }

    private void OnMouseDrag() {
        Vector3 mp = Input.mousePosition;
        Vector3 currentScreenPoint = new Vector3(mp.x, mp.y, _screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + _offset;
        transform.position = currentPosition;
    }

    private void OnMouseUp() {
        // Snap the object to the grid position nearest to the object
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        OnDropped(mp);
    }
}
