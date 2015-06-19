﻿using UnityEngine;
using System.Collections;

using System.Collections.Generic;
//using UnityEngine.UI;

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
	public static readonly float InitializeSpeed = 3.0f;
	protected Vector2 position;		// 位置座標.
	protected int life;				// 命.
	protected UISprite sprite;		// バー本体.
	//BarCollision barCollision;
	protected float speed;
	protected UILabel label;

	public cBar(){
		sprite = GameObject.Find ("UI Root/Panel/Bar").GetComponent<UISprite> ();
		label = GameObject.Find ("UI Root/Panel/Life").GetComponent<UILabel> ();
		position = new Vector2 (0.0f,0.0f);
		position.x = sprite.transform.localPosition.x;
		position.y = sprite.transform.localPosition.y;
		life = 3;
		speed = InitializeSpeed;
	}

	// 位置を設定する関数.
	public void SetPosition(float x){
		position.x = x;
		sprite.transform.localPosition = new Vector3(position.x,position.y,0.0f);
	}

	// 位置を取得する関数.
	public Vector2 GetPosition(){
		return position;
	}

	// 移動処理を行う関数.
	public void Move(eKeyCode keyCode = eKeyCode.None){
		float moveX = 0;
		switch (keyCode) {
		// 左が押された場合.
		case eKeyCode.LeftArrow:
			moveX = -speed;
			if(sprite.transform.localPosition.x <= -180){
				return;
			}
			break;
		// 右が押された場合.
		case eKeyCode.RightArrow:
			moveX = speed;
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

	public void Scaling(){
		sprite.transform.localScale =  new Vector3(1.5f,sprite.transform.localScale.y,
		                                           sprite.transform.localScale.z);
	}

	public void SetInitialize(){
		sprite.transform.localScale = new Vector3(1.0f,sprite.transform.localScale.y,
		                                         sprite.transform.localScale.z);
		speed = InitializeSpeed;
	}

	public void UpSpeed(){
		speed = InitializeSpeed * 2;
	}

	public void DrawLabel(){
		label.text = "Life:" + life.ToString ();
	}

	public void AddLife(){
		life += 1;
	}
	// 命を減らす関数.
	public void SubtractionLife(){
		life -= 1;
	}

	public int GetLife(){
		return life;
	}
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
		speed = 3.0f;
	}

	public cBall(string place){
		sprite = GameObject.Find (place).GetComponent<UISprite> ();
		// 初期設定としてボールの向きを傾けておく.
		rotation = -45.0f;
		sprite.transform.rotation = Quaternion.AngleAxis (rotation,Vector3.forward);
		// 仮の初期値.
		speed = 3.0f;
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
		case eReflectCode.UnderWall:
			// 下の壁に衝突.
			wallAngle = 270.0f;
			break;
		case eReflectCode.Bar:
			// Barに衝突.
			wallAngle = 270.0f;
			break;
		}

		// 反射角を求める.
		rotation = (2 * wallAngle) - rotation;
		// 求めた角度の方向に向きを合わせる.
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

	public void Scaling(){
		sprite.transform.localScale =  new Vector3(1.5f,1.5f,sprite.transform.localScale.z);
	}

	public Vector2 GetPosition(){
		return sprite.transform.localPosition;
	}
	
	// 速度を設定する関数.
	public void SetSpeed(float revisedSpeed){
		speed = revisedSpeed;
	}
	// 速度を変更する関数.
}

class cBlock{
	// ブロックがあるかないか.
	//protected UISprite sprite;			// ブロック本体.
	//protected Vector2 position;			// ブロックの位置座標.
	// アイテムを持つかどうか.
	// 何回当たれば消えるのか.

	// 引数有のコンストラクタ.
	public cBlock(/*int num*/){
		//sprite = GameObject.Find ("UI Root/Panel/BlockList/Block"+num).GetComponent<UISprite> ();
		//position = new Vector2(sprite.transform.localPosition.x,sprite.transform.localPosition.y);
	}

	// ブロックが当たった時の処理.

	// UISpriteを取得する関数
	//public UISprite GetSprite(){
	//	return sprite;
	//}
	public bool CheckDelete(GameObject gameobject){
		//UISprite sprite = gameobject.GetComponent<UISprite> ();


		return true;
	}
}

class cItem{
	public static readonly int UnderPositionY = -350;
	protected Vector2 position;				// アイテムの位置座標.
	protected UISprite sprite;				// アイテム本体.
	protected float speed;
	protected bool fallFlag;
	protected float effectTime;
	protected bool usingFlag = false;
	protected float usingTime = 0.0f;
	protected bool pierceFlag = false;

