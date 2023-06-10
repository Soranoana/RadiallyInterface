using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTest : MonoBehaviour {
	void Start() {

	}

	void Update() {

	}

	private void OnTriggerEnter(Collider other) {
		Debug.Log("Enter is run");
		DebugUIBuilder.instance.AddLabel("Enter is run");
		if (other.gameObject.tag == "Test") {
			Debug.Log("EnterColor is run");
			DebugUIBuilder.instance.AddLabel("EnterColor is run");
			other.gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	private void OnTriggerExit(Collider other) {
		Debug.Log("Exit is run");
		DebugUIBuilder.instance.AddLabel("Exit is run");
		if (other.gameObject.tag == "Test") {
			Debug.Log("ExitColor is run");
			DebugUIBuilder.instance.AddLabel("ExitColor is run");
			other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
		}
	}
}
