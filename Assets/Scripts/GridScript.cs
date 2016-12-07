using UnityEngine;
using System.Collections;

public class GridScript : MonoBehaviour {

	GameObject stone_;	
	GameObject target_;
	GameObject blackStonePrefab_, whiteStonePrefab_;
	GameObject blackTargetPrefab_, whiteTargetPrefab_;
	int colNo_, rowNo_;
	int colNum_ = 8;
	int rowNum_ = 8;
	int dirNum_ = 8; // 8方向
	int[] dirCol_ = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 }; // 各方向の移動量
	int[] dirRow_ = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 }; // 各方向の移動量

	public void SetColNo(int colNo)
	{
		colNo_ = colNo;
	}

	public void SetRowNo(int rowNo)
	{
		rowNo_ = rowNo;
	}

	public void SetBlackStonePrefab(GameObject blackStonePrefab)
	{
		blackStonePrefab_ = blackStonePrefab;
	}

	public void SetWhiteStonePrefab(GameObject whiteStonePrefab)
	{
		whiteStonePrefab_ = whiteStonePrefab;
	}

	public void SetBlackTargetPrefab(GameObject blackTargetPrefab)
	{
		blackTargetPrefab_ = blackTargetPrefab;
	}

	public void SetWhiteTargetPrefab(GameObject whiteTargetPrefab)
	{
		whiteTargetPrefab_ = whiteTargetPrefab;
	}

	public GameObject GetStone()
	{
		return stone_;
	}

	public GameObject GetTarget()
	{
		return target_;
	}

	bool SearchSameColorStone(bool isBlackTurn, int dirNum, int colNo, int rowNo)
	{
		if (colNo >= 0 && colNo < GameManager.instance.boardManager.GetColNum() && rowNo >= 0 && rowNo < GameManager.instance.boardManager.GetRowNum())
		{
			GameObject grid = GameManager.instance.boardManager.GetGrid(colNo, rowNo);
			GameObject stone = grid.GetComponent<GridScript>().GetStone();
			if (stone) {
				if (stone.GetComponent<StoneScript> ().IsBlack () == isBlackTurn) {
					return true;
				} else {
					return SearchSameColorStone (isBlackTurn, dirNum, colNo + dirCol_ [dirNum], rowNo + dirRow_ [dirNum]);
				}
			} else {
				return false;
			}
		}
		return false;
	}

	bool JudgeStonePutableDir(bool isBlackTurn, int dir)
	{
		// 1個目が自分と異なる色か確認
		int colNo = colNo_ + dirCol_[dir];
		int rowNo = rowNo_ + dirRow_[dir];
		if (colNo >= 0 && colNo < GameManager.instance.boardManager.GetColNum() && rowNo >= 0 && rowNo < GameManager.instance.boardManager.GetRowNum())
		{
			GameObject grid = GameManager.instance.boardManager.GetGrid(colNo, rowNo);
			GameObject stone = grid.GetComponent<GridScript>().GetStone();
			if (stone && stone.GetComponent<StoneScript>().IsBlack() != isBlackTurn)
			{
				// 自分と同じ色の石を探索していく
				return SearchSameColorStone(isBlackTurn, dir, colNo + dirCol_[dir], rowNo + dirRow_[dir]);
			}

		}
		return false;
	}

	public bool JudgeStonePutable(bool isBlackTurn)
	{
		for (int d = 0; d < dirNum_; ++d)
		{
			if (JudgeStonePutableDir(isBlackTurn, d))
			{
				return true;
			}
		}
		return false;
	}

	void TurnStoneDir(bool isBlackTurn, int dir, int colNo, int rowNo)
	{
		if (colNo >= 0 && colNo < GameManager.instance.boardManager.GetColNum() && rowNo >= 0 && rowNo < GameManager.instance.boardManager.GetRowNum())
		{
			GameObject grid = GameManager.instance.boardManager.GetGrid(colNo, rowNo);
			GameObject stone = grid.GetComponent<GridScript>().GetStone();
			if (stone && stone.GetComponent<StoneScript>().IsBlack() != isBlackTurn)
			{
				Destroy(stone);
				grid.GetComponent<GridScript>().PutStone(isBlackTurn);
				TurnStoneDir(isBlackTurn, dir, colNo + dirCol_[dir], rowNo + dirRow_[dir]);
			}
			else
			{
				return;
			}
		}
	}


	public void TurnStone(bool isBlackTurn)
	{
		for (int d = 0; d < dirNum_; ++d)
		{
			if (JudgeStonePutableDir(isBlackTurn, d))
			{
				TurnStoneDir(isBlackTurn, d, colNo_ + dirCol_[d], rowNo_ + dirRow_[d]);
			}
		}
	}

	public bool ShowTarget(bool isBlackTurn)
	{
		bool isPutable = false;
		for (int r = 0; r < rowNum_; ++r) {
			for (int c = 0; c < colNum_; ++c) {
				GameObject grid = GameManager.instance.boardManager.GetGrid(c, r);
				GameObject stone = grid.GetComponent<GridScript>().GetStone();
				GameObject target = grid.GetComponent<GridScript>().GetTarget();
				Destroy(target);
				if (!stone)
				{
					if (grid.GetComponent<GridScript> ().JudgeStonePutable(isBlackTurn)) {
						grid.GetComponent<GridScript> ().PutTarget (isBlackTurn);
						isPutable = true;
					}
				}
			}
		}
		return isPutable;
	}

	public void JudgeGame()
	{
		//int is1PWin = 0;
		int WhiteNum = 0;
		int BlackNum = 0;
		for (int r = 0; r < rowNum_; ++r) {
			for (int c = 0; c < colNum_; ++c) {
				GameObject grid = GameManager.instance.boardManager.GetGrid(c, r);
				GameObject stone = grid.GetComponent<GridScript>().GetStone();
				if (stone)
				{
					if (stone.GetComponent<StoneScript> ().IsBlack()) {
						BlackNum++;
					} else {
						WhiteNum++;
					}
				}
			}
		}
		if (WhiteNum < BlackNum) {
			StartCoroutine (ChangeScene ());
		} else if (WhiteNum == BlackNum) {
			StartCoroutine (ChangeScene2 ());
		} else {
			StartCoroutine (ChangeScene3 ());
		}
	}

	public void PutStone(bool isBlack)
	{
		// 黒か白のPrefabを設定
		GameObject stonePrefab;
		if (isBlack) stonePrefab = blackStonePrefab_;
		else stonePrefab = whiteStonePrefab_;

		// 石を置く
		stone_ = (GameObject)Instantiate(stonePrefab, transform.position, Quaternion.identity);

		// 石の色を設定
		stone_.GetComponent<StoneScript>().IsBlack(isBlack);
	}

	public void PutTarget(bool isBlack)
	{
		// 黒か白のPrefabを設定
		GameObject targetPrefab;
		if (isBlack) targetPrefab = blackTargetPrefab_;
		else targetPrefab = whiteTargetPrefab_;

		//ターゲットを置く
		target_ = (GameObject)Instantiate(targetPrefab, transform.position, Quaternion.identity);

		// ターゲットの色を設定
		target_.GetComponent<TargetScript>().IsBlack(isBlack);
	}

	IEnumerator ChangeScene()
	{
		// 4秒間待機
		yield return new WaitForSeconds(2);
		Application.LoadLevel("Result");
	}

	IEnumerator ChangeScene2()
	{
		// 4秒間待機
		yield return new WaitForSeconds(2);
		Application.LoadLevel("Result2");
	}
	IEnumerator ChangeScene3()
	{
		// 4秒間待機
		yield return new WaitForSeconds(2);
		Application.LoadLevel("Result3");
	}
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}
}