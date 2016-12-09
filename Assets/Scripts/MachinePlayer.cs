using UnityEngine;
using System.Collections;

public class MachinePlayer : PlayerBase
{
	void SearchPutableGrid(out bool[] isPutableGrid)
	{
		int colNum = GameManager.instance.boardManager.GetColNum();
		int rowNum = GameManager.instance.boardManager.GetRowNum();
		isPutableGrid = new bool[colNum * rowNum];

		for (int r = 0; r < rowNum; ++r)
		{
			for (int c = 0; c < colNum; ++c)
			{
				GridScript gridScript = GameManager.instance.boardManager.GetGrid(c, r).GetComponent<GridScript>();
				if (!gridScript.GetStone() && gridScript.JudgeStonePutable(isBlack_))
				{
					isPutableGrid[r * colNum + c] = true;
				}
				else
				{
					isPutableGrid[r * colNum + c] = false;
				}
			}
		} 
	}

	bool SelectGrid(out GridScript gridScript)
	{
		bool[] isPutableGrid;
		SearchPutableGrid(out isPutableGrid);

		int colNum = GameManager.instance.boardManager.GetColNum();
		int rowNum = GameManager.instance.boardManager.GetRowNum();
		for (int r = 0; r < rowNum; ++r)
		{
			for (int c = 0; c < colNum; ++c)
			{
				int index = r * colNum + c;
				if (isPutableGrid[index])
				{
					gridScript = GameManager.instance.boardManager.GetGrid(c, r).GetComponent<GridScript>();
					return true;
				}
			}
		}
		gridScript = null;
		return false;
	}

	public override bool Play()
	{
		if (GameManager.instance.boardManager.ShowTarget (isBlack_)) {
			// 石を置くグリッドを選ぶ
			GridScript gridScript;
			SelectGrid(out gridScript);
			if (!gridScript) Application.Quit();

			// 石を置く
			gridScript.PutStone(isBlack_);
			SoundManager.instance.RandomizeSfx(PutSound);

			// ひっくり返す
			gridScript.TurnStone(isBlack_);

			GameManager.AddTrun ();
			return true;
		} else {
			return true;
		}
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