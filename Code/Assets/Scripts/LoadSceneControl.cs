using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSceneControl : MonoBehaviour
{

	public Text information;
	public Slider loading;
	private AsyncOperation async;
	private bool load = false;

	// Use this for initialization
	void Start ()
	{
		information.text = GameManager.tips [GameManager.currentScene];
		//Debug.Log (GameManager.currentScene+"------"+GameManager.scene[GameManager.currentScene]);
		//Invoke(
		Invoke ("StartLoading", 2f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (load) {
			loading.value = async.progress;
		}
	}

	void StartLoading ()
	{
		load = true;
		StartCoroutine (LoadScene ());
	}

	IEnumerator LoadScene ()
	{
		async = Application.LoadLevelAsync (GameManager.scene[GameManager.currentScene]);
		yield return async;
	}
}
