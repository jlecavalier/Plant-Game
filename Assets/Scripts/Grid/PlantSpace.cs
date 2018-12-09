using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpace : MonoBehaviour {
    
	public static Vector2 spacePosition;
    public Plant _plant;
    public Plant CurrentPlant {
        get { return _plant; }
        set { _plant = value; }
    }

    private Clock _clock;

    private void OnEnable() {
        Clock _clock = Utils.GetClock();
        if (_clock) {
            _clock.Tick += ClockTick;
        }
    }

    private void OnDisable() {
        if (_clock) {
            _clock.Tick -= ClockTick;
        }
    }

    // Use this for initialization
    void Start () {
		spacePosition = gameObject.transform.position;
	}

	public bool getIsOccupied()
	{
		return _plant != null;
	}

    public void ClockTick(float timeOfDay) {
        if (_plant != null) {
            _plant.ConsumeWater();
        }
    }
}
