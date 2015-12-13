using UnityEngine;
using System.Collections;

public class EndControl : MonoBehaviour {

	public void Again(){
		GameManager.currentScene = 0;
		Application.LoadLevel ("LoadScene");
	}

	public void Quit(){
		Application.Quit ();
	}

	public void Blog(){
		Application.OpenURL ("http://blog.csdn.net/wuyt2008/article/details/49966839");
	}
}
