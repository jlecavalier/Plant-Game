using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeGrid : MonoBehaviour {

	public GameObject space;
	public List<GameObject> grid;
	public int n; //grid size
 
	// Use this for initialization
	void Start () {
		n = 3;
		grid = new List<GameObject> ();

		CreateGrid();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckGrid ();
	}

	void CreateGrid()
	{
		//place grids down;

		for (int i = 0; i < (n*n); i++) 
		{
			GameObject plant_space;
			plant_space = Instantiate (space);
			grid.Add(plant_space);
		}

	}

	void PlaceInGrid()
	{
		
	}

	void CheckGrid()
	{
		int tally = 0;
		for (int i = 0; i < grid.Count; i++) 
		{
			if (grid[i].GetComponent<PlantSpace>().getIsOccupied()) 
			{
				tally++;
			}
		}

		Debug.Log (tally);
	}
}
