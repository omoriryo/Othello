using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {
	public AudioClip ResultSound;
	// Use this for initialization
	void Start ()
	{
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
