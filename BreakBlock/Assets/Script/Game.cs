using UnityEngine;
using System.Collections;

// ステータス.
public enum eStatus{
	Tutorial,
	Play,
	Gameover,
	GameClear,
};

// 使用するキー.
enum eKeyCode{
	RightArrow,
	LeftArrow,
	None,
};

class cBar{
	protected Vector2 position;		// 位置座標.
	protected int life;				// 命.
	protected UISprite sprite;		// バー本体.
	protected bool moveFlag;

	public cBar(){
		sprite = GameObject.Find ("UI Root/Panel/Bar").GetComponent<UISprite> ();
		position = new Vector2 (0.0f,0.0f);
		position.x = sprite.transform.position.x;
		position.y = sprite.transform.position.y;
		life = 3;
		moveFlag = true;
	}

	// 位置を設定する関数.
	public void SetPosition(float x){
		position.x = x;
		sprite.transform.position = new Vector3(position.x,position.y,0.0f);
	}

	// 移動処理を行う関数.
	public void Move(eKeyCode keyCode = eKeyCode.None){
		if (moveFlag) {
			float moveX = 0;
			switch (keyCode) {
			// 左が押された場合.
			case eKeyCode.LeftArrow:
				moveX = -0.01f;
				if(sprite.transform.position.x < -0.43){
					return;
				}
				break;
			// 右が押された場合.
			case eKeyCode.RightArrow:
				moveX = 0.01f;
				if(sprite.transform.position.x > 0.43){
					return;
				}
				break;
			default:
				break;
			}
			position.x += moveX;
			SetPosition (position.x);
		}
	}

	public void SetMoveFlag(bool flag = true){
		moveFlag = flag;
	}

	// 命を減らす関数.
}

class cBall{
	protected float speed;			// ボールの速度.
	// 位置座標.

	cBall(){
		// 仮の初期値.
		speed = 0.5f;
	}

	// 反射を行う関数.
	// ボールの移動行う関数.
	public void Move(){
		Debug.Log ("ボールの移動");
	}
	// 速度を設定する関数.
	public void SetSpeed(){
	}
	// 速度を変更する関数.
}

class cBlock{
	// ブロックがあるかないか.
	// ブロック本体.
	// ブロックの位置座標.
	// アイテムを持つかどうか.

	cBlock(){
	}

	// ブロックが当たった時の処理.
	// ブロックを消す関数.
	// 
}

class cItem{
	protected Vector2 position;				// アイテムの位置座標.
	// アイテム本体.
	// アイテムの種類.

	cItem(){
		position = new Vector2 (0.0f,0.0f);
	}

	// 落下処理を行う関数.
	public void Fall(){
	}
	// アイテムの効果反映を行う関数.
	// アイテムをランダムでセットする関数.
}

public class Game : MonoBehaviour {
	private eStatus m_Status;

	private float m_downLAndRKeyTime = 0.0f;

	cBar m_myBar;
	
	// Use this for initialization
	void Start () {
		m_myBar = new cBar ();
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
		// RightArrowキーで右へ移動.
		if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
			m_downLAndRKeyTime += Time.deltaTime;
			
			if (m_downLAndRKeyTime >= 0.01f) {
				m_myBar.Move(eKeyCode.RightArrow);
				m_downLAndRKeyTime = 0.0f;
			}
		} 
		// LeftArrowキーで左へ移動.
		if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
			m_downLAndRKeyTime += Time.deltaTime;
			
			if (m_downLAndRKeyTime >= 0.01f) {
				m_myBar.Move(eKeyCode.LeftArrow);
				m_downLAndRKeyTime = 0.0f;
			}
		}

		if(!Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
			m_downLAndRKeyTime = 0.0f;
		}



		// ---------------------------------------------------------
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

	// コライダーとリジットボディを利用した当たり判定を行う関数.
	void OnTriggerEnter2D (Collider2D c){
		if (c.gameObject.tag == "Stage") {
			Debug.Log("hit");
			m_myBar.SetMoveFlag(false);
		}
	}
}
