/*
 * ======================================================================
 * Project Name    : RadiallyInterface
 * File Name       : createPointer.cs
 * Creation Date   : 2021/04/30
 *  
 * Copyright © 2021 Soranoana(ソラノアナ). All rights reserved.
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

/*
 * それぞれの指にポインタ―を付与する
 * LeapMotionの描画など
 */

public class CreatePointerManager : MonoBehaviour {

	//球体を描画する
	[SerializeField]
	private bool doRenderSphere = true;
	[SerializeField]
	private bool forceRenderColliderSphere = true;
	//手を描画する
	[SerializeField]
	private bool doRenderHands = true;

	//2cm角の球体
	[SerializeField]
	private float pointerScale = 0.02f;
	//当たり判定
	[SerializeField]
	private float pointerColRad = 0.05f;

	void Start() {
		//VIVE + VIVEコントローラ
		InitializeViveControlor();
		//VIVE + LeapMotion
		//InitializeViveLeapmotion();

	}

	private void InitializeViveLeapmotion() {
		//腕より上の関節(hierarchy)の名前
		string[] handJointsName = new string[7] {   "Leap Rig", "Hand Models", "LoPoly Rigged Hand Left", "LoPoly Rigged Hand Right",
													"LoPoly_Hand_Mesh_Left", "LoPoly_Hand_Mesh_Right", "Wrist" };
		//左右
		string[] LandR = new string[2] { "L_", "R_" };
		//腕と各指の関節名
		string[] fingerName = new string[6] { "Palm", "thumb", "index", "middle", "ring", "pinky" };
		//指先の関節名
		string[] fingerJoint = new string[5] { "_meta", "_a", "_b", "_c", "_end" };
		GameObject[,,] fingers = new GameObject[LandR.Length, fingerName.Length, fingerJoint.Length];
		//当たり判定のテーブル
		//                                            meta   a      b      c      end   //Left
		bool[,,] colliderTable = new bool[2, 6, 5]{{{ false, false, false, false, false },  //Palm      手のひら
													{ false, false, false, false, false },  //thumb     親指
													{ false, false, false, false, true  },  //index     人差し指
													{ false, false, false, false, false },  //middle    中指
													{ false, false, false, false, false },  //ring      薬指
													{ false, false, false, false, false },  //pinky     小指
													 },{                                //Right
													{ false, false, false, false, false },  //Palm      手のひら
													{ false, false, false, false, false },  //thumb     親指
													{ false, false, false, false, true  },  //index     人差し指
													{ false, false, false, false, false },  //middle    中指
													{ false, false, false, false, false },  //ring      薬指
													{ false, false, false, false, false },  //pinky     小指
													 } };

		for (int LR = 0; LR < LandR.Length; LR++) {
			GameObject hand;
			//リグを取得
			hand = GameObject.Find(handJointsName[0]);
			//リグの下の手を取得
			hand = hand.transform.Find(handJointsName[1]).gameObject;
			hand = hand.transform.Find(handJointsName[2 + LR]).gameObject;
			//手の描画の可否
			hand.transform.Find(handJointsName[4 + LR]).GetComponent<SkinnedMeshRenderer>().enabled = doRenderHands;
			//ローポリハンドのの下の腕を取得
			hand = hand.transform.Find(LandR[LR] + handJointsName[6]).gameObject;
			//腕の下の手のひらを取得
			hand = hand.transform.Find(LandR[LR] + fingerName[0]).gameObject;

			//行列に保存
			fingers[LR, 0, 0] = hand;

			for (int Name = 1; Name < fingerName.Length; Name++) {
				GameObject fingerParent = fingers[LR, 0, 0];
				for (int Joint = 0; Joint < fingerJoint.Length; Joint++) {
					//L_thumb_cとR_thumb_cは存在しないので飛ばす
					if (Name == 5 && Joint == 3) {
						//なにもしない
					} else {
						//目的の部分を見つける
						GameObject target = fingerParent.transform.Find(LandR[LR] + fingerName[Name] + fingerJoint[Joint]).gameObject;

						GameObject sphere = putSphere(target);

						//指先に当たり判定スクリプトをアタッチ
						//人差指先にアタッチ
						//当たり判定
						sphere.GetComponent<SphereCollider>().enabled = colliderTable[LR, Name, Joint];
						if (colliderTable[LR, Name, Joint] == true) {
							sphere.GetComponent<SphereCollider>().radius = pointerColRad;
							sphere.GetComponent<SphereCollider>().isTrigger = true;
							sphere.AddComponent<fingerFeelCollider>();
						}
						//表示の可否
						sphere.GetComponent<MeshRenderer>().enabled = doRenderSphere || ( forceRenderColliderSphere && colliderTable[LR, Name, Joint] );

						//配列に保存
						fingers[LR, Name, Joint] = target;
					}
				}
			}
		}

		//ピンチ用スクリプトの初期化
		GetComponent<ObjTransRota>().SetThumbAndIndex(fingers[0, 1, 4],
													  fingers[1, 1, 4],
													  fingers[0, 2, 4],
													  fingers[1, 2, 4]);
		/* 左手親指先
		 * 右手親指先
		 * 左手人差指先
		 * 右手人差指先
		 */
		variables.fingers[0] = fingers[0, 2, 4];
		variables.fingers[1] = fingers[1, 2, 4];
	}

