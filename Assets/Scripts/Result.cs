using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {
	public AudioClip ResultSound;
	public Sprite BlackWinSprite;
	public Sprite WhiteWinSprite;
	public Sprite DrawSprite;
	SpriteRenderer ResultSprite;
	// Use this for initialization
	void Start ()
	{
		ResultSprite = gameObject.GetComponent<SpriteRenderer> ();
		if (GameManager.Result == 0) {
			ResultSprite.sprite = BlackWinSprite;
		} else if(GameManager.Result == 1){
			ResultSprite.sprite = WhiteWinSprite;
		}else {
			ResultSprite.sprite = DrawSprite;
		}
		StartCoroutine (ChangeScene ());
	}

	IEnumerator ChangeScene()
	{
		SoundManager.instance.BgmStop ();
		SoundManager.instance.PlaySingle (ResultSound);
		yield return new WaitForSeconds(8);
		SoundManager.instance.BgmStart ();
		Application.LoadLevel("Title");
	}
}
