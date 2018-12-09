using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpace : MonoBehaviour {

    [System.Serializable]
    public class Settings {
        public bool receivesLight;
        public float minLightTime;
        public float maxLightTime;
    }
    public Settings settings;

    private bool _isLit;
    
	public static Vector2 spacePosition;
    private Plant _plant;
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
        _isLit = Light(settings.receivesLight && settings.minLightTime <= 0 && settings.maxLightTime >= 0);
	}

	public bool getIsOccupied()
	{
		return _plant != null;
	}

    public void ClockTick(float timeOfDay) {
        if (_plant != null) {
            _plant.ClockTick();
            _plant.ConsumeWater();
            _plant.UpdateLight(_isLit);
            _plant.CheckHealth();
            _plant.UpdateStageOfLife();
        }
        _isLit = Light(settings.receivesLight && settings.minLightTime <= timeOfDay && settings.maxLightTime >= timeOfDay);
    }

    private bool Light(bool turnOn) {
        foreach (Transform t in transform) {
            t.gameObject.SetActive(turnOn);
        }
        return turnOn;
    }
}
