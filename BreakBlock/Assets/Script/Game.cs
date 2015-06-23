using UnityEngine;
using System.Collections;

using System.Collections.Generic;
//using UnityEngine.UI;

// ステータス.
public enum eStatus{
	Tutorial,
	Play,
	Gameover,
	GameClear,
	GameComplete,
};

// 使用するキー.
enum eKeyCode{
	RightArrow,
	LeftArrow,
	None,
};

// 跳ね返る壁.
enum eReflectCode{
	RightWall,
	LeftWall,
	TopWall,
	UnderWall,
	Bar,
};

// アイテム.
enum eItemCode{
	Item1,
	Item2,
	Item3,
	Item4,
	Item5,
	None,
};

class cBar{
	public static readonly float InitializeSpeed = 3.0f;
	protected Vector2 position;		// 位置座標.
	protected int life;				// 命.
	protected UISprite sprite;		// バー本体.
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
			if(sprite.transform.localPosition.x <= -180.0f ||
			   sprite.transform.localScale.x == 1.5f && sprite.transform.localPosition.x <= -160.0f ){
				return;
			}
			break;
		// 右が押された場合.
		case eKeyCode.RightArrow:
			moveX = speed;
			if(sprite.transform.localPosition.x >= 180 ||
			   sprite.transform.localScale.x == 1.5f && sprite.transform.localPosition.x >= 160.0f ){
				return;
			}
			break;
		default:
			break;
		}
		position.x += moveX;
		SetPosition (position.x);
	}

	// バーの大きさを変更する関数.
	public void SetScaling(float scalX){
		sprite.transform.localScale =  new Vector3(scalX,sprite.transform.localScale.y,
		                                           sprite.transform.localScale.z);
		CheckOver (scalX);
	}

	// バーの大きさを変更した時はみ出さないようにする関数.
	void CheckOver(float scalX){
		if (scalX == 1.5f) {
			if (sprite.transform.localPosition.x > 160.0f) {
				position.x=160.0f;
				sprite.transform.localPosition = new Vector3(position.x,position.y,0.0f);
			}
			else if(sprite.transform.localPosition.x < -160.0f){
				position.x=-160.0f;
				sprite.transform.localPosition = new Vector3(position.x,position.y,0.0f);
			}
		}
	}

	// バーのステータスをもとに戻す関数.
	public void SetInitialize(){
		sprite.transform.localScale = new Vector3(1.0f,sprite.transform.localScale.y,
		                                         sprite.transform.localScale.z);
		speed = InitializeSpeed;
	}

	// スピードをアップさせる関数.
	public void UpSpeed(){
		speed = InitializeSpeed * 2;
	}

	// 残りの命を描画する関数.
	public void DrawLabel(){
		label.text = "Life:" + life.ToString ();
	}

	// 命を増やす関数.
	public void AddLife(){
		life += 1;
	}
	// 命を減らす関数.
	public void SubtractionLife(){
		life -= 1;
	}

	// 命を取得する関数.
	public int GetLife(){
		return life;
	}
}

class cBall{
	protected float speed;			// ボールの速度.
	protected Vector2 position;		// 位置座標.
	protected UISprite sprite;		// ボール本体.
	protected float rotation;

	public cBall(){
		sprite = GameObject.Find ("UI Root/Panel/Ball").GetComponent<UISprite> ();
		// 初期設定としてボールの向きを傾けておく.
		rotation = -45.0f;
		sprite.transform.rotation = Quaternion.AngleAxis (rotation,Vector3.forward);
		position = new Vector2 (sprite.transform.localPosition.x,sprite.transform.localPosition.y);
		// 仮の初期値.
		speed = 3.0f;
	}