	public cItem(){
		position = new Vector2 (0.0f,0.0f);
		sprite = GameObject.Find ("UI Root/Panel/Item1").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
		speed = 1.5f;
		fallFlag = false;
		effectTime = 10.0f;
	}

	public void SetInitializePos(){
		position.y = UnderPositionY;
		sprite.transform.localPosition = position;
	}

	public void SetPosition(Vector2 pos){
		position = pos;
		sprite.transform.localPosition = position;
	}

	public void SetFallFlag(bool flag = false){
		fallFlag = flag;
	}

	// 落下処理を行う関数.
	public void Fall(){
		Vector2 pos = sprite.transform.localPosition;
		if (fallFlag) {
			pos.y -= speed;
				
			SetPosition (pos);
			if(pos.y<UnderPositionY){
				fallFlag=false;
			}
		}
	}

	// 動いているか調べる関数.
	public bool CheckMove(){
		if (fallFlag) {
			return true;
		}
		return false;
	}

	// アイテムの効果反映を行う関数.
	public virtual void Effect(cBar bar,cBall ball,cBall ball2,cBall ball3){
	}

	public void SetUsingFlag(bool flag = false){
		usingFlag = flag;
	}

	public bool GetPierceFlag(){
		return pierceFlag;
	}
	
	//public virtual void ReflectionEffect(cBar bar,cBall ball){
	//}
}

class cItem1 : cItem{
	public cItem1(){
		sprite = GameObject.Find ("UI Root/Panel/Item1").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// バーを伸ばす処理.
	public override void Effect(cBar bar,cBall ball,cBall ball2,cBall ball3){
		bar.SetInitialize ();
		bar.Scaling ();
	}
	/*
	public override void ReflectionEffect(cBar bar,cBall ball){
		if (usingFlag) {
			usingTime += Time.deltaTime;
			Debug.Log("in");
			if(usingTime > 3.0f){
				bar.SetInitialize ();
				usingFlag = false;
				usingTime = 0.0f;
			}
		}
	}*/
}

class cItem2 : cItem{
	public cItem2(){
		sprite = GameObject.Find ("UI Root/Panel/Item2").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// ボールを3つにする処理.
	public override void Effect(cBar bar,cBall ball,cBall ball2,cBall ball3){
		ball2.SetPosition (ball.GetPosition());
		ball3.SetPosition (ball.GetPosition());
	}

}

class cItem3 : cItem{
	public cItem3(){
		sprite = GameObject.Find ("UI Root/Panel/Item3").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// 一定時間ブロックを貫通する処理.
	public override void Effect(cBar bar,cBall ball,cBall ball2,cBall ball3){
		pierceFlag = true;	
	}
}

class cItem4 : cItem{
	public cItem4(){
		sprite = GameObject.Find ("UI Root/Panel/Item4").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// バーの移動速度を上げる処理
	public override void Effect(cBar bar,cBall ball,cBall ball2,cBall ball3){
		bar.SetInitialize ();
		bar.UpSpeed ();
	}
}

class cItem5 : cItem{
	public cItem5(){
		sprite = GameObject.Find ("UI Root/Panel/Item5").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// 命を増やす処理
	public override void Effect(cBar bar,cBall ball,cBall ball2,cBall ball3){
		bar.AddLife ();
	}
}

public class Game : MonoBehaviour {
	// 定数
	public static readonly int BlockNum = 66;	// ブロックの合計数

	//private List<string> m_hitHardBlockList = new List<string> ();

	private UILabel m_lavel;

	private eStatus m_Status;

	private cBar m_myBar;
	private GameObject m_bar;
	private BarCollision m_barCollision;

	private cBall m_ball;
	private GameObject m_ballGameObject;
	private BallCollision m_ballCollision;

	private cBall m_ball2;
	private cBall m_ball3;


	private cItem m_item;

	private int m_deleteCount = 0;
	private int m_ballCount=1;

	// Use this for initialization
	void Start () {
		m_lavel = GameObject.Find ("UI Root/Panel/Label").GetComponent<UILabel> ();

		m_bar = GameObject.Find ("Bar");
		m_barCollision = m_bar.GetComponent<BarCollision> ();
		m_myBar = new cBar ();

		m_ball = new cBall ();
		m_ballGameObject = GameObject.Find ("Ball");
		m_ballCollision = m_ballGameObject.GetComponent<BallCollision> ();

		m_ball2 = new cBall ("UI Root/Panel/Ball2");
		m_ball3 = new cBall ("UI Root/Panel/Ball3");

		m_item = new cItem ();
		m_Status = eStatus.Tutorial;

		Transit (m_Status);
	}

