﻿/*
 * ======================================================================
 * Project Name    : RadiallyInterface
 * File Name       : variables.cs
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
using UnityEngine.SceneManagement;

public class variables : MonoBehaviour {

	//複数プラットフォーム対応用
	public enum platform_type {
		_NonVR_Mouse,
		_NonVR_LeapMotion,
		_VIVE_Controller,
		_VIVE_LeapMotion,
		_Cosmos_Controller,
		_Cosmos_LeapMotion,
		_OculusRift_Controller,
		_OculusRift_LeapMotion,
		_OculusQuest_controller,
		_OculusQuest_HandTracking,
		_OtherHMD,
	}

	//複数シーンの一括管理
	public enum scene_type {
		_Radially,
		_RadiallyExperiment,
		_Circle,
		_CircleExperiment,
		_CircleExperiment2,
		_50KanaInterface,
	}

	//実行するプラットフォームの選択
	public static platform_type platformType { get; set; }

	//実行するシーンの選択
	public static scene_type sceneType { get; set; }

	//多角形部分の半径・円環の内側の半径
	public static float radiusIn { get; set; }

	//円環の外側の半径
	public static float radiusOut { get; set; }

	//多角形部分の半径・円環の内側の半径(Circle用副輪)
	public static float radiusIn_subCircle { get; set; }

	//円環の外側の半径(Circle用副輪)
	public static float radiusOut_subCircle { get; set; }

	//システムの厚さ
	public static float poleHeight { get; set; }

	//システムのキーの数（中心とシステムキー除く）
	public static int poleSum { get; set; }

	//多角形作成用頂点群
	public static Vector3[][] polygonalPillarVertex { get; set; }

	//多角形のマテリアル
	public static Material material_PolygonalPillar { get; set; }

	//台形用通常マテリアル
	public static Material material_TrapezoidPole_Normal { get; set; }

	//台形用接触時マテリアル
	public static Material material_TrapezoidPole_Touch { get; set; }

	//台形用通常マテリアル(Circleでの非アクティブ時)
	public static Material material_TrapezoidPole_Normal_Nonactive { get; set; }

	//台形用接触時マテリアル(Circleでの非アクティブ時)
	public static Material material_TrapezoidPole_Touch_Nonactive { get; set; }

	//台形の強調ライン用マテリアル
	public static Material material_LineRenderer { get; set; }

	//台形の強調ライン用マテリアル(Circleでの非アクティブ時)
	public static Material material_LineRenderer_Nonactive { get; set; }

	//文字の色(Circleでの通常色)
	public static Material material_Text { get; set; }

	//文字の色(Circleでの非アクティブ時)
	public static Material material_Text_Nonactive { get; set; }

	//タイピングソフトのクリアー部分の文字の色
	public static Material material_Typing_Clear { get; set; }

	//タイピングソフトのエラー部分の文字の色
	public static Material material_Typing_Error { get; set; }

	//タイピングソフトのその他部分の文字の色
	public static Material material_Typing_Other { get; set; }

	//台形部分の分割数
	public static int trapezoidDivisionNum { get; set; }

	//システムアイコンの数
	public static int systemCommandNum { get; set; }

	//そのシステムアイコンを表示するか
	public static bool[] displaySystemCommand { get; set; }

	//システムアイコンの名前
	public static string[] systemCommandName { get; set; }

	//そのシステムアイコンを使えるようにするか
	public static bool[] useSystemCommand { get; set; }

	//システムアイコンの配置される円の半径
	public static float systemCommandRadius { get; set; }

	//文字の色
	public static Material material_SystemText { get; set; }

	//文字の大きさ
	public static int systemTextFontSize { get; set; }

	//VRで使用しているか
	public static bool isOnXR { get; set; }

	//centralSystemのgameObjectの座標
	public static Vector3 createSourcePosition { get; set; }

	//キーの縁取りの太さ
	public static float lineRendererWidth { get; set; }

	//キーの縁取りの本体からのずらし加減
	public static float lineShiftSlightly { get; set; }

	//指のオブジェクト 左手人差指、右手人差指の順
	public static GameObject[] fingers { get; set; } = new GameObject[2];

	//頭のオブジェクト
	public static GameObject headObject { get; set; }

	//ピンチ動作時に奥行き方向にどれだけ指とシステムとを離すか
	public static float pinchDistance { get; set; }

	//ピンチ判定を行う指間の距離
	public static float pinchLength { get; set; }

	//システムがCircleの方であるか
	public static bool isCircleSystem { get; private set; }

	//システムの状態
	public static int stage { get; set; }
	/* ring1 → 主に真ん中にいるリング
     * ring2 → 途中で出現する2個目のリング
     *
     * stage: 0（そのままorポインタ帰還）
     * ring1 濃く　入力可能
     * ring2 存在せず
     * 
     * stage: 1（ring1に接触～ring2に接触中）
     * ring1 薄く　入力停止　ring2呼び出す
     * ring2 濃く　入力可能
     * 
     * stage: 2（ring2の外か中に移動～中心部に移動中）
     * ring1 （更に）薄く　入力停止
     * ring2 薄く　入力不可
     * 
     * stage: 0（ポインタ帰還）
     * ring1 濃く　入力可能
     * ring2 消す
     */

	//ログ出力用インスタンス
	public static logSave logInstance { get; set; }

	//ログ出力ディレクトリ
	public static string logDirectory { get; set; }

	//50音システムであるか？
	public static bool is50Kana { get; set; }

	//50音システムにおけるキーの横方向間隔
	public static float cubesIntervalX { get; set; }

	//50音システムにおけるキーの縦方向間隔
	public static float cubesIntervalY { get; set; }

	//50音システムのキーの横の長さ
	public static float cubeWidth { get; set; }

	//50音システムのキーの縦の長さ
	public static float cubeVertical { get; set; }

	//50音システムのキーの斜め抜け可能角度
	public static float cubeAngle { get; set; }

	//二手択一用
	public static bool isLeftHandLastTouch { get; set; }

	//実験用生成seed値
	public static int seedExperiment { get; set; }

	//実験用何回目か
	public static int numExperiment { get; set; }

	//実験用一回当たり何文字実行？
	public static int taskSetNumExperiment { get; set; }

	/* Inspector用 */
	[SerializeField, Header("実行するプラットフォームの選択")]
	private platform_type PlatformType;

	[SerializeField, Header("実行するシーンの選択")]
	private scene_type SceneType;

	[SerializeField, Header("円環の内径半径(単位cm)")]
	private float RadiusIn;

	[SerializeField, Header("円環の外径半径(単位cm)")]
	private float RadiusOut;

	[SerializeField, Header("円環の内径半径(単位cm)(Circle用副輪)")]
	private float RadiusIn_subCircle;

	[SerializeField, Header("円環の外径半径(単位cm)(Circle用副輪)")]
	private float RadiusOut_subCircle;

	[SerializeField, Header("円環の厚さ(単位cm)")]
	private float PoleHeight;

	[SerializeField, Header("円環の縁取りの太さ(単位cm)")]
	private float LineRendererWidth;

	[SerializeField, Header("円環の縁取りのずらし量(単位cm)")]
	private float LineShiftSlightly;

	[SerializeField, Header("円環内部の多角形のマテリアル")]
	private Material Material_PolygonalPillar;

	[SerializeField, Header("キーの常態のマテリアル")]
	private Material Material_TrapezoidPole_Normal;

	[SerializeField, Header("キーの接触時のマテリアル")]
	private Material Material_TrapezoidPole_Touch;

	[SerializeField, Header("キーの常態のマテリアル(Circleでの非アクティブ時)")]
	private Material Material_TrapezoidPole_Normal_Nonactive;

	[SerializeField, Header("キーの接触時のマテリアル(Circleでの非アクティブ時)")]
	private Material Material_TrapezoidPole_Touch_Nonactive;

	[SerializeField, Header("キーの輪郭線のマテリアル")]
	private Material Material_LineRenderer;

	[SerializeField, Header("キーの輪郭線のマテリアル(Circleでの非アクティブ時)")]
	private Material Material_LineRenderer_Nonactive;

	[SerializeField, Header("円環外側のシステムキーのマテリアル")]
	private Material Material_SystemText;

	[SerializeField, Header("テキストの色(Circle用)")]
	private Material Material_Text;

	[SerializeField, Header("テキストの色(Circle用非アクティブ時)")]
	private Material Material_Text_Nonactive;

	[SerializeField, Header("タイピングソフトのクリアー部分の文字の色")]
	private Material Material_Typing_Clear;

	[SerializeField, Header("タイピングソフトのエラー部分の文字の色")]
	private Material Material_Typing_Error;

	[SerializeField, Header("タイピングソフトのその他部分の文字の色")]
	private Material Material_Typing_Other;

	[SerializeField, Header("円環外側のシステムキーの半径(単位cm)")]
	private float SystemCommandRadius;

	[SerializeField, Header("ピンチ時のシステムと指の距離(単位cm)")]
	private float PinchDistance;

	[SerializeField, Header("ピンチ判定の相対座標の閾値(単位cm)")]
	private float PinchLength;

	[SerializeField, Header("テキストのフォントサイズ( X cm角。小数点第二位まで有効)"), Tooltip("システム上に表示されるテキストのフォントサイズ")]
	private float SystemTextFontSize;

	[SerializeField, Header("キーの分割数"), Tooltip("キーを表示するための台形のポリゴンを任意の回数分割する"), Range(0, 29)]//MeshColliderのConvexが三角形ポリゴン255枚以下の必要があるため
	private int TrapezoidDivisionNum;

	[SerializeField, Header("ログ出力用インスタンス")]
	private logSave LogInstance;

	[SerializeField, Header("ログ出力ディレクトリ"), Tooltip("絶対パスにて入力。最後に￥は不要。")]
	private string LogDirectory;

	[SerializeField, Header("50音システムにおけるキーの横方向間隔(単位cm)")]
	private float CubesIntervalX;

	[SerializeField, Header("50音システムにおけるキーの縦方向間隔(単位cm)")]
	private float CubesIntervalY;

	[SerializeField, Header("50音システムのキーの横の長さ(単位cm)")]
	private float CubeWidth;

	[SerializeField, Header("50音システムのキーの縦の長さ(単位cm)")]
	private float CubeVertical;

	[SerializeField, Header("50音システムのキーの斜め抜け可能角度(単位°)")]
	private float CubeAngle;


	[SerializeField, Header("実験用生成seed値"), Tooltip("0未満で完全ランダム")]
	private int SeedExperiment;

	[SerializeField, Header("実験が何回目か"), Tooltip("0以下ですべて実行")]
	private int NumExperiment;

	[SerializeField, Header("実験一回当たり何ワード実行か"), Tooltip("0以上")]
	private int TaskSetNumExperiment;


	private void Awake() {
		FieldSerialize();
	}

	private void OnValidate() {
		FieldSerialize();
	}

	private void FieldSerialize() {
		//プラットフォーム
		platformType = PlatformType;
		//シーン
		sceneType = SceneType;
		//内径
		radiusIn = RadiusIn / 100;
		//外形
		radiusOut = RadiusOut / 100;
		//内径(Circle用副輪)
		radiusIn_subCircle = RadiusIn_subCircle / 100;
		//外形(Circle用副輪)
		radiusOut_subCircle = RadiusOut_subCircle / 100;
		//厚さ
		poleHeight = PoleHeight / 100;
		//キー数
		//poleSum = PoleSum;
		//縁取りの太さ
		lineRendererWidth = LineRendererWidth / 100;
		//縁取りのずらし
		lineShiftSlightly = LineShiftSlightly / 100;

		//多角形のマテリアル初期化
		material_PolygonalPillar = Material_PolygonalPillar;
		//台形柱通常
		material_TrapezoidPole_Normal = Material_TrapezoidPole_Normal;
		//台形柱接触
		material_TrapezoidPole_Touch = Material_TrapezoidPole_Touch;
		//台形柱通常(Circleでの非アクティブ時)
		material_TrapezoidPole_Normal_Nonactive = Material_TrapezoidPole_Normal_Nonactive;
		//台形柱接触(Circleでの非アクティブ時)
		material_TrapezoidPole_Touch_Nonactive = Material_TrapezoidPole_Touch_Nonactive;
		//台形の強調ライン
		material_LineRenderer = Material_LineRenderer;
		//台形の強調ライン(Circleでの非アクティブ時)
		material_LineRenderer_Nonactive = Material_LineRenderer_Nonactive;
		//テキストの色(Circle用)
		material_Text = Material_Text;
		//テキストの色(Circle用非アクティブ時)
		material_Text_Nonactive = Material_Text_Nonactive;
		//タイピングソフトのクリアー部分の文字の色
		material_Typing_Clear = Material_Typing_Clear;
		//タイピングソフトのエラー部分の文字の色
		material_Typing_Error = Material_Typing_Error;
		//タイピングソフトのその他部分の文字の色
		material_Typing_Other = Material_Typing_Other;

		//台形の分割回数
		trapezoidDivisionNum = TrapezoidDivisionNum;

		//システムキーの半径
		systemCommandRadius = SystemCommandRadius / 100;
		//ピンチ時の指とシステムの距離
		pinchDistance = PinchDistance / 100;
		//ピンチ判定用の相対座標の閾値
		pinchLength = PinchLength / 100;
		//テキストのマテリアル
		material_SystemText = Material_SystemText;
		//テキストのフォントサイズ
		systemTextFontSize = (int)( SystemTextFontSize * 100 );

		//stage初期化
		stage = 0;

		//システムの種別判定
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName.Substring(0, 6) == "Circle") {
			isCircleSystem = true;
		} else {
			isCircleSystem = false;
		}
		if (sceneName.Substring(0, 6) == "50Kana") {
			is50Kana = true;
		} else {
			is50Kana = false;
		}

		//ログ出力用
		logInstance = LogInstance;

		//ログ出力ディレクトリ
		logDirectory = LogDirectory;

		//50音システムの各値初期化
		cubesIntervalX = CubesIntervalX / 100;
		cubesIntervalY = CubesIntervalY / 100;
		cubeWidth = CubeWidth / 100;
		cubeVertical = CubeVertical / 100;
		cubeAngle = Mathf.Cos(CubeAngle * Mathf.Deg2Rad);

		//実験関係
		seedExperiment = SeedExperiment;
		numExperiment = NumExperiment;
		taskSetNumExperiment = TaskSetNumExperiment;
	}
}
