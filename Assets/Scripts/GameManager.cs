using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardManager;

	PlayerBase[] players;
	public static bool isBlackTurn;
	public static int Result = 0;
	bool isEnd = true;
	public static int TrunNum;
	public AudioClip PutSound;

	void Awake ()
	{
		//********** 開始 **********// 
		//ゲーム開始時にGameManagerをinstanceに指定
		if (instance == null) {
			instance = this;
			//このオブジェクト以外にGameManagerが存在する時
		} else if (instance != this) {
			//このオブジェクトを破壊する
			Destroy(gameObject);
		}
		//シーン遷移時にこのオブジェクトを受け継ぐ
		DontDestroyOnLoad(gameObject);
		//********** 終了 **********// 
		boardManager = GetComponent<BoardManager>();
	}

	 //Update is called once per frame
	void Update () {
		if (!isEnd) {
			int playerNo = isBlackTurn ? 0 : 1;
			if (players [playerNo].Play ()) {
				isBlackTurn = !isBlackTurn;
			}
			if (!GameManager.instance.boardManager.ShowTarget(isBlackTurn) && !GameManager.instance.boardManager.ShowTarget (!isBlackTurn)) {
				isEnd = true;
				GameManager.instance.boardManager.JudgeGame ();
			}
		}
	}



	public static void AddTrun(){
		TrunNum++;
	}

	public void InitGame(){
		TrunNum = 0;
		isEnd = false;
		GameManager.instance.boardManager.MakeGrids ();
		// Playerクラスの生成
		if (Title.GetIs1pGame()) {
			players = new PlayerBase[] {
				new HumanPlayer (),
				new MachinePlayer (),
			};
		} else {
			players = new PlayerBase[] {
				new HumanPlayer (),
				new HumanPlayer(),
			};
		}

		players[0].IsBlack(true);
		players[1].IsBlack(false);
		for (int i = 0; i < 2; ++i)
		{
			players[i].SetSound(PutSound);
		}

		isBlackTurn = true;
		isEnd = false;

	} 
}