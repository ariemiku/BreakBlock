using UnityEngine;
using System.Collections;

// ステータス.
public enum eStatus{
	Tutorial,
	Play,
	Gameover,
	GameClear,
};

public class Game : MonoBehaviour {
	private eStatus m_Status;

	// Use this for initialization
	void Start () {
		m_Status = eStatus.Tutorial;
		Transit (m_Status);
	}

	void StartTutorial(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("Tutorial");
	}
	
	void StartPlay(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("Play");
	}
	
	void StartGameover(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("Gameover");
	}

	void StartGameClear(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("GameClear");
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_Status) {
		case eStatus.Tutorial:
			UpdateTutorial ();
			break;
		case eStatus.Play:
			UpdatePlay ();
			break;
		case eStatus.Gameover:
			UpdateGameover ();
			break;
		case eStatus.GameClear:
			UpdateGameClear ();
			break;
		}
	}

	// tutorial状態の更新関数.
	void UpdateTutorial(){
		// enterキーでゲームに遷移する.
		if (Input.GetKeyDown (KeyCode.Return)) {
			Transit (eStatus.Play);
		}
	}
	
	// play状態の更新関数.
	void UpdatePlay(){
		// enterキーでゲームオーバーへ遷移
		if (Input.GetKeyDown (KeyCode.Return)) {
			Transit (eStatus.Gameover);
		}
		// Spaceキーでゲームクリアへ遷移.
		if (Input.GetKeyDown (KeyCode.Space)) {
			Transit (eStatus.GameClear);
		}
	}
	
	// gameover状態の更新関数.
	void UpdateGameover(){
		// enterキーでタイトルに切り替える.
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("Title");
		}
	}

	// gameClear状態の更新関数.
	void UpdateGameClear(){
		// Spaceキーで投稿画面に切り替える.
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Application.LoadLevel("Contribute");
		}
	}
	
	// シーンを代える.
	void Transit(eStatus NextStatus){
		switch (NextStatus) {
		case eStatus.Tutorial:
			m_Status = NextStatus;
			StartTutorial(m_Status);
			break;
		case eStatus.Play:
			m_Status = NextStatus;
			StartPlay(m_Status);
			break;
		case eStatus.Gameover:
			m_Status = NextStatus;
			StartGameover(m_Status);
			break;
		case eStatus.GameClear:
			m_Status = NextStatus;
			StartGameClear(m_Status);
			break;
		}
	}
}
