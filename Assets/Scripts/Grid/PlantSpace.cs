using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpace : MonoBehaviour {

	public bool isOccupied;
	public static Vector2 spacePosition;
    public Plant _plant;
    public Plant CurrentPlant {
        get { return _plant; }
        set { _plant = value; }
    }

	// Use this for initialization
	void Start () {
		spacePosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool getIsOccupied()
	{
		return _plant != null;
	}
}