	public cBall(string place){
		sprite = GameObject.Find (place).GetComponent<UISprite> ();
		// 初期設定としてボールの向きを傾けておく.
		rotation = -45.0f;
		sprite.transform.rotation = Quaternion.AngleAxis (rotation,Vector3.forward);
		sprite.transform.localPosition = new Vector3(50.0f,-400.0f,0.0f);
		position = new Vector2 (sprite.transform.localPosition.x,sprite.transform.localPosition.y);
		// 仮の初期値.
		speed = 0.0f;
	}
	
	// 位置を設定する関数.
	public void SetPosition(Vector2 pos){
		position = pos;
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

		// 自身の向きに移動.
		if (NotFrameOver (pos)) {
			pos.x += move.x;
			pos.y += move.y;

			SetPosition (pos);
		}
	}
	
	bool NotFrameOver(Vector2 pos){
		if (pos.x < -213.0f) {
			Reflect(eReflectCode.LeftWall);
			pos.x=-212.0f;
			SetPosition (pos);
			return false;
		}
		if (pos.x > 213.0f){
			Reflect(eReflectCode.RightWall);
			pos.x=212.0f;
			SetPosition (pos);
			return false;
		}
		if(pos.y > 313.0f){
			Reflect(eReflectCode.TopWall);
			pos.y=313.0f;
			SetPosition (pos);
			return false;
		}
		return true;
	}

	// ボールの傾きを取得する関数.
	public float GetLotation(){
		return rotation;
	}

	// ボールの傾きをセットする関数.
	public void SetLotation(float revisedRotation){
		rotation = revisedRotation;
		sprite.transform.rotation = Quaternion.AngleAxis (rotation,Vector3.forward);
	}

	// 位置を取得する関数.
	public Vector2 GetPosition(){
		return position;
	}
	
	// 速度を設定する関数.
	public void SetSpeed(float revisedSpeed){
		speed = revisedSpeed;
	}

	// 速度を取得する関数.
	public float GetSpeed(){
		return speed;
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


	public cItem(){
		position = new Vector2 (0.0f,-350.0f);
		sprite = GameObject.Find ("UI Root/Panel/Item1").GetComponent<UISprite> ();
		position.x = sprite.transform.localPosition.x;
		sprite.transform.localPosition = position;
		speed = 1.5f;
		fallFlag = false;
		effectTime = 10.0f;
	}

	// アイテムの位置の初期化を行う関数.
	public void SetInitializePos(){
		position.y = UnderPositionY;
		sprite.transform.localPosition = position;
	}

	// 位置を設定する関数.
	public void SetPosition(Vector2 pos){
		position = pos;
		sprite.transform.localPosition = position;
	}

	// 落下フラグを設定する関数.
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
	public virtual void Effect(cBar bar,cBall ball,cBall ball2){
	}

	// アイテムが取得されているかどうか設定する関数.
	public void SetUsingFlag(bool flag = false){
		usingFlag = flag;
	}
}

// バーを伸ばすアイテム
class cItem1 : cItem{
	public cItem1(){
		sprite = GameObject.Find ("UI Root/Panel/Item1").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// バーを伸ばす処理.
	public override void Effect(cBar bar,cBall ball,cBall ball2){
		bar.SetInitialize ();
		bar.SetScaling (1.5f);
	}
}

// ボールを2つにするアイテム
class cItem2 : cItem{
	public cItem2(){
		sprite = GameObject.Find ("UI Root/Panel/Item2").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// ボールを2つにする処理.
	public override void Effect(cBar bar,cBall ball,cBall ball2){
		if (ball2.GetSpeed () <= 0.0f) {
			ball2.SetPosition (ball.GetPosition ());
			ball2.SetLotation (ball.GetLotation () + 90.0f);

			ball2.SetSpeed (ball.GetSpeed ());
		}

		if (ball.GetSpeed () <= 0.0f) {
			ball.SetPosition (ball2.GetPosition ());
			ball.SetLotation (ball2.GetLotation () + 90.0f);
			
			ball.SetSpeed (ball2.GetSpeed ());
		}
	}

}

// 一定時間ブロックを貫通するアイテム
class cItem3 : cItem{
	public cItem3(){
		sprite = GameObject.Find ("UI Root/Panel/Item3").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// 一定時間ブロックを貫通する処理.
	public override void Effect(cBar bar,cBall ball,cBall ball2){
		bar.SetInitialize ();
	}
}

// バーの移動速度を上げるアイテム
class cItem4 : cItem{
	public cItem4(){
		sprite = GameObject.Find ("UI Root/Panel/Item4").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// バーの移動速度を上げる処理
	public override void Effect(cBar bar,cBall ball,cBall ball2){
		bar.SetInitialize ();
		bar.UpSpeed ();
	}
}

// 命を増やすアイテム
class cItem5 : cItem{
	public cItem5(){
		sprite = GameObject.Find ("UI Root/Panel/Item5").GetComponent<UISprite> ();
		position = sprite.transform.localPosition;
	}

