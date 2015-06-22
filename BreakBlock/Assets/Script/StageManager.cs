using UnityEngine;
using System.Collections;

/*
class cBlock{
	// ブロックがあるかないか.
	protected UISprite[] block = new UISprite[66];			// ブロック本体.
	//protected Vector2 position;			// ブロックの位置座標.
	// アイテムを持つかどうか.
	// 何回当たれば消えるのか.

	// 引数有のコンストラクタ.
	public cBlock(int num){
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
}*/

public class StageManager : MonoBehaviour {
	public enum eStage {
		Stage1,
		Stage2,
		Stage3,
		Stage4,
		Stage5,
	};

	private static StageManager s_instance;
	eStage m_stage;
	bool m_lastStage = false;
	//protected UISprite[] block = new UISprite[66];
	private GameObject block;

	// Use this for initialization
	void Start () {
	}



	// Update is called once per frame
	void Update () {
		switch (m_stage) {
		case eStage.Stage1:
			break;
		case eStage.Stage2:
			break;
		case eStage.Stage3:
			break;
		case eStage.Stage4:
			break;
		case eStage.Stage5:
			break;
		}
	}

	public static StageManager GetInstance () {
		if (s_instance == null) {
			GameObject gameObject = new GameObject ("StageManager");
			s_instance = gameObject.AddComponent<StageManager> ();
		}
		
		return s_instance;
	}

	public void Transit (eStage nextStage) {
		switch (nextStage) {
		case eStage.Stage1:
			StartStage1 ();
			break;
		case eStage.Stage2:
			StartStage2 ();
			break;
		case eStage.Stage3:
			StartStage3 ();
			break;
		case eStage.Stage4:
			StartStage4 ();
			break;
		case eStage.Stage5:
			StartStage5 ();
			break;
		}
		m_stage = nextStage;
	}

	void StartStage1 () {
		Debug.Log ("ステージ1");
		block = GameObject.Find ("UI Root/Panel/BlockList1");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
	}
	
	void StartStage2 () {
		Debug.Log ("ステージ2");
		block = GameObject.Find ("UI Root/Panel/BlockList2");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
	}
	
	void StartStage3 () {
		Debug.Log ("ステージ3");
		block = GameObject.Find ("UI Root/Panel/BlockList3");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
	}
	
	void StartStage4 () {
		Debug.Log ("ステージ4");
		block = GameObject.Find ("UI Root/Panel/BlockList4");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
	}

	void StartStage5 () {
		Debug.Log ("ステージ5");
		block = GameObject.Find ("UI Root/Panel/BlockList5");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
		m_lastStage = true;	
	}
	
	public void SetNextStage () {
		switch (m_stage) {
		case eStage.Stage1:
			Transit (eStage.Stage2);
			break;
		case eStage.Stage2:
			Transit (eStage.Stage3);
			break;
		case eStage.Stage3:
			Transit (eStage.Stage4);
			break;
		case eStage.Stage4:
			Transit (eStage.Stage5);
			break;
		case eStage.Stage5:
			break;
		}
	}
	
	public bool CheckLastStage () {
		return m_lastStage;
	}
}
