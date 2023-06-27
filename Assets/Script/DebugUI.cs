using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {
	// bool inMenu;
	// private string buttonText = "Clear Log";

	void Start() {
		DebugUIBuilder.instance.AddLabel("Debug Start", DebugUIBuilder.DEBUG_PANE_CENTER);
		// DebugUIBuilder.instance.AddLabel("Debug Log", DebugUIBuilder.DEBUG_PANE_LEFT);
		DebugUIBuilder.instance.Show();
		// inMenu = true;
	}

	void Update() {
		// Aボタンでデバッグディスプレイの表示・非表示
		// if (OVRInput.GetDown(OVRInput.Button.One)) {
		// 	if (inMenu)
		// 		DebugUIBuilder.instance.Hide();
		// 	else
		// 		DebugUIBuilder.instance.Show();
		// 	inMenu = !inMenu;
		// }

		// 左のオプションボタンでデバッグログをクリア
		if (OVRInput.GetDown(OVRInput.Button.Start)) {
			DebugUIBuilder.instance.AddLabel("Clear");
			DebugUIBuilder.instance.AddDivider();
		}
	}
}