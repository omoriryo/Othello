using UnityEngine;
using System.Collections;

public class HumanPlayer : PlayerBase {

	// タップを検知し、GridScriptを取得する
	bool DetectTap(out GridScript gridScript)
	{
		gridScript = null;
		if (Input.GetMouseButtonDown(0)) // タップ検知
		{

			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = new RaycastHit2D();
			if (hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100))
			{
				GameObject obj = hit.collider.gameObject; // タップしたGameObjectを取得
				gridScript = obj.GetComponent<GridScript>(); // GridScriptを取得
				return true;
			}
		}
		return false;
	}

	public override bool Play()
	{
		// タップ検知
		GameObject grid = GameManager.instance.boardManager.GetGrid(0,0);
		if (grid.GetComponent<GridScript> ().ShowTarget (isBlack_)) {
			GridScript gridScript;
			if (DetectTap (out gridScript)) {
				// グリッドをタップしており、かつ、そこに石が無いなら
				if (gridScript && !gridScript.GetStone ()) {
					// そこに石を置けるなら
					if (gridScript.JudgeStonePutable (isBlack_)) {
						// 石を置く
						gridScript.PutStone (isBlack_);
						SoundManager.instance.RandomizeSfx(PutSound);

						// ひっくり返す
						gridScript.TurnStone (isBlack_);

						GameManager.AddTrun ();
						return true;
					}
				}
			}
			return false;
		} else {
			return true;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}