using UnityEngine;
using System.Collections;

public class TopScore : MonoBehaviour {
	public static int m_topScore = 1000;
	public static string m_name = "one";
	public static bool m_newFlag = false;

	// トップスコアを取得する関数.
	public static int GetTopScore(){
		return m_topScore;
	}

	// 名前を取得する関数.
	public static string GetTopName(){
		return m_name;
	}

	// 過去のスコアを超えたかどうか返す関数.
	public static bool GetNewFlag(){
		return m_newFlag;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
