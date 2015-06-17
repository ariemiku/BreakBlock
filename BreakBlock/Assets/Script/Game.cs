using UnityEngine;
using System.Collections;

using UnityEngine.UI;

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

enum eReflectCode{
	RightWall,
	LeftWall,
	TopWall,
	UnderWall,
	Bar,
};

class cBar{
	protected Vector2 position;		// 位置座標.
	protected int life;				// 命.
	protected UISprite sprite;		// バー本体.
	protected bool moveFlag;

	public cBar(){
		sprite = GameObject.Find ("UI Root/Panel/Bar").GetComponent<UISprite> ();
		position = new Vector2 (0.0f,0.0f);
		position.x = sprite.transform.localPosition.x;
		position.y = sprite.transform.localPosition.y;
		life = 3;
		moveFlag = true;
	}

	// 位置を設定する関数.
	public void SetPosition(float x){
		position.x = x;
		sprite.transform.localPosition = new Vector3(position.x,position.y,0.0f);
	}

	// 移動処理を行う関数.
	public void Move(eKeyCode keyCode = eKeyCode.None){
		if (moveFlag) {
			float moveX = 0;
			switch (keyCode) {
			// 左が押された場合.
			case eKeyCode.LeftArrow:
				moveX = -5.0f;
				if(sprite.transform.localPosition.x <= -180){
					return;
				}
				break;
			// 右が押された場合.
			case eKeyCode.RightArrow:
				moveX = 5.0f;
				if(sprite.transform.localPosition.x >= 180){
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
	protected UISprite sprite;		// ボール本体.
	protected float rotation;

	public cBall(){
		sprite = GameObject.Find ("UI Root/Panel/Ball").GetComponent<UISprite> ();
		// 初期設定としてボールの向きを傾けておく.
		rotation = -45.0f;
		sprite.transform.rotation = Quaternion.AngleAxis (rotation,Vector3.forward);
		// 仮の初期値.
		speed = 2.5f;
	}

	// 位置を設定する関数.
	public void SetPosition(Vector2 position){
		sprite.transform.localPosition = new Vector3(position.x,position.y,0.0f);
	}

	// 反射を行う関数.
	public void Reflect(eReflectCode reflectCode){
		//float reflectAngle;
		float wallAngle = 0.0f;
		switch (reflectCode) {
		case eReflectCode.RightWall:
			// 右の壁に衝突.
			wallAngle = 180.0f;
			break;
		case eReflectCode.LeftWall:
			// 左の壁に衝突.
			wallAngle = 0.0f;
			break;
		case eReflectCode.TopWall:
			// 上の壁に衝突.
			wallAngle = 90.0f;
			break;
		case eReflectCode.Bar:
			// 上の壁に衝突.
			wallAngle = 270.0f;
			break;
		case eReflectCode.UnderWall:
			// 下の壁に衝突.
			wallAngle = 270.0f;
			break;
		}


		rotation = 2 * wallAngle - rotation;
		sprite.transform.rotation = Quaternion.AngleAxis (rotation,Vector3.forward);
	}
	
	// ボールの移動行う関数.
	public void Move(){
		Vector2 pos;
		pos.x = sprite.transform.localPosition.x;
		pos.y = sprite.transform.localPosition.y;

		Vector2 move;
		move.x = sprite.transform.up.x * speed;
		move.y = sprite.transform.up.y * speed;

		// 自身の向きに移動
		pos.x += move.x;
		pos.y += move.y;

		SetPosition (pos);
	}


	// 速度を設定する関数.
	/*
	public void SetSpeed(){
	}*/
	// 速度を変更する関数.
}

class cBlock{
	// ブロックがあるかないか.
	// ブロック本体.
	// ブロックの位置座標.
	// アイテムを持つかどうか.

	public cBlock(){
	}

	// ブロックが当たった時の処理.
	// ブロックを消す関数.
	// 
}

class cItem{
	protected Vector2 position;				// アイテムの位置座標.
	// アイテム本体.
	// アイテムの種類.

	public cItem(){
		position = new Vector2 (0.0f,0.0f);
	}

	// 落下処理を行う関数.
	/*
	public void Fall(){
	}*/
	// アイテムの効果反映を行う関数.
	// アイテムをランダムでセットする関数.
}

public class Game : MonoBehaviour {
	private eStatus m_Status;

	private float m_downLAndRKeyTime = 0.0f;

	private cBar m_myBar;
	private cBall m_ball;

	// Use this for initialization
	void Start () {
		m_myBar = new cBar ();
		m_ball = new cBall ();
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
		m_ball.Move();

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
	void OnTriggerEnter2D(Collider2D c){
		// 壁にぶつかったら反射を行う.
		if (c.gameObject.tag == "RightWall") {
			m_ball.Reflect(eReflectCode.RightWall);
		}
		if (c.gameObject.tag == "LeftWall") {
			m_ball.Reflect(eReflectCode.LeftWall);
		}
		if (c.gameObject.tag == "TopWall") {
			m_ball.Reflect(eReflectCode.TopWall);
		}
		if (c.gameObject.tag == "Bar") {
			m_ball.Reflect(eReflectCode.Bar);
		}
		if (c.gameObject.tag == "UnderWall") {
			m_ball.Reflect(eReflectCode.UnderWall);
		}
	}
	
	void OnCollisionEnter(Collision collision ) {
		Debug.Log("hit2");
	}
}
