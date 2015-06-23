using UnityEngine;
using System.Collections;

public class Contribute : MonoBehaviour {
	UILabel m_topScoreLabel;
	UILabel m_topNameLabel;
	UIInput m_nameInput;

	// Use this for initialization
	void Start () {
		m_topScoreLabel = GameObject.Find ("UI Root/Panel/TopScore").GetComponent<UILabel> ();
		m_topNameLabel = GameObject.Find ("UI Root/Panel/TopName").GetComponent<UILabel> ();
		m_nameInput = GameObject.Find ("UI Root/Panel/NameInput").GetComponent<UIInput> ();
		m_topScoreLabel.text = "TopScore:" + TopScore.GetTopScore ().ToString ();
		if (!TopScore.GetNewFlag ()) {
			m_topNameLabel.text = "        name:" + TopScore.GetTopName ().ToString ();
			m_nameInput.transform.localPosition = new Vector3(-200.0f,-500.0f,0.0f);
		}
		else {
			m_topNameLabel.text = "new recode! Please input your name.";
			m_nameInput.transform.localPosition = new Vector3(-200.0f,-60.0f,0.0f);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if(TopScore.GetNewFlag()){
				CompletionNameInput();
				TopScore.m_newFlag = false;
				m_topNameLabel.text = "        name:" + TopScore.GetTopName ().ToString ();
				m_nameInput.transform.localPosition = new Vector3(-200.0f,-500.0f,0.0f);
			}
			else{
				Application.LoadLevel("Title");
			}
		}
	}

	void CompletionNameInput(){
		TopScore.m_name = m_nameInput.value;
	}
}
