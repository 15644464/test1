/************************************************
 * 创建人:      
 * 创建日期:  
 * 应用场景:加载xml文件
 * 
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
//using Unity.VectorGraphics;

public static class XMLUtils {
    public static string PATH = $"{Application.streamingAssetsPath}/Data/";
    public static XmlNodeList xmlLoad (string filePath, string singleNode) {
        if (File.Exists (filePath)) {
            XmlDocument _xmlDoc = new XmlDocument ();
            XmlReaderSettings _set = new XmlReaderSettings ();
            _set.IgnoreComments = true;
            XmlReader _reader = XmlReader.Create (filePath, _set);
            _xmlDoc.Load (_reader);
            _reader.Close ();
            XmlNodeList _nodeList = _xmlDoc.SelectSingleNode (singleNode).ChildNodes;
            return _nodeList;
        }
        return null;
    }


    public static IEnumerator xmlLoad (string filePath, string singleNode, Action<XmlNodeList> action) {
        UnityWebRequest uwr = UnityWebRequest.Get (filePath);
        yield return uwr.SendWebRequest ();
        string str = uwr.downloadHandler.text;
        XmlDocument _xmlDoc = new XmlDocument ();
        _xmlDoc.LoadXml (str);
        XmlNodeList _nodeList = _xmlDoc.SelectSingleNode (singleNode).ChildNodes;
        action.Invoke (_nodeList);
    }
    public static IEnumerator xmlLoad(string filePath, Action<XmlElement> action)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(filePath);
        yield return uwr.SendWebRequest();
        string str = uwr.downloadHandler.text;
        
        XmlDocument _xmlDoc = new XmlDocument();
        _xmlDoc.LoadXml(str);
        XmlElement xmlElement = _xmlDoc.DocumentElement;
        action.Invoke(xmlElement);
    }

    public static IEnumerator imageLoad(string filePath, Action<Texture> action)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(filePath);
        DownloadHandlerTexture downloadHandlerTexture = new();
        uwr.downloadHandler = downloadHandlerTexture;
        yield return uwr.SendWebRequest();
        if (string.IsNullOrEmpty(uwr.error))
        {
            action?.Invoke(downloadHandlerTexture.texture);
        }
        else
        {
            Debug.LogError(uwr.error + "  " + filePath);
        }
    }
    public static IEnumerator UnityWebRequestGetData(string _url, Action<Texture2D> OnTextureLoad)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_url))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isHttpError || uwr.isNetworkError) Debug.Log(uwr.error);
            else if (uwr.isDone) OnTextureLoad?.Invoke(DownloadHandlerTexture.GetContent(uwr));
        }
    }
    //svg加载添加com.unity.vectorgraphics插件
    //public static IEnumerator UnityWebRequestsetSvg(string _url, Action<Texture2D> OnTextureLoad)
    //{
    //    using (UnityWebRequest uwr = UnityWebRequestTexture(_url))
    //    {
    //        var path = Paths.getFilePath($"line.svg");
    //        UnityWebRequest webRequest = UnityWebRequest.Get(path);
    //        yield return webRequest.SendWebRequest();
    //        if (string.IsNullOrEmpty(webRequest.error))
    //        {
    //            var svg = new VectorUtils.TessellationOptions()
    //            {
    //                StepDistance = 2.0f,
    //                MaxCordDeviation = 1.0f,
    //                MaxTanAngleDeviation = 0.5f,
    //                SamplingStepSize = 100.0f,
    //            };
    //            var sceneInfo = SVGParser.ImportSVG(new StringReader(webRequest.downloadHandler.text));
    //            var geoms = VectorUtils.TessellateScene(sceneInfo.Scene, svg);

    //            var sprite = VectorUtils.BuildSprite(geoms, 100.0f, VectorUtils.Alignment.Center, Vector2.zero, 128, true);
    //            transform.GetComponent<SVGImage>().sprite = sprite;
    //            transform.GetComponent<SVGImage>().color += new Color(0, 0, 0, 1);
    //            yield return 1;
    //        }
    //    }
    //}

    public static IEnumerator loadImage(string path, Action<Texture> action)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(path);
        DownloadHandlerTexture downloadHandlerTexture = new();
        webRequest.downloadHandler = downloadHandlerTexture;
        yield return webRequest.SendWebRequest();
        if (string.IsNullOrEmpty(webRequest.error))
        {
            action?.Invoke(downloadHandlerTexture.texture);
        }
        else
        {
            Debug.LogError(webRequest.error + "  " + path);
        }
    }
    public static IEnumerator xmlLoad (List<string> filePath, string singleNode, Action<List<XmlNodeList>> action) {
        List<XmlNodeList> nodeList = new List<XmlNodeList> ();
        foreach (var item in filePath) {
            if (item.EndsWith(".meta")) {
                continue;
            }
            UnityWebRequest uwr = UnityWebRequest.Get (item);
            yield return uwr.SendWebRequest();
            string str = uwr.downloadHandler.text;
            if (string.IsNullOrEmpty (uwr.error)) {
                XmlDocument _xmlDoc = new XmlDocument ();
                _xmlDoc.LoadXml (str);
                XmlNodeList _nodeList = _xmlDoc.SelectSingleNode (singleNode).ChildNodes;
                nodeList.Add (_nodeList);
            } else {
                Debug.LogError (uwr.error);
            }
            
        }
        action.Invoke (nodeList);
    }
    public static IEnumerator xmlLoad (string[] filePath, string singleNode, Action<List<XmlNodeList>> action) {
        List<XmlNodeList> nodeList = new List<XmlNodeList> ();
        foreach (var item in filePath) {
            if (item.EndsWith (".meta")) {
                continue;
            }
            UnityWebRequest uwr = UnityWebRequest.Get (item);
            yield return uwr.SendWebRequest ();
            string str = uwr.downloadHandler.text;
            if (string.IsNullOrEmpty (uwr.error)) {
                Debug.Log (str);
                XmlDocument _xmlDoc = new XmlDocument ();
                _xmlDoc.LoadXml (str);
                XmlNodeList _nodeList = _xmlDoc.SelectSingleNode (singleNode).ChildNodes;
                nodeList.Add (_nodeList);
            } else {
                Debug.LogError (uwr.error);
            }
        }
        action.Invoke (nodeList);
    }


    // public static void loadImage (string[] imagePath, Image[] image, float width = 0, float height = 0) {

    //     LoadImageHelp.instance.load (imagePath, (int index,Texture texture) => {
    //         Texture2D tx = (Texture2D) texture;
    //         float diff = (float) tx.width / (float) tx.height;
    //         float heightDiff = width / diff;
    //         Texture2D result = new Texture2D ((int) width, (int) heightDiff, tx.format, true);

    //         Color [] rpixels = result.GetPixels (0);
    //         float incX = (1f / width);
    //         float incY = (1f / heightDiff);
    //         for (int px = 0; px < rpixels.Length; px++) {
    //             rpixels [px] = tx.GetPixelBilinear (incX * ((float) px % width), incY * ((float) Mathf.Floor (px / width)));
    //         }
    //         result.SetPixels (rpixels, 0);
    //         result.Apply ();
    //         image[index].sprite = Sprite.Create (result, new Rect (0, Math.Max (result.height / 2 - height / 2, 0), result.width, Math.Min (height, result.height)), new Vector2 (0.5f, 0.5f));
    //     });



    // }
    // public static void loadImage (List<string> imagePath, List<Image> image, float width = 0, float height = 0) {

    //     LoadImageHelp.instance.load (imagePath, (int index, Texture texture) => {
    //         Texture2D tx = (Texture2D) texture;
    //         float diff = (float) tx.width / (float) tx.height;
    //         float heightDiff = width / diff;
    //         Texture2D result = new Texture2D ((int) width, (int) heightDiff, tx.format, true);

    //         Color [] rpixels = result.GetPixels (0);
    //         float incX = (1f / width);
    //         float incY = (1f / heightDiff);
    //         for (int px = 0; px < rpixels.Length; px++) {
    //             rpixels [px] = tx.GetPixelBilinear (incX * ((float) px % width), incY * ((float) Mathf.Floor (px / width)));
    //         }
    //         result.SetPixels (rpixels, 0);
    //         result.Apply ();
    //         image [index].sprite = Sprite.Create (result, new Rect (0, Math.Max (result.height / 2 - height / 2, 0), result.width, Math.Min (height, result.height)), new Vector2 (0.5f, 0.5f));
    //     });



    // }

    /// <summary>
    /// 绘制一个圆
    /// </summary>
    /// <param name="self">LineRenderer组件</param>
    /// <param name="center">圆的中心点坐标</param>
    /// <param name="direction">圆的朝向</param>
    /// <param name="radius">圆的半径</param>
    /// <param name="thickness">圆的宽度 即LineRenderer组件width</param>
    /// <returns></returns>
    public static LineRenderer DrawCircle(this LineRenderer self, Vector3 center, Vector3 direction, float radius, float thickness)
    {
        //设置宽度
        self.startWidth = thickness;
        self.endWidth = thickness;
        //设置坐标点个数
        self.positionCount = 360;
        //设置闭环
        self.loop = true;
        //朝向
        Quaternion q = Quaternion.FromToRotation(Vector3.up, direction);
        float x, z;
        //每一度求得一个在圆上的坐标点
        for (int i = 0; i < 360; i++)
        {
            x = radius * Mathf.Cos(i * Mathf.PI / 180f);
            z = radius * Mathf.Sin(i * Mathf.PI / 180f);
            Vector3 pos = new Vector3(x, 0, z);
            //
            pos = q * pos + center;
            self.SetPosition(i, pos);
        }
        return self;
    }

    private static byte [] getImageByte (string imagePath) {
        FileStream files = new FileStream (imagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        byte [] imgByte = new byte [files.Length];
        try {
            files.Read (imgByte, 0, imgByte.Length);
        } catch (Exception) {

        } finally {
            files.Close ();
        }
        return imgByte;
    }


}
