using UnityEngine;
using System.Collections;

public class BarCollision : MonoBehaviour {
	public static readonly int UnderPositionY = -350;

	// コライダーとリジットボディを利用した当たり判定を行う関数.
	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.tag == "Item") {
			c.gameObject.transform.localPosition = new Vector3(c.gameObject.transform.localPosition.x,
			                                                   (float)UnderPositionY,
			                                                   c.gameObject.transform.localPosition.z);
		}
	}
}
