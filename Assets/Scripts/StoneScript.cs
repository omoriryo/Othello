using UnityEngine;
using System.Collections;

public class StoneScript : MonoBehaviour {

	bool isBlack_;

	public bool IsBlack()
	{
		return isBlack_;
	}

	public void IsBlack(bool isBlack)
	{
		isBlack_ = isBlack;
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
}