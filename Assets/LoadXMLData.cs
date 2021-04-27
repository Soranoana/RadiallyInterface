using UnityEngine;

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
        // XDocument xml = XDocument.Load(Application.dataPath + "/sample.xml");

        //テーブルを読み込む
        XElement table = xml.Element("リスト");

        //データの中身すべてを取得
        var rows = table.Elements("データ");

        //取り出し
        foreach (XElement row in rows)
        {
            XElement item = row.Element("名前");
            Debug.Log(item.Value);
        }
    }
}
