using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class XmlTools : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadXml(string pass)
    {
        if (!Directory.Exists(Paths.getFilePath(pass))) 
        {
            Directory.CreateDirectory(Paths.AndroidExternalPath);

            

        }
    }
    /// <summary>
    /// 文件的创建，写入
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="name">文件名</param>
    /// <param name="info">信息</param>
    public static void Createfile(string path, string name, string info)
    {
        StreamWriter sw;//流信息
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {//判断文件是否存在
            sw = t.CreateText();//不存在，创建
        }
        else
        {
            sw = t.AppendText();//存在，则打开
        }
        sw.WriteLine(info);//以行的形式写入信息
        sw.Close();//关闭流
        sw.Dispose();//销毁流
    }
    /// <summary>
    /// 文件的读取
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="name">文件名</param>
    /// <returns>文件数据</returns>
    public static ArrayList LoadFile(string path, string name)
    {
        StreamReader sr = null;//文件流
        try
        {//通过路径和文件名读取文件
            sr = File.OpenText(path + "//" + name);
        }
        catch (Exception ex)//需要引入命名空间 using System
        {
            Debug.LogError(ex.Message);
            return null;
        }
        string line;
        ArrayList arrlist = new ArrayList();//需要引入命名空间 using System.Collections
        while ((line = sr.ReadLine()) != null)
        {//读取每一行加入到ArrayList中
            arrlist.Add(line);
        }
        sr.Close();
        sr.Dispose();
        return arrlist;
    }

}
