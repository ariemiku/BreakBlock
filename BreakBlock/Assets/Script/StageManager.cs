using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {
	public enum eStage {
		Stage1,
		Stage2,
		Stage3,
		Stage4,
		Stage5,
	};

	public static int m_stageNum = 1;
	private static StageManager s_instance;
	eStage m_stage;
	bool m_lastStage = false;
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
		m_stageNum = 1;
	}
	
	void StartStage2 () {
		Debug.Log ("ステージ2");
		block = GameObject.Find ("UI Root/Panel/BlockList2");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
		m_stageNum = 2;
	}
	
	void StartStage3 () {
		Debug.Log ("ステージ3");
		block = GameObject.Find ("UI Root/Panel/BlockList3");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
		m_stageNum = 3;
	}
	
	void StartStage4 () {
		Debug.Log ("ステージ4");
		block = GameObject.Find ("UI Root/Panel/BlockList4");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
		m_stageNum = 4;
	}

	void StartStage5 () {
		Debug.Log ("ステージ5");
		block = GameObject.Find ("UI Root/Panel/BlockList5");
		block.transform.localPosition = new Vector3 (block.transform.localPosition.x,0.0f,0.0f);
		m_lastStage = true;	
		m_stageNum = 5;
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