	// 命を増やす処理
	public override void Effect(cBar bar,cBall ball,cBall ball2){
		bar.AddLife ();
	}
}

public class Game : MonoBehaviour {
	// 定数
	public static readonly int BlockNum = 66;	// ブロックの合計数

	private UILabel m_lavel;

	private UILabel m_descriptionLabel;

	private UILabel m_scoreLabel;
	private int m_score = 0;

	private eStatus m_Status;

	private cBar m_myBar;
	private GameObject m_bar;
	private BarCollision m_barCollision;

	private cBall m_ball;
	private GameObject m_ballGameObject;
	private BallCollision m_ballCollision;

	private cBall m_ball2;
	private GameObject m_ballGameObject2;
	private BallCollision m_ballCollision2;

	private cItem m_item;
	private eItemCode m_itemCode = eItemCode.None;
	private eItemCode m_usingItemCode = eItemCode.None;
	private bool m_usingItem = false;
	private float usingTime = 0.0f;

	private int m_deleteCount = 0;

	// Use this for initialization
	void Start () {
		m_lavel = GameObject.Find ("UI Root/Panel/Label").GetComponent<UILabel> ();
		m_scoreLabel = GameObject.Find ("UI Root/Panel/Score").GetComponent<UILabel> ();
		m_descriptionLabel = GameObject.Find ("UI Root/Panel/Description").GetComponent<UILabel> ();
		m_descriptionLabel.transform.localPosition = new Vector3 (0.0f,-10.0f,0.0f);

		m_bar = GameObject.Find ("Bar");
		m_barCollision = m_bar.GetComponent<BarCollision> ();
		m_myBar = new cBar ();

		m_ball = new cBall ();
		m_ballGameObject = GameObject.Find ("Ball");
		m_ballCollision = m_ballGameObject.GetComponent<BallCollision> ();

		m_ball2 = new cBall ("UI Root/Panel/Ball2");
		m_ballGameObject2 = GameObject.Find ("Ball2");
		m_ballCollision2 = m_ballGameObject2.GetComponent<BallCollision> ();

		m_item = new cItem ();
		m_Status = eStatus.Tutorial;

		StageManager.GetInstance ().Transit (StageManager.eStage.Stage1);
		Transit (m_Status);
	}

	void StartTutorial(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		m_ball.SetPosition (new Vector2 (m_myBar.GetPosition().x+5.0f,m_myBar.GetPosition().y+20.0f));
		m_ball = new cBall ();
		m_ball2 = new cBall ("UI Root/Panel/Ball2");
		m_item.SetPosition(new Vector2 (0.0f,-350.0f));
		m_myBar.SetInitialize ();
		m_itemCode = eItemCode.None;
		m_usingItemCode = eItemCode.None;
		m_usingItem = false;

		Debug.Log ("Tutorial");
		m_lavel.text = "Stage "+StageManager.m_stageNum;
	}
	
	void StartPlay(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("Play");
		m_lavel.text = "Stage "+StageManager.m_stageNum;
	}
	
