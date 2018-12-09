using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    public delegate void TickHandler(float timeOfDay);
    public event TickHandler Tick;

    [System.Serializable]
	public class Settings {
        public float tickTime = 5f;
        public float hoursPerSecond = 0.1f;
    }
    public Settings settings;

    private float _tickTimer;
    private float _timeOfDay;
    public float TimeOfDay {
        get { return _timeOfDay; }
        set { _timeOfDay = value; }
    }

    private void Start() {
        _tickTimer = Time.time;
        _timeOfDay = 0f;
    }

    private void Update() {
        if ((Time.time - _tickTimer) >= settings.tickTime) {
            Debug.Log("Ticking!");
            // Update the time of day
            _timeOfDay += settings.tickTime * settings.hoursPerSecond;
            // Reset to zero if we go over 24.
            if (_timeOfDay >= 24f) {
                _timeOfDay = _timeOfDay - 24f;
            }
            // Call the tick event if anybody's listening.
            if (Tick != null) {
                Tick(_timeOfDay);  
            }
            // Reset the tick timer
            _tickTimer = Time.time;
        }
    }
}