	void StartTutorial(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		m_ball.SetPosition (new Vector2 (m_myBar.GetPosition().x+5.0f,m_myBar.GetPosition().y+20.0f));
		m_ballCount = 1;
		m_ball = new cBall ();
		Debug.Log ("Tutorial");
		m_lavel.text = "Tutorial";
	}
	
	void StartPlay(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("Play");
		m_lavel.text = "Game";
	}
	
	void StartGameover(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("Gameover");
		m_lavel.text = "Gameover";
	}

	void StartGameClear(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("GameClear");
		m_lavel.text = "GameClear";
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
		// 壁にぶつかっていない間移動可能.
		if (!m_barCollision.HitWall ()) {
			// RightArrowキーで右へ移動.
			if (Input.GetKey (KeyCode.RightArrow) && !Input.GetKey (KeyCode.LeftArrow)) {
				m_myBar.Move (eKeyCode.RightArrow);
			} 
			// LeftArrowキーで左へ移動.
			if (Input.GetKey (KeyCode.LeftArrow) && !Input.GetKey (KeyCode.RightArrow)) {
				m_myBar.Move (eKeyCode.LeftArrow);
			}
		}

		if (m_barCollision.HitItem()) {
			m_item.SetUsingFlag(true);
			m_item.Effect(m_myBar,m_ball,m_ball2,m_ball3);
			m_item.SetInitializePos();
		}

		if (m_ballCollision.Over ()) {
			m_ball.SetSpeed(0.0f);
			m_ballCount-=1;
			m_ballCollision.SetOverFlag();
		}
		if (m_ballCollision.Reflect()) {
			m_deleteCount = m_ballCollision.DeleteCount();
			ManageHit(m_ball,m_ballCollision.HitTagName());
		}
		m_ball.Move();
				
		m_myBar.DrawLabel ();
		m_item.Fall ();

		//m_item.ReflectionEffect (m_myBar,m_ball);
		// ---------------------------------------------------------
		// ゲームオーバーへ遷移
		if (m_ballCount <= 0) {
			Debug.Log(m_myBar.GetLife());
			if(m_myBar.GetLife() <= 0){
				Transit (eStatus.Gameover);
			}else{
				m_myBar.SubtractionLife();
				Transit (eStatus.Tutorial);
			}
		}
		// ゲームクリアへ遷移.
		if (m_deleteCount >= BlockNum) {
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

	// アイテムをランダムでセットする関数.
	void SetRandomItem(){
		int randomNum = Random.Range (1,5+1);
		switch (randomNum) {
		case 1:
			m_item = new cItem1();
			break;
		case 2:
			m_item = new cItem2();
			break;
		case 3:
			m_item = new cItem3();
			break;
		case 4:
			m_item = new cItem4();
			break;
		case 5:
			m_item = new cItem5();
			break;
		}
	}

	void ManageHit(cBall ball, string hitTagName){
		switch(hitTagName){
		case null:
			break;
		case "RightWall":
			ball.Reflect(eReflectCode.RightWall);
			break;
		case "LeftWall":
			ball.Reflect(eReflectCode.LeftWall);
			break;
		case "TopWall":
			ball.Reflect(eReflectCode.TopWall);
			break;
		case "Bar":
			m_ball.Reflect(eReflectCode.Bar);
			break;
		case "BlockTop":
			if(!m_item.GetPierceFlag()){
				ball.Reflect(eReflectCode.UnderWall);
			}
			break;
		case "BlockUnder":
			if(!m_item.GetPierceFlag()){
				ball.Reflect(eReflectCode.TopWall);
			}
			if(!m_item.CheckMove()){
				SetRandomItem();
				m_item.SetPosition(m_ballCollision.BreakBlockPosition());
				m_item.SetFallFlag(true);
			}
			break;
		case "BlockRight":
			if(!m_item.GetPierceFlag()){
				ball.Reflect(eReflectCode.LeftWall);
			}
			break;
		case "BlockLeft":
			if(!m_item.GetPierceFlag()){
				ball.Reflect(eReflectCode.RightWall);
			}
			break;
		}
		Debug.Log (m_item.GetPierceFlag());
		m_ballCollision.SetReflectFlag (false);

		// ブロックを消した数でボールの速さを変える.
		if (m_deleteCount >= (int)(BlockNum * 0.2f)) {
			m_ball.SetSpeed(4.0f);
			m_ball2.SetSpeed(4.0f);
			m_ball3.SetSpeed(4.0f);
		}
		if (m_deleteCount >= (int)(BlockNum * 0.8f)) {
			m_ball.SetSpeed(6.0f);
			m_ball2.SetSpeed(6.0f);
			m_ball3.SetSpeed(6.0f);
		}
	}
}
