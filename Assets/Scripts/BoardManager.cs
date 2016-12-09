using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject gridPrefab_, blackStonePrefab_, whiteStonePrefab_;
	public GameObject blackTargetPrefab_, whiteTargetPrefab_;
	int colNum_ = 8, rowNum_ = 8; // 縦横のグリッド数
	int planeSize_ = 1; // 1グリッドの大きさ
	GameObject[] grids_ = new GameObject[64];

	public int GetColNum()
	{
		return colNum_;
	}

	public int GetRowNum()
	{
		return rowNum_;
	}

	public GameObject GetGrid(int colNo, int rowNo)
	{
		return grids_[rowNo * colNum_ + colNo];
	}

	// グリッド群（オセロ盤）の生成
	public void MakeGrids()
	{
		// 盤中心から端グリッド中心までの距離[グリッド分]
		float offsetX = colNum_ / 2 - 0.5f;
		float offsetY = rowNum_ / 2 - 0.5f;

		for (int r = 0; r < rowNum_; ++r)
		{
			float posY = (offsetY - r) * planeSize_; // グリッド中心のz座標
			for (int c = 0; c < colNum_; ++c)
			{
				// グリッドの生成
				float posX = (c - offsetX) * planeSize_; // グリッド中心のx座標
				Vector3 pos = new Vector3(posX, posY, 0); // グリッド中心の三次元座標
				GameObject grid = (GameObject)Instantiate(
					gridPrefab_, pos, Quaternion.identity); // グリッドの生成

				// グリッドの登録
				GridScript gridScript = grid.GetComponent<GridScript>();
				gridScript.SetColNo(c);
				gridScript.SetRowNo(r);
				gridScript.SetBlackStonePrefab(blackStonePrefab_);
				gridScript.SetWhiteStonePrefab(whiteStonePrefab_);
				gridScript.SetBlackTargetPrefab(blackTargetPrefab_);
				gridScript.SetWhiteTargetPrefab(whiteTargetPrefab_);
				grids_[r * colNum_ + c] = grid;
			}
		}

		// 初期配置の生成
		int right = colNum_ / 2;
		int left = right - 1;
		int bottom = rowNum_ / 2;
		int top = rowNum_ / 2 - 1;
		grids_[top * colNum_ + left].GetComponent<GridScript>().PutStone(false);
		grids_[bottom * colNum_ + right].GetComponent<GridScript>().PutStone(false);
		grids_[top * colNum_ + right].GetComponent<GridScript>().PutStone(true);
		grids_[bottom * colNum_ + left].GetComponent<GridScript>().PutStone(true);
		grids_[top * (colNum_ - 1) + right + 1].GetComponent<GridScript>().PutTarget(true);
		grids_[bottom * (colNum_ - 1)+ 16].GetComponent<GridScript>().PutTarget(true);
		grids_[top * (colNum_ ) - 5].GetComponent<GridScript>().PutTarget(true);
		grids_[bottom * (colNum_ )+ 5].GetComponent<GridScript>().PutTarget(true);


	}

	public void JudgeGame()
	{
		//int is1PWin = 0;
		int WhiteNum = 0;
		int BlackNum = 0;
		for (int r = 0; r < rowNum_; ++r) {
			for (int c = 0; c < colNum_; ++c) {
				GameObject grid = GetGrid(c, r);
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
			GameManager.Result = 0;
		} else if (WhiteNum > BlackNum) {
			GameManager.Result = 1;
		} else {
			GameManager.Result = 2;
		}
		StartCoroutine (ChangeScene ());
	}

	public bool ShowTarget(bool isBlackTurn)
	{
		bool isPutable = false;
		for (int r = 0; r < rowNum_; ++r) {
			for (int c = 0; c < colNum_; ++c) {
				GameObject grid = GetGrid(c, r);
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

	IEnumerator ChangeScene()
	{
		// 4秒間待機
		yield return new WaitForSeconds(2);
		Application.LoadLevel("Result");
	}

		// Use this for initialization
	void Start () {
			//MakeGrids();
	}

		// Update is called once per frame
	void Update(){
	}
}