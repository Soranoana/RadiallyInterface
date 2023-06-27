using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//参考
//https://developer.oculus.com/documentation/unity/unity-handtracking/?locale=fr_FR
public class OculusHandGetter : MonoBehaviour {
	//課題
	/// <summary>
	/// 課題
	/// ・左目と右目でスフィアの位置が違う。
	/// ・スフィアに色がつかない
	/// </summary>

	//トラッキング関係のスクリプト
	private OVRHand oVRHand;
	private OVRSkeleton oVRSkeleton;

	private bool isCheck = false;

	void Start() {
		//初期化
		oVRHand = GetComponent<OVRHand>();
		oVRSkeleton = GetComponent<OVRSkeleton>();

		//デフォルトの手を非表示にする
		GetComponent<OVRSkeletonRenderer>().enabled = false;
		GetComponent<OVRMeshRenderer>().enabled = false;
		//各ボーンに色や形、位置を設定した子オブジェクトを生成する
		int index = 0;
		if (!isCheck) {
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 4, Color.red);

			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 3, Color.red);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.blue);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 1.5f, Color.blue);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100, Color.blue);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 0.5f, Color.blue);

			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.yellow);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 1.5f, Color.yellow);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100, Color.yellow);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.green);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 1.5f, Color.green);

			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100, Color.green);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.white);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 1.5f, Color.white);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100, Color.white);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.magenta);

			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 1.5f, Color.magenta);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100, Color.magenta);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 0.5f, Color.magenta);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.blue);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.yellow);

			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.green);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.white);
			primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject, Vector3.one / 100 * 2, Color.magenta);
			isCheck = true;
		}
	}

	void Update() {
		//アプリが手を検出しているかどうか
		if (oVRHand.IsTracked) {
			//追跡システムが手のポーズ全体に対して持つ信頼レベルを確認
			// Debug.Log(oVRHand.HandConfidence);  //Low or High ?

			//bone総数取得
			// Debug.Log("Count " + oVRSkeleton.Bones.Count);//24
			// Debug.Log("Max" + OVRSkeleton.BoneId.Hand_MaxSkinnable);
			//座標取得
			// Debug.Log(oVRSkeleton.Bones[0].Transform.position);
			//オブジェクト名取得
			// Debug.Log(oVRSkeleton.Bones[0].Transform.name);


			for (int i = 0; i < oVRSkeleton.Bones.Count; i++) {
				// Debug.Log("Bone[" + i + "]: " + oVRSkeleton.Bones[i].Transform.name);
				// Bone[ 0]:Hand_WristRoot
				// Bone[ 1]:Hand_ForearmStub
				// Bone[ 2]:Hand_Thumb0
				// Bone[ 3]:Hand_Thumb1
				// Bone[ 4]:Hand_Thumb2
				// Bone[ 5]:Hand_Thumb3
				// Bone[ 6]:Hand_Index1
				// Bone[ 7]:Hand_Index2
				// Bone[ 8]:Hand_Index3
				// Bone[ 9]:Hand_Middle1
				// Bone[10]:Hand_Middle2
				// Bone[11]:Hand_Middle3
				// Bone[12]:Hand_Ring1
				// Bone[13]:Hand_Ring2
				// Bone[14]:Hand_Ring3
				// Bone[15]:Hand_Pinky0
				// Bone[16]:Hand_Pinky1
				// Bone[17]:Hand_Pinky2
				// Bone[18]:Hand_Pinky3
				// Bone[19]:Hand_ThumbTip
				// Bone[20]:Hand_IndexTip
				// Bone[21]:Hand_MiddleTip
				// Bone[22]:Hand_RingTip
				// Bone[23]:Hand_PinkyTip
			}
		}
	}

	private void primiteiveGenerator(GameObject parent, Vector3 scale, Color color) {
		GameObject generate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		// Material material = generate.AddComponent(typeof(Material)) as Material;
		generate.GetComponent<Renderer>().material.color = color;
		// material.color = color;
		generate.transform.localScale = scale;
		generate.transform.position = parent.transform.position;
		generate.transform.parent = parent.transform;
		Debug.Log(parent.gameObject.name);
		DebugUIBuilder.instance.AddLabel(parent.gameObject.name);

		if (parent.gameObject.name == "Hand_IndexTip") {
			generate.AddComponent(typeof(HandTest));
			// Debug.Log("added: " + generate.GetComponent<HandTest>());
			generate.AddComponent(typeof(SphereCollider));
			generate.GetComponent<SphereCollider>().isTrigger = true;
			Debug.Log("run");
		}
	}
}

