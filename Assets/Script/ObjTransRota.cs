﻿/*
 * ======================================================================
 * Project Name    : RadiallyInterface
 * File Name       : ObjTransRota.cs
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
using Valve.VR;

/* 指の接触を検知し、ピンチとして動作する
 * ピンチ時には、
 * ピンチ側の手の、人差し指の指先の座標にシステムを配置する
 * ピンチ側の手の、ピンチ解除、及び存在が消えたときは座標決定
 * ピンチと反対側の手の、上下左右前後への相対移動で、ローカルオイラー回転させる
 * ピンチと反対側の手の、存在しない時は回転は一切しない（途中で表示されてもしない。途中で消えたら回転は止めたまま。）
 */

public class ObjTransRota : MonoBehaviour {

	[SerializeField]
	private SteamVR_Input_Sources leftHand;
	[SerializeField]
	private SteamVR_Input_Sources rightHand;
	[SerializeField]
	private SteamVR_Action_Boolean action_Boolean;

	private GameObject LThumb;
	private GameObject RThumb;
	private GameObject LIndex;
	private GameObject RIndex;

	//ピンチで操作する対象オブジェクト
	private GameObject targetObject;

	//左手がピンチ中
	private bool isLPinch = false;
	//右手がピンチ中
	private bool isRPinch = false;

	//ピンチ時の他方の人差し指オブジェクト
	private GameObject indexBaseObject = null;
	//ピンチ時の他方の人差し指の初期座標
	private Vector3 indexBaseVector;
	//ピンチ時の他方の人差し指を中心に表れるキューブ群
	private GameObject[] controlCubes = new GameObject[6];

	void Start() {
		targetObject = GameObject.Find("central");
	}

	void Update() {
		//processingForViveControlor();
		//processingForViveLeapmotion();
	}
	private void processingForViveControlor() {
		//右手でピンチ中はそもそも考えない
		if (!isRPinch) {
			if (!LThumb.activeInHierarchy) {
				isLPinch = false;
			} else if (action_Boolean.GetState(leftHand)) {
				Debug.Log("Grab!L");
				isLPinch = true;
			} else {
				isLPinch = false;
			}
		}
		//左手でピンチ中はそもそも考えない
		if (!isLPinch) {
			if (!RThumb.activeInHierarchy) {
				isRPinch = false;
			} else if (action_Boolean.GetState(rightHand)) {
				Debug.Log("Grab!R");
				isRPinch = true;
			} else {
				isRPinch = false;
			}
		}

		if (isLPinch) {
			Debug.Log("run");
			//L人差し指位置にシステムを配置
			targetObject.transform.position = LIndex.transform.position + new Vector3(0, 0, variables.pinchDistance);
			targetObject.transform.LookAt(variables.headObject.transform);
			targetObject.transform.Rotate(new Vector3(0f, -180f, 0f));
			variables.isLeftHandLastTouch = true;
			Debug.Log("run2");
		} else if (isRPinch) {
			Debug.Log("run");
			//R人差し指位置にシステムを配置
			targetObject.transform.position = RIndex.transform.position + new Vector3(0, 0, variables.pinchDistance);
			targetObject.transform.LookAt(variables.headObject.transform);
			targetObject.transform.Rotate(new Vector3(0f, -180f, 0f));
			variables.isLeftHandLastTouch = false;
			Debug.Log("run2");
		}
	}

	private void processingForViveLeapmotion() {
		//右手でピンチ中はそもそも考えない
		if (!isRPinch) {
			if (!LThumb.activeInHierarchy) {
				isLPinch = false;
			} else if (Vector3.Distance(LThumb.transform.position, LIndex.transform.position) <= variables.pinchLength) {
				isLPinch = true;
			} else {
				isLPinch = false;
			}
		}
		//左手でピンチ中はそもそも考えない
		if (!isLPinch) {
			if (!RThumb.activeInHierarchy) {
				isRPinch = false;
			} else if (Vector3.Distance(RThumb.transform.position, RIndex.transform.position) <= variables.pinchLength) {
				isRPinch = true;
			} else {
				isRPinch = false;
			}
		}

		if (isLPinch) {
			//L人差し指位置にシステムを配置
			targetObject.transform.position = LIndex.transform.position + new Vector3(0, 0, variables.pinchDistance);

			targetObject.transform.LookAt(variables.headObject.transform);
			targetObject.transform.Rotate(new Vector3(-90f, -180f, 0f));

			variables.isLeftHandLastTouch = true;
		} else if (isRPinch) {
			//R人差し指位置にシステムを配置
			targetObject.transform.position = RIndex.transform.position + new Vector3(0, 0, variables.pinchDistance);

			targetObject.transform.LookAt(variables.headObject.transform);
			targetObject.transform.Rotate(new Vector3(-90f, -180f, 0f));

			variables.isLeftHandLastTouch = false;
		}
	}

	public void SetThumbAndIndex(GameObject LThumbObj, GameObject RThumbObj, GameObject LIndexObj, GameObject RIndexObj) {
		LThumb = LThumbObj;
		RThumb = RThumbObj;
		LIndex = LIndexObj;
		RIndex = RIndexObj;
	}

	private void genereteSixCubes() {
		string[] cubesName = new string[6] { "Up", "Forward", "Right", "Back", "Left", "Down" };
		Vector3[] cubesVector = new Vector3[6] { Vector3.up, Vector3.forward, Vector3.right, Vector3.back, Vector3.left, Vector3.down };
		for (int i = 0; i < 6; i++) {
			controlCubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			controlCubes[i].transform.name = "cube" + cubesName[i];
			controlCubes[i].transform.position = indexBaseVector + cubesVector[i] * 0.1f;
			controlCubes[i].transform.eulerAngles = Vector3.zero;
			controlCubes[i].transform.localScale = Vector3.one * 0.05f;

		}
	}

	private void objectRotation() {
		Vector3 directionVector = indexBaseObject.transform.position - indexBaseVector;
		if (Vector3.Distance(directionVector, Vector3.zero) >= 0.05f) {
			// x < y
			if (Mathf.Abs(directionVector.x) < Mathf.Abs(directionVector.y)) {
				if (Mathf.Abs(directionVector.y) >= Mathf.Abs(directionVector.z)) {
					// x < y > z  y最大
					directionVector = new Vector3(0, directionVector.z > 0 ? 1 : -1, 0);
				}
			}
		}
		targetObject.transform.localEulerAngles += directionVector;
	}
}
