using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeGrid : MonoBehaviour {

	public List<PlantSpace> grid;
	public int n; //grid size
 
	// Use this for initialization
	void Start () {
		grid = new List<PlantSpace> ();
        PopulateGrid();
	}

    private void PopulateGrid() {
        foreach (Transform t in transform) {
            if (t.GetComponent<PlantSpace>()) {
                grid.Add(t.GetComponent<PlantSpace>());
            }
        }
    }
    
    public PlantSpace GetPlantSpaceAtPosition(Vector3 position) {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        if (x >= 0 && x <= 9 && y >= 0 && y <= 9) {
            return grid[x + (y * 10)];
        }
        return null;
    }

	void CheckGrid() {
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
