using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DragAndDrop))]
public class WaterPlant : MonoBehaviour {

    private DragAndDrop _dragAndDrop;

    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.position;
    }

    private void OnEnable() {
        _dragAndDrop = GetComponent<DragAndDrop>();
        _dragAndDrop.OnDropped += WaterAt;
    }

    private void WaterAt(Vector3 position) {

    }
}
