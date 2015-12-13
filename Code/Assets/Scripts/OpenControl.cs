using UnityEngine;
using System.Collections;

public class OpenControl : MonoBehaviour {

	public void StartGame(){
		GameManager.currentScene = GameManager.currentScene + 1;
		Application.LoadLevel ("LoadScene");
	}

	public void BoxAR(){
		GameManager.currentScene = 6;
		Application.LoadLevel ("LoadScene");
	}
}
