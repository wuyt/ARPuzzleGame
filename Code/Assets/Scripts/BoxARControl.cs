using UnityEngine;
using System.Collections;

public class BoxARControl : MonoBehaviour
{

	public GameObject box;
	public GameObject text_end;
	public GameObject egg;
	public GameObject ab;
	public GameObject koto;
	public GameObject sao;
	public GameObject suzumiya;
	public GameObject[] yasuhara;
	public GameObject[] todo;
	public GameObject[] sakaki;
	public GameObject[] imai;
	public GameObject[] miyamori;
	public GameObject[] losted;
	private int currentScene = 1;
	private static string[] rnd = {
		"SceneAB",
		"SceneSAO",
		"SceneKoto",
		"SceneSuzumiya"
	};



	void Start(){
		egg.SetActive (false);
		ab.SetActive (false);
		koto.SetActive (false);
		sao.SetActive (false);
		suzumiya.SetActive (false);

		AllFalse ();

		yasuhara [0].SetActive (true);
		todo [0].SetActive (true);
		sakaki [0].SetActive (true);
		imai [0].SetActive (true);
		miyamori [0].SetActive (true);

		AutoLost ();
	}

	void AllFalse(){
		for (int i=0; i<yasuhara.Length; i++) {
			yasuhara[i].SetActive (false);
		}
		for (int i=0; i<todo.Length; i++) {
			todo[i].SetActive (false);
		}
		for (int i=0; i<sakaki.Length; i++) {
			sakaki[i].SetActive (false);
		}
		for (int i=0; i<imai.Length; i++) {
			imai[i].SetActive (false);
		}
		for (int i=0; i<miyamori.Length; i++) {
			miyamori[i].SetActive (false);
		}

		System.GC.Collect(); 
	}

	public void TrackLost (string information)
	{
		switch (currentScene) {
		case 0:
			break;
		case 1:
			if (information == "Box") {
				currentScene = 2;
				AllFalse();

				yasuhara [1].SetActive (true);
				todo [1].SetActive (true);
				sakaki [1].SetActive (true);
				imai [1].SetActive (true);
				miyamori [1].SetActive (true);
			}
			break;
		case 2:
			if (information == "Miyamori") {
				currentScene = 3;
				egg.SetActive (true);
				Lost(egg);
				AllFalse();

				RandomScene ();
			}
			break;
		case 3:
			if (information == "SAO" || information == "AB" || information == "Koto" || information == "Suzumiya") {
				currentScene = 4;
				box.SetActive(false);
				text_end.SetActive(true);

				ab.SetActive (true);
				sao.SetActive (true);
				suzumiya.SetActive (true);
				koto.SetActive (true);
				AllFalse();
				miyamori[2].SetActive (true);
			}
			break;
		case 4:
			if (information == "Box") {
				currentScene = 5;
				Application.LoadLevel ("SceneEnd");
			}
			break;
		default:
			break;
		}

		AutoLost ();
	}

	void AutoLost(){
		for (int i=0; i<losted.Length; i++) {
			Lost (losted[i]);
		}
		System.GC.Collect(); 
	}

	void Lost(GameObject gm){
		Renderer[] rendererComponents = gm.GetComponentsInChildren<Renderer>();
		Collider[] colliderComponents = gm.GetComponentsInChildren<Collider>();
		
		// Disable rendering:
		foreach (Renderer component in rendererComponents)
		{
			component.enabled = false;
		}
		
		// Disable colliders:
		foreach (Collider component in colliderComponents)
		{
			component.enabled = false;
		}
	}

	private void RandomScene ()
	{
		string strReturn = PlayerPrefs.GetString ("scene", "SceneAB");
		int temp = Random.Range (0, 4);
		if (strReturn == rnd [temp]) {
			temp = temp + 1;
		}
		if (temp >= rnd.Length) {
			temp = 0;
		}
		strReturn = rnd [temp];
		PlayerPrefs.SetString ("scene", strReturn);
		//return strReturn;

		switch (strReturn) {
		case "SceneAB":
			ab.SetActive (true);
			Lost(ab);
			yasuhara [2].SetActive (true);
			todo [2].SetActive (true);
			sakaki [2].SetActive (true);
			imai [3].SetActive (true);
			miyamori [3].SetActive (true);
			break;
		case "SceneSAO":
			sao.SetActive (true);
			Lost(sao);
			yasuhara [2].SetActive (true);
			todo [2].SetActive (true);
			sakaki [3].SetActive (true);
			imai [2].SetActive (true);
			miyamori [5].SetActive (true);
			break;
		case "SceneKoto":
			koto.SetActive (true);
			Lost(koto);
			yasuhara [3].SetActive (true);
			todo [2].SetActive (true);
			sakaki [2].SetActive (true);
			imai [2].SetActive (true);
			miyamori [4].SetActive (true);
			break;
		case "SceneSuzumiya":
			suzumiya.SetActive (true);
			Lost(suzumiya);
			yasuhara [2].SetActive (true);
			todo [3].SetActive (true);
			sakaki [2].SetActive (true);
			imai [2].SetActive (true);
			miyamori [6].SetActive (true);
			break;
		default:
			break;
		}
	}
}
