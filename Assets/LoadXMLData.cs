using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class LoadXMLData : MonoBehaviour
{

    public XDocument FingerDataXml;

    void Start()
    {
        LoadData();
    }

    private void LoadData()
    {

        //ディレクトリ指定してファイルを読み込み
        //XElement p;
        XDocument xml = XDocument.Load(Application.dataPath + "/FingerDataXml.xml");
        //var names = xml.Descendants("VIVE").Descendants("LeapMotion").Select(p => p.Element("ListMatrixR")?.Value);
        IEnumerable<XElement> xelements = xml.Root.Elements().Where(p => p.Value);

        //Debug.Log("names:" + string.Join("", "", names));
        //Debug.Log("p:" + p);
        Debug.Log("xml:" + xml);
        //テーブルを読み込む
        //XElement table = xml.Element("リスト");

        //データの中身すべてを取得
        //var rows = table.Elements("データ");

        //取り出し
        foreach (var row in names)
        {
            //XElement item = row.Element("名前");
            Debug.Log(row.p);
            //Debug.Log(item.Value);
        }
    }
}
