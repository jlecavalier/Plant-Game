using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsButtonScript : MonoBehaviour {


	public GameObject creditsText;
    public GameObject titleText;

	// Use this for initialization
	void Start () {
		creditsText.SetActive (false);
	}
	
	public void ActivateText()
	{
		creditsText.SetActive (true);
        titleText.SetActive(false);
	}

	public void DeactivateText()
	{
		creditsText.SetActive (false);
        titleText.SetActive(true);
	}
}
