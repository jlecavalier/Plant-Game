using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    public PlantData data;

    private Vector3 _lastGridPosition;
    public Vector3 LastGridPosition {
        get { return _lastGridPosition; }
        set { _lastGridPosition = value; }
    }

    private bool _hasBeenPlaced;
    public bool HasBeenPlaced {
        get { return _hasBeenPlaced; }
        set { _hasBeenPlaced = value; }
    }
}
