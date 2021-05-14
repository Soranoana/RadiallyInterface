/*
 * ======================================================================
 * Project Name    : RadiallyInterface
 * File Name       : TypingSystemGeneralization.cs
 * Creation Date   : 2021/05/15
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
using System.IO;
using UnityEngine;

//centralSystemからの入力値を表示していくだけ。
//ベストはcentralSystemに組み込むことだが、大学院時代の実験用のプログラムの名残で別スクリプトになっている。

public class TypingSystemGeneralization : MonoBehaviour {
	public TextMesh InputTextObject;//入力を表示する欄
	public centralSystem centralSystem;

	private string inputText = "";

	void Start() {
		decideTaskIndexQueue();
	}

	void Update() {
		displayInputText();
	}

	//キーイベントを得て保存する
	public void listenKeyEvent(string data) {
		inputText = data;
	}

	//タスクのキューを作成
	void decideTaskIndexQueue() {
		//seed値の初期化
		if (0 <= variables.seedExperiment) {
			//Random.seed = variables.seedExperiment;
			Random.InitState(variables.seedExperiment);
		}
	}


	//インプット情報を保存する→タスクテキストの縁ありで表示
	void displayInputText() {
		InputTextObject.text = inputText;
	}

	//マテリアル情報をカラーコード情報に変換
	private string TransMaterialToColorCode(Material material) {
		string colorCode = "<color=#";
		//カラーを取り出したうえで、16進数に変換、格納
		colorCode += ( (int)( material.color.r * 255 ) ).ToString("x2");
		colorCode += ( (int)( material.color.g * 255 ) ).ToString("x2");
		colorCode += ( (int)( material.color.b * 255 ) ).ToString("x2");
		colorCode += ">";
		//Debug.Log(colorCode);
		return colorCode;
	}
}
