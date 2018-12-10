using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour {

	public void LoadGame()
	{
		SceneManager.LoadScene (1);	
	}
}
