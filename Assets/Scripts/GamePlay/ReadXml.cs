using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using StructData;

public class ReadXml : MonoBehaviour
{
    //存属性名
    List<string> xmlName = new ();
    //存键和值
    public Text text;
    public Dictionary<string, Xmldiction> XmlDiction;
    void Start()
    {
        print(Paths.getFilePath("project.xml"));
        XmlDiction = new();
        StartCoroutine(XMLUtils.xmlLoad(Paths.getFilePath("project.xml"), (XmlElement xml) => {
            Xmldiction dic = new();
            dic=Play(xml);
            dic.name = "project.xml";
            xmlName.Add(dic.name);
            XmlDiction.Add(dic.name, dic);
            test();
        }));
    }

    public void test()
    {
        text.text = Application.dataPath;

    }
    Xmldiction Play(XmlElement xml)
    {
        Xmldiction dic = new ();
        dic.name = xml.Name;
        int cont = 0;
        dic.Key = new();
        //判断是否有标签属性
        foreach (XmlAttribute item in xml.Attributes)
        {
            cont++;
            if (dic.Key.ContainsKey(item.Name))
            {
                dic.Key[item.Name]= item.Value;
            }
            else
            {
                dic.Key.Add(item.Name, item.Value);
            }
            if (cont== xml.Attributes.Count)
            {
                if (!string.IsNullOrEmpty(xml.Value))
                {
                    //print(xml.Value);
                }
            }
        }
        //判断是否有子节点
        dic.Child = new();
        if (xml != null && xml.HasChildNodes && xml.ChildNodes[0]?.Name != "#text" && xml.ChildNodes[0]?.Name != "#cdata-section" && xml.ChildNodes.Count > 0)
        {
            //Debug.LogError(xml.Name);
            foreach (XmlElement item in xml)
            {
                //dic.Child.Add(item.Name,Play(item));
                if (dic.Child.ContainsKey(item.Name))
                {

                }
                else
                {
                    dic.Child.Add(item.Name, Play(item));
                }
            }
        }
        else
        {
            //没有子节点
            //print(xml.Name + xml.InnerText);
            dic.text = xml.InnerText;
        }
        return dic;
    }
    void Update()
    {
        
    }
}