	private void InitializeViveControlor() {
		//腕より上の関節(hierarchy)の名前
		string[] handJointsName = new string[6] { "[CameraRig]", "vr_glove_left_model_slim", "vr_glove_right_model_slim", "slim_l", "slim_r", "Root" };
		//腕と各指の関節名
		string[] fingerJointsName = new string[6] { "wrist_r", "finger_thumb", "finger_index", "finger_middle", "finger_ring", "finger_pinky" };
		//指先の関節名
		string[] level = new string[5] { "_meta_r", "_0_r", "_1_r", "_2_r", "_r_end" };
		GameObject[,,] fingers = new GameObject[2, fingerJointsName.Length, level.Length];
		//当たり判定のテーブル
		//                                        _meta_r _0_r   _1_r   _2_r   _r_end
		bool[,] colliderTable = new bool[6, 5]{ { false,  false, false, false, false }, //wrist_r
												{ false,  false, false, false, false }, //finger_thumb
												{ false,  false, false, false, true  }, //finger_index
												{ false,  false, false, false, false }, //finger_middle
												{ false,  false, false, false, false }, //finger_ring
												{ false,  false, false, false, false }, //finger_pinky
		};

		//[CameraRig]オブジェクトを検索・取得
		GameObject cameraRig = GameObject.Find(handJointsName[0]);

		//目的地に向けての目印オブジェクトを作成
		//腕位置
		GameObject objBookmark_wrist_r = null;
		//腕より下の作業中の位置
		GameObject objBookmarkTip = null;

		//両手それぞれ実施
		for (int i = 0; i < 2; i++) {
			//目印オブジェクトの初期化
			//名前が変わるところをiで変更している。
			objBookmark_wrist_r = cameraRig.transform.Find(handJointsName[1 + i]).gameObject;
			objBookmark_wrist_r = objBookmark_wrist_r.transform.Find(handJointsName[3 + i]).gameObject;
			//初期化ついでに手のモデルの表示可否設定
			objBookmark_wrist_r.transform.Find("vr_glove_right_slim").GetComponent<SkinnedMeshRenderer>().enabled = doRenderHands;
			//初期化の続き
			objBookmark_wrist_r = objBookmark_wrist_r.transform.Find(handJointsName[5]).gameObject;

			//ターゲットオブジェクトの初期化
			GameObject target = null;
			for (int j = 0; j < fingerJointsName.Length; j++) {
				//作業位置を腕の位置にする
				objBookmarkTip = objBookmark_wrist_r;
				for (int k = 0; k < level.Length; k++) {
					GameObject sphere = null;
					if (j == 0) {
						//腕は一か所のみ配置して終わり
						if (k == 0) {
							target = objBookmarkTip.transform.Find(fingerJointsName[j]).gameObject;
							putSphere(target);
							objBookmarkTip = objBookmark_wrist_r = target;
						}
					} else if (j == 1) {
						//親指には_meta_rがいないので飛ばす
						if (k != 0) {
							// Debug.Log(objBookmarkTip.transform.Find(fingerJointsName[j] + level[k]).gameObject.name);
							target = objBookmarkTip.transform.Find(fingerJointsName[j] + level[k]).gameObject;
							sphere = putSphere(target);
							objBookmarkTip = target;
							//指先に当たり判定スクリプトをアタッチ
							//人差指先にアタッチ
							//当たり判定
							sphere.GetComponent<SphereCollider>().enabled = colliderTable[j, k];
							if (colliderTable[j, k] == true) {
								sphere.GetComponent<SphereCollider>().radius = pointerColRad;
								sphere.GetComponent<SphereCollider>().isTrigger = true;
								sphere.AddComponent<fingerFeelCollider>();
							}
							//表示の可否
							sphere.GetComponent<MeshRenderer>().enabled = doRenderSphere || ( forceRenderColliderSphere && colliderTable[j, k] );

							//配列に保存
							fingers[i, j, k] = sphere;
						}
					} else {
						target = objBookmarkTip.transform.Find(fingerJointsName[j] + level[k]).gameObject;
						sphere = putSphere(target);
						objBookmarkTip = target;
						//指先に当たり判定スクリプトをアタッチ
						//人差指先にアタッチ
						//当たり判定
						sphere.GetComponent<SphereCollider>().enabled = colliderTable[j, k];
						if (colliderTable[j, k] == true) {
							sphere.GetComponent<SphereCollider>().radius = pointerColRad;
							sphere.GetComponent<SphereCollider>().isTrigger = true;
							sphere.AddComponent<fingerFeelCollider>();
						}
						//表示の可否
						sphere.GetComponent<MeshRenderer>().enabled = doRenderSphere || ( forceRenderColliderSphere && colliderTable[j, k] );

						//配列に保存
						fingers[i, j, k] = sphere;
					}
				}
			}
		}

		//ピンチ用スクリプトの初期化
		GetComponent<ObjTransRota>().SetThumbAndIndex(fingers[0, 1, 4],
													  fingers[1, 1, 4],
													  fingers[0, 2, 4],
													  fingers[1, 2, 4]);
		/* 左手親指先
		 * 右手親指先
		 * 左手人差指先
		 * 右手人差指先
		 */
		variables.fingers[0] = fingers[0, 2, 4];
		variables.fingers[1] = fingers[1, 2, 4];
	}

	private GameObject putSphere(GameObject target) {
		//マーカー用球体を作る
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		//大きさ
		sphere.transform.localScale = new Vector3(pointerScale, pointerScale, pointerScale);
		//場所は目的部分のところ
		sphere.transform.position = target.transform.position;
		//部分を親に
		sphere.transform.parent = target.transform;
		//名前は親にちなむ
		sphere.name = target.name + "_Pointer";
		//当たり判定用に
		Rigidbody rigidbody = sphere.AddComponent<Rigidbody>();
		//重力は消す
		rigidbody.useGravity = false;
		//回転や移動もなし
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		//今の目的部分を次の親にする
		sphere.transform.parent = target.transform;

		return sphere;
	}
}