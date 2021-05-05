using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR.OpenVR;
using OVR;
using UnityEngine.XR;
using UnityEngine.VR;
using Valve.VR;

//VRデバイスの名前を確認し、variablesに保存する。

public class DeviceTypeDiscrimination : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		// 表示デバイスがHMDかどうか（ミラーリング含む）
		if (OVRManager.isHmdPresent) {
			// headset connected
		}
		// Debug.Log("名前:" + OVRManager.systemHeadsetType.GetType().FullName);
		// Debug.Log("Quest:" + OVRDeviceSelector.isTargetDeviceQuest);
		// Debug.Log("Quest2:" + OVRDeviceSelector.isTargetDeviceQuest2);
		// Debug.Log("名前2:" + OpenVR.ExtendedDisplay.ToString());
		// Debug.Log("名前2:" + OpenVR.IsHmdPresent());
		// Debug.Log("名前2:" + XRSettings.loadedDeviceName);


		//デバイスがQuestかどうか確認する
		//Quest1か2か
		Debug.Log("名前2:" + ( SystemInfo.deviceName == "Oculus Quest" ));
		Debug.Log("名前2:" + ( SystemInfo.deviceName == "Oculus Quest 2" ));

		// デバイスがQuestじゃないなら        
		//HTC Vive なら “lighthouse”、Oculus Rift なら “oculus”、Cosmosなら"vive_eyes"、VRが刺さってないとNullReferenceException
		Debug.Log("名前2:" + SteamVR.instance.hmd_TrackingSystemName);

	}

	// Update is called once per frame
	void Update() {

	}
}
