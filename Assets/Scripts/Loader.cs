using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	//GameManagerのプレファブを指定
	public GameObject gameManager;
	public GameObject soundManager;

	void Awake ()
	{
		//GameManagerが存在しない時、GameManagerを作成する
		if (GameManager.instance == null) {
			Instantiate (gameManager);
		}
		if (SoundManager.instance == null) {
			Instantiate (soundManager);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
