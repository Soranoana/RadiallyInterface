//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using System;
using System.IO;

public class logSave : MonoBehaviour {

    private StreamWriter sw;
    private string filePath;
    private float deltaTime;
    private float deltaTimeF;

    private int deleteCount = 0;
    private float sumTime = 0;
    private int textSum = 0;
    private float startTime = 0f;
    private float currentTime = 0f;

    void Start() {
        string nowdate = Nowdate();

        if (variables.logDirectory == "") {
            filePath = "C:/logSave";
        } else {
            filePath = variables.logDirectory;
        }

        Directory.CreateDirectory(filePath);
        string systemName;
        if (variables.isCircleSystem) {
            systemName = "Circle";
        } else {
            systemName = "Radially";
        }

        /* プラットホーム依存コンパイル */
# if UNITY_EDITOR
        filePath += "/log" + nowdate + " " + systemName + ".csv";
# elif UNITY_STANDALONE_WIN
        filePath += "/log" + nowdate + " " + systemName + ".csv";
# elif UNITY_ANDROID
        filePath = Application.persistentDataPath + "/log" + " " + systemName + ".csv";
# endif
        /* プラットホーム依存コンパイル ここまで*/

        sw = new StreamWriter(filePath, true, System.Text.Encoding.GetEncoding("utf-8"));
        //nowdate = nowdate.Replace("／", "/");
        //nowdate = nowdate.Replace("：", ":");
        sw.WriteLine(nowdate);
        string swStr = "\tRealtime\tGameTime\tDeltaTime\tEvent\n";

        sw.Write(swStr.Replace("\t", ","));
        sw.Flush();
        sw.Close();
        //Debug.Log(swStr.Replace("\t", " "));
        deltaTime = NowTimeNum();
        deltaTimeF = Time.fixedTime;
    }

    void Update() {
        sumTime += Time.deltaTime;
    }

    public void LogSaving(string log1, string log2) {
        float ftime = Time.fixedTime;
        sw = new StreamWriter(filePath, true);
        string swStr = "";
        swStr += "\t" + NowTime()/*.Replace("：", ":")*/ + "\t";
        //swStr += Time.fixedTime.ToString() + "\t";
        swStr += sumTime + "\t";
        swStr += ( NowTimeNum() - deltaTime ).ToString("N2") + "\t";
        //swStr += "FixedTime\t" + ( ftime - deltaTimeF ).ToString("N2") + "\t";

        swStr += log1 + "\n";
        swStr += "\t\t\t\t" + log2 + "\n";

        deltaTime = NowTimeNum();
        deltaTimeF = ftime;

        //合計時間計算用
        if (startTime == 0f) {
            startTime = sumTime;
        }
        currentTime = sumTime;

        //CSV対応するために置き換え
        sw.Write(swStr.Replace("\t", ","));
        sw.Flush();
        sw.Close();
        Debug.Log(swStr.Replace("\t", " "));
    }

    private string NowTime() {
        string dateTimeStr = System.DateTime.Now.Hour.ToString("D2") + "-"
                           + System.DateTime.Now.Minute.ToString("D2") + "-"
                           + System.DateTime.Now.Second.ToString("D2") + "."
                           + System.DateTime.Now.Millisecond.ToString("D3");
        return dateTimeStr;
    }

    private string Nowdate() {
        string dateTimeStr = System.DateTime.Now.Year.ToString("D4") + "／"
                           + System.DateTime.Now.Month.ToString("D2") + "／"
                           + System.DateTime.Now.Day.ToString("D2") + " "
                           + System.DateTime.Now.Hour.ToString("D2") + "："
                           + System.DateTime.Now.Minute.ToString("D2") + "："
                           + System.DateTime.Now.Second.ToString("D2");
        return dateTimeStr;
    }

    private float NowTimeNum() {
        float dateTimeFlo = System.DateTime.Now.Hour * 3600
                          + System.DateTime.Now.Minute * 60
                          + System.DateTime.Now.Second
                          + System.DateTime.Now.Millisecond * 0.001f;
        return dateTimeFlo;
    }

    public void OnDelete() {
        deleteCount++;
    }

    public void textCount(int count) {
        textSum += count;
    }

    private void OnApplicationQuit() {
        sw = new StreamWriter(filePath, true);
        string swStr = "";
        swStr += "\n";
        swStr += "\t\t\terror\t" + deleteCount + "\n";
        swStr += "\t\t\tcharactor\t" + textSum + "\n";
        swStr += "\t\t\ttime\t" + (currentTime-startTime).ToString("N2") + "\n";
        sw.Write(swStr.Replace("\t", ","));
        sw.Flush();
        sw.Close();
        Debug.Log("quit");
    }
}
