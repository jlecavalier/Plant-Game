using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DragAndDrop))]
[RequireComponent(typeof(Plant))]
public class PlantSnapToGrid : MonoBehaviour {

    private DragAndDrop _dragAndDrop;
    private Plant _plant;

    private void OnEnable() {
        _dragAndDrop = GetComponent<DragAndDrop>();
        _dragAndDrop.OnDropped += Snap;
    }

    private void Start() {
        _plant = GetComponent<Plant>();
    }

    private void OnDisable() {
        _dragAndDrop.OnDropped -= Snap;
    }

    public void Snap(Vector3 desiredPosition) {
        Vector3 targetPosition = new Vector3(Mathf.Round(desiredPosition.x), Mathf.Round(desiredPosition.y), transform.position.z);
        if (targetPosition.x < 0 || targetPosition.x > 9 || targetPosition.y < 0 || targetPosition.y > 9) {
            if (_plant.HasBeenPlaced) {
                transform.position = _plant.LastGridPosition;
            } else {
                // TODO: Return plant to object pool. Increase number of available plants in UI, maybe?
                Destroy(gameObject);
            }
        } else {
            transform.position = targetPosition;
            _plant.HasBeenPlaced = true;
            _plant.LastGridPosition = targetPosition;
        }
    }
}
