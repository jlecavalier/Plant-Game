using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    public PlantData data;

    private int _waterLevel;

    public delegate void OnPlantWasWateredHandler(Plant plant);
    public event OnPlantWasWateredHandler OnPlantWasWatered;

    private void Start() {
        _waterLevel = data.startingValues.startingWater;
    }

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

    public void WaterPlant() {
        if (OnPlantWasWatered != null) {
            OnPlantWasWatered(this);
        }
        _waterLevel += 5;
    }

    public void ConsumeWater() {
        _waterLevel -= 5;
        Debug.Log(_waterLevel);
    }
}