	void StartGameover(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("Gameover");
		m_descriptionLabel.transform.localPosition = new Vector3 (0.0f,-10.0f,0.0f);
		m_descriptionLabel.text = "Gameover\npush Enter";
		m_lavel.text = "Stage "+StageManager.m_stageNum;
	}

	void StartGameClear(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		if(StageManager.GetInstance ().CheckLastStage ()){
			Transit (eStatus.GameComplete);
		}
		Debug.Log ("GameClear");
		m_deleteCount = 0;
		m_ballCollision.SetDeleteCount ();
		m_ballCollision2.SetDeleteCount ();
		m_myBar.SetInitialize ();
		m_ball.SetPosition (new Vector2 (m_myBar.GetPosition().x+5.0f,m_myBar.GetPosition().y+20.0f));
		m_ball = new cBall ();
		m_ball2 = new cBall ("UI Root/Panel/Ball2");
		m_item.SetPosition(new Vector2 (0.0f,-350.0f));
		m_itemCode = eItemCode.None;
		m_usingItemCode = eItemCode.None;
		m_usingItem = false;
		StageManager.GetInstance ().SetNextStage ();
		m_lavel.text = "Stage "+StageManager.m_stageNum;
	}

	void StartGameComplete(eStatus PrevStatus){
		// 代わった時に1回しかやらないことをする.
		Debug.Log ("GameComplete");
		m_descriptionLabel.transform.localPosition = new Vector3 (0.0f,-10.0f,0.0f);
		m_descriptionLabel.text = "GameComplete\npush Enter";
		m_lavel.text = "Stage "+StageManager.m_stageNum;
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
		case eStatus.GameComplete:
			UpdateGameComplete ();
			break;
		}
	}

