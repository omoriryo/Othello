using System.Collections;
using UnityEngine;

public class Title : MonoBehaviour
{
	public static bool is1p;
	public AudioClip SelectSound;

	public static bool GetIs1pGame() {
		return is1p;
	}

	public void SetIs1pGame(){
		is1p = true;
		SoundManager.instance.RandomizeSfx(SelectSound);
		Application.LoadLevel("Battle");
	}

	public void SetIs2pGame(){
		is1p = false;
		SoundManager.instance.RandomizeSfx(SelectSound);
		Application.LoadLevel("Battle");
	}

	void Start ()
	{
	}

	void Update ()
	{
//		//マウスの左ボタンをクリック
//		if(Input.GetMouseButtonUp(0)){
//			Application.LoadLevel("Battle");
//		}
	}
}