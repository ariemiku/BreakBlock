using UnityEngine;
using System.Collections;

public class BarCollision : MonoBehaviour {
	public static readonly int UnderPositionY = -350;
	private bool m_hitStageFlag = false;
	private bool m_hitItemFlag = false;

	// コライダーとリジットボディを利用した当たり判定を行う関数.
	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.tag == "Item") {
			m_hitItemFlag = true;
		}

		if(c.gameObject.tag == "Stage"){
			m_hitStageFlag = true;
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if (c.gameObject.tag == "Item") {
			m_hitItemFlag = false;
		}

		if(c.gameObject.tag == "Stage"){
			m_hitStageFlag = false;
		}
	}

	public bool HitWall(){
		return m_hitStageFlag;
	}

	public bool HitItem(){
		return m_hitItemFlag;
	}
}