	// tutorial状態の更新関数.
	void UpdateTutorial(){
		// enterキーでゲームに遷移する.
		if (Input.GetKeyDown (KeyCode.Return)) {
			Transit (eStatus.Play);
			m_descriptionLabel.transform.localPosition = new Vector3 (0.0f,-550.0f,0.0f);
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

		// アイテム取得時一定時間効果が表れる処理.
		if (m_usingItem == true) {
			usingTime += Time.deltaTime;

			if(usingTime >= 5.0f){
				m_usingItem = false;
				usingTime = 0.0f;
				if(m_usingItemCode==eItemCode.Item1){
					m_myBar.SetScaling(1.0f);
				}
			}
		}

		// アイテムを取得した時の処理
		if (m_barCollision.HitItem()) {
			m_score+=50;
			m_usingItem = true;
			m_item.SetUsingFlag(true);
			m_item.Effect(m_myBar,m_ball,m_ball2);
			m_item.SetInitializePos();
			m_usingItemCode = m_itemCode;
			usingTime = 0.0f;
		}

		MoveBall (m_ball,m_ballCollision);
		MoveBall (m_ball2,m_ballCollision2);

		m_myBar.DrawLabel ();
		m_scoreLabel.text = "Score:" + m_score.ToString ();
		m_item.Fall ();

		// ---------------------------------------------------------
		// ゲームオーバーへ遷移
		if (m_ball.GetSpeed()<=0.0f && m_ball2.GetSpeed()<=0.0f){
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
		// ---------------------------------------------------------
	}
	
	// gameover状態の更新関数.
	void UpdateGameover(){
		// enterキーでタイトルに切り替える.
		if(Input.GetKeyDown(KeyCode.Return))
		{
			// スコアがトップより高かったら投稿画面に遷移する.
			if(TopScore.GetTopScore() < m_score){
				TopScore.m_topScore = m_score;
				TopScore.m_newFlag = true;
				Application.LoadLevel("Contribute");
			}
			else{
				Application.LoadLevel("Title");
			}
		}
	}

	// gameClear状態の更新関数.
	void UpdateGameClear(){
		// Enterキーで切り替える.
		if(Input.GetKeyDown(KeyCode.Return))
		{
			Transit (eStatus.Play);
		}
	}

	void UpdateGameComplete(){
		// enterキーで遷移する.
		if (Input.GetKeyDown (KeyCode.Return)) {
			if(TopScore.GetTopScore() < m_score){
				TopScore.m_topScore = m_score;
				TopScore.m_newFlag = true;
			}
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
		case eStatus.GameComplete:
			m_Status = NextStatus;
			StartGameComplete(m_Status);
			break;
		}
	}

	// ボールの移動処理関連を行う関数.
	void MoveBall(cBall ball,BallCollision ballCollision){
		if (ballCollision.Over () || ball.GetPosition().y <= -400) {
			ball.SetSpeed(0.0f);
			ballCollision.SetOverFlag();
		}
		if (ballCollision.Reflect()) {
			ManageHit(ball,ballCollision);
			m_deleteCount = m_ballCollision.DeleteCount() + m_ballCollision2.DeleteCount();
			ballCollision.SetReflectFlag (false);
		}
		ball.Move();
	}

	// アイテムをランダムでセットする関数.
	void SetRandomItem(){
		int randomNum = Random.Range (1,5+1);
		switch (randomNum) {
		case 1:
			m_item = new cItem1();
			m_itemCode = eItemCode.Item1;
			break;
		case 2:
			m_item = new cItem2();
			m_itemCode = eItemCode.Item2;
			break;
		case 3:
			m_item = new cItem3();
			m_itemCode = eItemCode.Item3;
			break;
		case 4:
			m_item = new cItem4();
			m_itemCode = eItemCode.Item4;
			break;
		case 5:
			m_item = new cItem5();
			m_itemCode = eItemCode.Item5;
			break;
		}
	}

	// ボールが何かにあったった時の処理をする関数
	void ManageHit(cBall ball, BallCollision ballCollision){
		switch(ballCollision.HitTagName()){
		case null:
			break;
		case "Bar":
			ball.Reflect(eReflectCode.Bar);
			break;
		case "BlockTop":
			if(m_usingItemCode == eItemCode.Item3 && m_usingItem == true){
			}
			else{
				ball.Reflect(eReflectCode.UnderWall);
			}
			m_score+=20;
			break;
		case "BlockUnder":
			if(m_usingItemCode == eItemCode.Item3 && m_usingItem == true){
			}
			else{
				ball.Reflect(eReflectCode.TopWall);
			}
			m_score+=20;
			break;
		case "BlockRight":
			if(m_usingItemCode == eItemCode.Item3 && m_usingItem == true){
			}
			else{
				ball.Reflect(eReflectCode.LeftWall);
			}
			m_score+=20;
			break;
		case "BlockLeft":
			if(m_usingItemCode == eItemCode.Item3 && m_usingItem == true){
			}
			else{
				ball.Reflect(eReflectCode.RightWall);
			}
			m_score+=20;
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
		}

		// アイテムを出現させる.
		if((ballCollision.HitTagName() == "BlockTop" || ballCollision.HitTagName() == "BlockUnder" || 
		    ballCollision.HitTagName() == "BlockRight" || ballCollision.HitTagName() == "BlockLeft")
		   && !m_item.CheckMove()){
				SetRandomItem();
				m_item.SetPosition(ballCollision.BreakBlockPosition());
				m_item.SetFallFlag(true);
		}

		// ブロックを消した数でボールの速さを変える.
		if (m_deleteCount >= (int)(BlockNum * 0.2f)) {
			if(m_ball.GetSpeed()!=0.0f){
				m_ball.SetSpeed(4.0f);
			}
			if(m_ball2.GetSpeed()!=0.0f){
				m_ball2.SetSpeed(4.0f);
			}
		}
		if (m_deleteCount >= (int)(BlockNum * 0.8f)) {
			if(m_ball.GetSpeed()!=0.0f){
				m_ball.SetSpeed(6.0f);
			}
			if(m_ball2.GetSpeed()!=0.0f){
				m_ball2.SetSpeed(6.0f);
			}
		}
	}
}
