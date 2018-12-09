using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandMovement : MonoBehaviour {

	private Clock _clock;
	float ratio;

	private void OnEnable() {
		
		Clock _clock = Utils.GetClock();
		ratio = _clock.settings.hoursPerSecond * _clock.settings.tickTime;
		Debug.Log (_clock);
		if (_clock) {
			_clock.Tick += ClockTick;
		}
	}

	private void OnDisable() {
		if (_clock) {
			_clock.Tick -= ClockTick;
		}
	}

	void Start()
	{
		Clock _clock = Utils.GetClock ();

	}
	public void ClockTick(float timeOfDay) {

		Debug.Log ("Move Hand");		
		Debug.Log (_clock);
	
		MoveHand (timeOfDay);
	}	

	void MoveHand(float timeOfDay)
	{
		Vector3 z_axis = new Vector3(0,0,1); //z-axis

		float r = 3 / ratio; 
		float angle = 90 / r;

		gameObject.transform.RotateAround (gameObject.transform.position, z_axis, -angle);
	}


}
