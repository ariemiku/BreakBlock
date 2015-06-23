using UnityEngine;
using System.Collections;

using System;

public class Contribute : MonoBehaviour {
	UILabel m_topScoreLabel;
	UILabel m_topNameLabel;
	UILabel m_descriptionLabel;
	UIInput m_nameInput;
	
	DateTime m_nowDate;

	// Use this for initialization
	void Start () {
		m_topScoreLabel = GameObject.Find ("UI Root/Panel/TopScore").GetComponent<UILabel> ();
		m_topNameLabel = GameObject.Find ("UI Root/Panel/TopName").GetComponent<UILabel> ();
		m_descriptionLabel = GameObject.Find ("UI Root/Panel/Description").GetComponent<UILabel> ();
		m_nameInput = GameObject.Find ("UI Root/Panel/NameInput").GetComponent<UIInput> ();
		m_topScoreLabel.text = "Top Score : " + TopScore.GetTopScore ().ToString ();
		if (!TopScore.GetNewFlag ()) {
			m_topNameLabel.text = "Top Name : " + TopScore.GetTopName ().ToString ();
			m_nameInput.transform.localPosition = new Vector3(-200.0f,-500.0f,0.0f);
			m_descriptionLabel.text = "push Enter";
		}
		else {
			m_topNameLabel.text = "new recode! Please input your name.";
			m_nameInput.transform.localPosition = new Vector3(-200.0f,-60.0f,0.0f);
			m_descriptionLabel.text = "contribute score in enter";
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			// 新記録だった場合 入力された名前を格納する.
			if(TopScore.GetNewFlag()){
				CompletionNameInput();
				TopScore.m_newFlag = false;
				m_topNameLabel.text = "        name:" + TopScore.GetTopName ().ToString ();
				m_nameInput.transform.localPosition = new Vector3(-200.0f,-500.0f,0.0f);
				m_descriptionLabel.text = "Contribution completion!\npush Enter";
				// 現在の日付と時刻を取得する
				m_nowDate = DateTime.Now;
				// 取得した日付と時刻を表示する
				Debug.Log(m_nowDate.ToString());
			}
			else{
				// タイトルに遷移する.
				Application.LoadLevel("Title");
			}
		}
	}

	// 入力された名前を格納する.
	void CompletionNameInput(){
		TopScore.m_name = m_nameInput.value;
	}
}
