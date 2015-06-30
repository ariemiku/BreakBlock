using UnityEngine;
using System.Collections;

public class BallCollision : MonoBehaviour {
	private bool m_overFlag = false;
	private string m_hitTagName = null;
	private bool m_reflectFlag = false;
	private Vector2 m_position = new Vector2 (0.0f,0.0f);
	private int m_deleteCount = 0;

	// コライダーとリジットボディを利用した当たり判定を行う関数.
	void OnTriggerEnter2D(Collider2D c){
		m_reflectFlag = true;
						
		// ブロックに衝突した場合
		if (c.gameObject.tag == "BlockTop") {
			m_hitTagName = "BlockTop";
			m_deleteCount+=1;
			m_position = new Vector2(c.gameObject.transform.parent.gameObject.transform.localPosition.x,
			                         c.gameObject.transform.parent.gameObject.transform.localPosition.y);
			// 衝突したブロック自体（親）を消す
			Destroy(c.gameObject.transform.parent.gameObject);
		}
		else if (c.gameObject.tag == "BlockUnder") {
			m_hitTagName = "BlockUnder";
			m_deleteCount+=1;
			m_position = new Vector2(c.gameObject.transform.parent.gameObject.transform.localPosition.x,
			                         c.gameObject.transform.parent.gameObject.transform.localPosition.y);
			// 衝突したブロック自体（親）を消す
			Destroy(c.gameObject.transform.parent.gameObject);
		}
		else if (c.gameObject.tag == "BlockRight") {
			m_hitTagName = "BlockRight";
			m_deleteCount+=1;
			m_position = new Vector2(c.gameObject.transform.parent.gameObject.transform.localPosition.x,
			                         c.gameObject.transform.parent.gameObject.transform.localPosition.y);
			// 衝突したブロック自体（親）を消す
			Destroy(c.gameObject.transform.parent.gameObject);
		}
		else if (c.gameObject.tag == "BlockLeft") {
			m_hitTagName = "BlockLeft";
			m_deleteCount+=1;
			m_position = new Vector2(c.gameObject.transform.parent.gameObject.transform.localPosition.x,
			                         c.gameObject.transform.parent.gameObject.transform.localPosition.y);
			// 衝突したブロック自体（親）を消す
			Destroy(c.gameObject.transform.parent.gameObject);
		}


		// 壁にぶつかったら反射を行う.
		if (c.gameObject.tag == "RightWall") {
			m_hitTagName = "RightWall";
		}
		if (c.gameObject.tag == "LeftWall") {
			m_hitTagName = "LeftWall";
		}
		if (c.gameObject.tag == "TopWall") {
			m_hitTagName = "TopWall";
		}
		if (c.gameObject.tag == "Under") {
			m_hitTagName = "Under";
		}
		if (c.gameObject.tag == "Bar") {
			m_hitTagName = "Bar";
		}	
	}

	void OnTriggerExit2D(Collider2D c){
		if (c.gameObject.tag == "UnderWall") {
			m_overFlag = true;
		}
	}


	public bool Over(){
		return m_overFlag;
	}

	public void SetOverFlag(bool flag = false){
		m_overFlag = false;
	}

	public bool Reflect(){
		return m_reflectFlag;
	}

	public void SetReflectFlag(bool flag = false){
		m_reflectFlag = flag;
		m_hitTagName = null;
	}

	public string HitTagName(){
		return m_hitTagName;
	}

	public Vector2 BreakBlockPosition(){
		return m_position;
	}

	public int DeleteCount(){
		return m_deleteCount;
	}

	public void SetDeleteCount(int num=0){
		m_deleteCount = num;
	}
}
