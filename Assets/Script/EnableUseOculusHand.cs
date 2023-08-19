using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUseOculusHand : MonoBehaviour
{
	//課題
	/// <summary>
	/// 課題
	/// ・左目と右目でスフィアの位置が違う。
	/// </summary>

	//トラッキング関係のスクリプト
	private OVRHand oVRHand;
	private OVRSkeleton oVRSkeleton;

	private bool isCheck = false;

	// 定数
	private readonly Vector3 scale = Vector3.one / 100;    // 半径1cmくらいの球に設定
	private readonly Color color = Color.white;    // 白に設定

	void Start()
	{
		//初期化
		oVRHand = GetComponent<OVRHand>();
		oVRSkeleton = GetComponent<OVRSkeleton>();

	}

	void Update()
	{

		if (oVRHand.IsTracked && !isCheck)
		{
			InitializeHandJointObject();
		}
		
		//アプリが手を検出しているかどうか
		if (oVRHand.IsTracked)
		{
			//追跡システムが手のポーズ全体に対して持つ信頼レベルを確認
			// Debug.Log(oVRHand.HandConfidence);  //Low or High ?

			//bone総数取得
			// Debug.Log("Count " + oVRSkeleton.Bones.Count);//24
			// Debug.Log("Max" + OVRSkeleton.BoneId.Hand_MaxSkinnable);
			//座標取得
			// Debug.Log(oVRSkeleton.Bones[0].Transform.position);
			//オブジェクト名取得
			// Debug.Log(oVRSkeleton.Bones[0].Transform.name);


			for (int i = 0; i < oVRSkeleton.Bones.Count; i++)
			{
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

	private void primiteiveGenerator(GameObject parent)
	{
		GameObject generate = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		generate.GetComponent<Renderer>().material.color = color;
		generate.GetComponent<MeshRenderer>().enabled = false;
		generate.transform.localScale = scale;
		generate.transform.position = parent.transform.position;
		generate.transform.parent = parent.transform;
		Debug.Log(parent.gameObject.name);
		// DebugUIBuilder.instance.AddLabel(parent.gameObject.name);

		if (parent.gameObject.name == "Hand_IndexTip")
		{
			generate.AddComponent(typeof(HandTest));
			// Debug.Log("added: " + generate.GetComponent<HandTest>());
			generate.AddComponent(typeof(SphereCollider));
			generate.GetComponent<SphereCollider>().isTrigger = true;
		}
	}
	private void InitializeHandJointObject()
	{
		//デフォルトの手を非表示にする
		GetComponent<OVRSkeletonRenderer>().enabled = false;
		GetComponent<OVRMeshRenderer>().enabled = false;
		//各ボーンに色や形、位置を設定した子オブジェクトを生成する
		int index = 0;
		// 手首
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 0]:Hand_WristRoot
																				// 手首〜親指
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 1]:Hand_ForearmStub
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 2]:Hand_Thumb0
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 3]:Hand_Thumb1
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 4]:Hand_Thumb2
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 5]:Hand_Thumb3
																				// 人差し指 
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 6]:Hand_Index1
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 7]:Hand_Index2
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 8]:Hand_Index3
																				// 中指
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[ 9]:Hand_Middle1
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[10]:Hand_Middle2
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[11]:Hand_Middle3
																				// 薬指
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[12]:Hand_Ring1
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[13]:Hand_Ring2
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[14]:Hand_Ring3
																				// 小指
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[15]:Hand_Pinky0
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[16]:Hand_Pinky1
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[17]:Hand_Pinky2
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[18]:Hand_Pinky3
																				// 親指の爪
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[19]:Hand_ThumbTip
																				// 人差し指の爪
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[20]:Hand_IndexTip
																				// 中指の爪
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[21]:Hand_MiddleTip
																				// 薬指の爪
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[22]:Hand_RingTip
																				// 小指の爪
		primiteiveGenerator(oVRSkeleton.Bones[index++].Transform.gameObject);   // Bone[23]:Hand_PinkyTip
		isCheck = true;

	}
}

