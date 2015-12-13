using UnityEngine;
using System.Collections;

public static class GameManager {

	public static int currentScene=0;
	public static string[] tips = {"努力加载中","开始游戏吧","你找到一个锁着的箱子","再问问宫森葵，她知道的更多","快去找宫森葵，你找到资料了","恭喜你，游戏已经通关","请把手机放到Cardboard里，请小心周围情况。"};
	public static string[] scene = {"Open","SceneOne","SceneTow","SceneSAO","SceneFour","SceneEnd","BoxAR"};
	private static string[] rnd = {"SceneAB","SceneSAO","SceneKoto","SceneSuzumiya"};

	public static void TrackLost(string information){
		switch (currentScene) {
		case 0:
			break;
		case 1:
			if(information=="Box"){
				currentScene=2;
				Application.LoadLevel("LoadScene");
			}
			break;
		case 2:
			if(information=="Miyamori"){
				currentScene=3;
				scene[3]=RandomScene();
				Application.LoadLevel("LoadScene");
			}
			break;
		case 3:
			if(information=="SAO"||information=="AB"||information=="Koto"||information=="Suzumiya"){
				currentScene=4;
				Application.LoadLevel("LoadScene");
			}
			break;
		case 4:
			if(information=="Box"){
				currentScene=5;
				Application.LoadLevel("LoadScene");
			}
			break;
		default:
			break;
		}
	}

	private static string RandomScene(){
		string strReturn = PlayerPrefs.GetString("scene","SceneAB");
		int temp = Random.Range (0, 4);
		if(strReturn==rnd[temp]){
			temp=temp+1;
		}
		if(temp>=rnd.Length){
			temp=0;
		}
		strReturn = rnd [temp];
		PlayerPrefs.SetString ("scene",strReturn);
		return strReturn;
	}
}
