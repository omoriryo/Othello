using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour {
	public Sprite BlackTrunSprite;
	public Sprite WhiteTrunSprite;
	Image TrunSprite;
	// Use this for initialization
	void Start () {
		GameManager.instance.InitGame ();
	}
	
	// Update is called once per frame
	void Update () {
		TrunSprite = gameObject.GetComponent<Image> ();
		if (GameManager.isBlackTurn) {
			TrunSprite.sprite = BlackTrunSprite;
		} else {
			TrunSprite.sprite = WhiteTrunSprite;
		}
	}
}
