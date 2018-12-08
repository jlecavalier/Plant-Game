using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpace : MonoBehaviour {

	public bool isOccupied;
	public static Vector2 spacePosition;

	// Use this for initialization
	void Start () {
		spacePosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool getIsOccupied()
	{
		return isOccupied;
	}

	public void setIsOccupied(bool b)
	{
		isOccupied = b;
	}
}
