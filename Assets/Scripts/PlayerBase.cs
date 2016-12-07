using UnityEngine;
using System.Collections;

abstract public class PlayerBase : MonoBehaviour
{
	protected bool isBlack_;
	public AudioClip PutSound;

	public void IsBlack(bool isBlack)
	{
		isBlack_ = isBlack;
	}

	public void SetSound(AudioClip clip){
		PutSound = clip;
	}

	public abstract bool Play();

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}
}