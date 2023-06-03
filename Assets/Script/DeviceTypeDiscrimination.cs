/*
 * ======================================================================
 * Project Name    : RadiallyInterface
 * File Name       : DeviceTypeDiscrimination.cs
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR.OpenVR;
using OVR;
using UnityEngine.XR;
// using UnityEngine.VR;
// using Valve.VR;

//VRデバイスの名前を確認し、variablesに保存する。

public class DeviceTypeDiscrimination : MonoBehaviour {

	//動作させるVRデバイス
	enum RunMode {
		Auto,
		Oculus_Quest,
		Oculus_Quest_2,
		VIVE_CE,
		VIVE_Cosmos,
		Oculus_rift,
		Other_headset,
		Non_VR,
	}

	//動作させるコントローラ
	enum InputDevice {
		Auto,
		Controller,
		Hand_tracking,
		Leapmotion,
		Mouse,
	}

	[SerializeField]
	private RunMode runMode;

	[SerializeField]
	private InputDevice inputDevice;


	private void Awake() {
		InitializeHeadObject();
	}
	void Start() {

		bool isLeapmotionConnected;
		//LeapMotionが刺さっているか
		if (false) {
			//LeapMotionが刺さっている
			isLeapmotionConnected = true;
			Debug.Log("Leapmotion connected");
		} else {
			//LeapMotionが刺さっていない
			isLeapmotionConnected = false;
			Debug.Log("Leapmotion NOT connected");
		}

		// 表示デバイスがHMDかどうか（ミラーリング含む）
		if (OVRManager.isHmdPresent) {
			// headset connected
			Debug.Log("HMD is present");
		}

		//デバイスがQuest1か2かPCか
		if (SystemInfo.deviceName == "Oculus Quest") {
			Debug.Log("Device name is Oculus Quest");
			//Oculus Quest
			if (IsControllerActive()) {
				variables.platformType = variables.platform_type._OculusQuest_controller;
				Debug.Log("Use controller");
			} else {
				variables.platformType = variables.platform_type._OculusQuest_HandTracking;
				Debug.Log("Use HandTracking");
			}
		} else if (SystemInfo.deviceName == "Oculus Quest 2") {
			Debug.Log("Device name is Oculus Quest 2");
			//Oculus Quest 2
			if (IsControllerActive()) {
				variables.platformType = variables.platform_type._OculusQuest_controller;
				Debug.Log("Use controller");
			} else {
				variables.platformType = variables.platform_type._OculusQuest_HandTracking;
				Debug.Log("Use HandTracking");
			}
		} else {
			//PC
			try {
				// if (SteamVR.instance.hmd_TrackingSystemName == "lighthouse") {
				// 	Debug.Log("HMD name is lighthouse(VIVE CE)");
				// 	//HTC Vive
				// 	if (isLeapmotionConnected) {
				// 		variables.platformType = variables.platform_type._VIVE_LeapMotion;
				// 		Debug.Log("Use LeapMotion");
				// 	} else if (IsControllerActive()) {
				// 		variables.platformType = variables.platform_type._VIVE_Controller;
				// 		Debug.Log("Use controller");
				// 	} else {
				// 		variables.platformType = variables.platform_type._VIVE_HandTracking;
				// 		Debug.Log("Use HandTracking");
				// 	}
				// } else if (SteamVR.instance.hmd_TrackingSystemName == "oculus") {
				// 	Debug.Log("HMD name is Oculus(Oculus rift)");
				// 	//Oculus Rift
				// 	if (isLeapmotionConnected) {
				// 		variables.platformType = variables.platform_type._OculusRift_LeapMotion;
				// 		Debug.Log("Use LeapMotion");
				// 	} else if (IsControllerActive()) {
				// 		variables.platformType = variables.platform_type._OculusRift_Controller;
				// 		Debug.Log("Use controller");
				// 	} else {
				// 		variables.platformType = variables.platform_type._OculusRift_HandTracking;
				// 		Debug.Log("Use HandTracking");
				// 	}
				// } else if (SteamVR.instance.hmd_TrackingSystemName == "vive_eyes") {
				// 	Debug.Log("HMD name is vive_eyes(VIVE Cosmos)");
				// 	//VIVE Cosmos
				// 	if (isLeapmotionConnected) {
				// 		variables.platformType = variables.platform_type._Cosmos_LeapMotion;
				// 		Debug.Log("Use LeapMotion");
				// 	} else if (IsControllerActive()) {
				// 		variables.platformType = variables.platform_type._Cosmos_Controller;
				// 		Debug.Log("Use controller");
				// 	} else {
				// 		variables.platformType = variables.platform_type._Cosmos_HandTracking;
				// 		Debug.Log("Use HandTracking");
				// 	}
				// } else {
				//other devices
				variables.platformType = variables.platform_type._OtherHMD;
				Debug.Log("HMD is Other device");
				// }

			} catch (System.Exception) {
				//VRデバイスが刺さってない
				if (isLeapmotionConnected) {
					variables.platformType = variables.platform_type._NonVR_LeapMotion;
					Debug.Log("Use LeapMotion");
				} else {
					variables.platformType = variables.platform_type._NonVR_Mouse;
					Debug.Log("Use Mouse");
				}
				throw;
			}
		}
	}

	void Update() {
		//途中でコントローラ-ハンドトラッキング間で切り替え可能にする。
	}

	//コントローラobjectがアクティブかどうかをboolで返す。
	private bool IsControllerActive() {
		if (SystemInfo.deviceName == "Oculus Quest") {

		} else if (SystemInfo.deviceName == "Oculus Quest 2") {

		}
		// else if (SteamVR.instance.hmd_TrackingSystemName == "lighthouse") {
		// 	if (GameObject.Find("[CameraRig]").transform.Find("vr_glove_left_model_slim").gameObject.activeInHierarchy ||
		// 		GameObject.Find("[CameraRig]").transform.Find("vr_glove_right_model_slim").gameObject.activeInHierarchy) {
		// 		return true;
		// 	}
		// 	return false;
		// } else if (SteamVR.instance.hmd_TrackingSystemName == "oculus") {

		// } else if (SteamVR.instance.hmd_TrackingSystemName == "vive_eyes") {
		// 	if (GameObject.Find("[CameraRig]").transform.Find("vr_glove_left_model_slim").gameObject.activeInHierarchy ||
		// 			GameObject.Find("[CameraRig]").transform.Find("vr_glove_right_model_slim").gameObject.activeInHierarchy) {
		// 		return true;
		// 	}
		// 	return false;
		// }
		return false;
	}

	//variablesスクリプトのheadObjectを初期化する。
	private void InitializeHeadObject() {
		//デバイス判定
		if (runMode == RunMode.Auto) {
			if (XRSettings.loadedDeviceName == "oculus display") {
				OVRPlugin.SystemHeadset headset = OVRPlugin.GetSystemHeadsetType();
				if (headset == OVRPlugin.SystemHeadset.Oculus_Link_Quest) {
					runMode = RunMode.Oculus_Quest;
					// Debug.Log("Oculus_Link_Quest");
				} else if (headset == OVRPlugin.SystemHeadset.Oculus_Link_Quest_2) {
					runMode = RunMode.Oculus_Quest_2;
					// Debug.Log("Oculus_Link_Quest_2");
				} else if (headset == OVRPlugin.SystemHeadset.Oculus_Quest) {
					runMode = RunMode.Oculus_Quest;
					// Debug.Log("Oculus_Quest");
				} else if (headset == OVRPlugin.SystemHeadset.Oculus_Quest_2) {
					runMode = RunMode.Oculus_Quest_2;
					// Debug.Log("Oculus_Quest_2");
				} else if (headset == OVRPlugin.SystemHeadset.Rift_CB ||
							headset == OVRPlugin.SystemHeadset.Rift_CV1 ||
							headset == OVRPlugin.SystemHeadset.Rift_DK1 ||
							headset == OVRPlugin.SystemHeadset.Rift_DK2 ||
							headset == OVRPlugin.SystemHeadset.Rift_S) {
					runMode = RunMode.Oculus_rift;
					// Debug.Log("Oculus_rift");
				} else {
					runMode = RunMode.Other_headset;
				}
			} else {
				// if (SteamVR.instance.hmd_TrackingSystemName == "lighthouse") {
				// 	runMode = RunMode.VIVE_CE;
				// } else if (SteamVR.instance.hmd_TrackingSystemName == "vive_eyes") {
				// 	runMode = RunMode.VIVE_Cosmos;
				// } else {
				runMode = RunMode.Non_VR;
				// }
			}
		}
		//ヘッドセット初期化
		if (runMode == RunMode.Oculus_Quest) {
			variables.headObject = GameObject.Find("OVRCameraRig").transform.Find("TrackingSpace").transform.Find("CenterEyeAnchor").gameObject;
		} else if (runMode == RunMode.Oculus_Quest_2) {
			variables.headObject = GameObject.Find("OVRCameraRig").transform.Find("TrackingSpace").transform.Find("CenterEyeAnchor").gameObject;
		} else if (runMode == RunMode.VIVE_CE) {
			variables.headObject = GameObject.Find("[CameraRig]").transform.Find("Camera").gameObject;
		} else if (runMode == RunMode.Oculus_rift) {
			variables.headObject = GameObject.Find("OVRCameraRig").transform.Find("TrackingSpace").transform.Find("CenterEyeAnchor").gameObject;
		} else if (runMode == RunMode.VIVE_Cosmos) {
			variables.headObject = GameObject.Find("[CameraRig]").transform.Find("Camera").gameObject;
		} else {
			variables.headObject = GameObject.Find("[CameraRig]").transform.Find("Camera").gameObject;
		}
	}

	private void InitializeInputDevice() {

		//Leapmotion対応はまだ先
		// bool connectedLeapmtion;

		if (inputDevice == InputDevice.Auto) {
			if (runMode == RunMode.Non_VR) {
				inputDevice = InputDevice.Mouse;
			} else if (runMode == RunMode.Oculus_Quest) {

			} else if (runMode == RunMode.Oculus_Quest_2) {

			} else if (runMode == RunMode.VIVE_CE) {

			} else if (runMode == RunMode.Oculus_rift) {

			} else if (runMode == RunMode.VIVE_Cosmos) {

			} else {

			}
		}
	}
}
