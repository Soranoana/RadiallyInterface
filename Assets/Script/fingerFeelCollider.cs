/*
 * ======================================================================
 * Project Name    : RadiallyInterface
 * File Name       : fingerFeelCollider.cs
 * Creation Date   : 2020/01/30
 *  
 * Copyright © 2020 Soranoana(ソラノアナ). All rights reserved.
 *  
 * This source code or any portion thereof must not be  
 * reproduced or used in any manner whatsoever.
 * However, use is permitted only when the name of 
 * the quotation source or the creator is specified.
 * ======================================================================
 */
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

/* Convexを使えない問題が発生したため、
 * 以下にてトリガーイベントを代行する、
 * スクリプトを指先に配置する
 */

public class fingerFeelCollider : MonoBehaviour {

	void Start() {

	}

	void Update() {
		Collider[] cols = Physics.OverlapSphere(transform.position, transform.localScale.x / 2f);
		Vector3 myPosition = transform.position; // for example
		string a = "";
		foreach (Collider col in cols) {
			Vector3 closestPoint = col.ClosestPoint(myPosition);
			Vector3 positionDifference = ( closestPoint - myPosition );
			Vector3 overlapDirection = positionDifference.normalized;
			a += col.gameObject.name + " ";
		}
		// Debug.Log(a);
	}

	public void OnTriggerEnter(Collider other) {
		try {
			other.gameObject.GetComponent<MultipleTrapezoidPole>().OnTriggerEnterOwnMade(this.gameObject);
			// Debug.Log("run");
		} catch {
			// Debug.Log("指がキーではないオブジェクトに接触しました");
		}
		try {
			other.gameObject.GetComponent<PolygonalPillar>().OnTriggerEnterOwnMade(this.gameObject);
		} catch {
			// Debug.Log("指が多角柱ではないオブジェクトに接触しました");
		}
	}

	public void OnTriggerExit(Collider other) {
		try {
			other.gameObject.GetComponent<MultipleTrapezoidPole>().OnTriggerExitOwnMade(this.gameObject);
		} catch {
			// Debug.Log("指がキーではないオブジェクトとの接触を終えました");
		}
		try {
			other.gameObject.GetComponent<PolygonalPillar>().OnTriggerEnterOwnMade(this.gameObject);
		} catch {
			// Debug.Log("指が多角柱ではないオブジェクトに接触を終えました");
		}

	}
}
