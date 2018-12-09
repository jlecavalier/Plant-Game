using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockFaceChange : MonoBehaviour {
	private Clock _clock;

	public Sprite day;
	public Sprite night;

	private void OnEnable() {

		Clock _clock = Utils.GetClock();
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
		gameObject.GetComponent<SpriteRenderer>().sprite = day;
			

	}
	public void ClockTick(float timeOfDay) {

		Debug.Log ("Move Hand");		
		Debug.Log (_clock);

		ChangeFace (timeOfDay);
	}	

	void ChangeFace(float timeOfDay)
	{
		if (timeOfDay == 12) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = night;
		} else if (timeOfDay == 0) {
			gameObject.GetComponent<SpriteRenderer>().sprite = day;

		}
	}

}
