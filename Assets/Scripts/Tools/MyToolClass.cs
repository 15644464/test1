#nullable enable
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Drawing;
//using Protocol;

namespace MyTool
{
    public static class MyToolClass
    {


        /// <summary>
        /// 序列化obj对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializableObject(this object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                byte[] byArr = new byte[(int)ms.Length];
                Buffer.BlockCopy(ms.GetBuffer(), 0, byArr, 0, byArr.Length);
                return byArr;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public static object DeserializeObject(this byte[] by)
        {
            using (MemoryStream ms = new MemoryStream(by))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(ms);
                return obj;
            }
        }

        /// <summary>
        /// 围绕targe旋转
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="targe">目标</param>
        /// <param name="axis">旋转轴</param>
        /// <param name="angle">旋转速度</param>
        public static void MyRotateAround(this Transform trans, Transform targe, Vector3 axis, float angle)
        {
            // 定义一个 以向量 axis 为旋转轴（y 轴），旋转angle 度的 四元数
            Quaternion rot = Quaternion.AngleAxis(angle, axis);

            //计算从目标指向摄像头的朝向向量,旋转此向量
            Vector3 dir = rot * (trans.position - targe.position);
            //旋转此向量=camera.LookAt(targe);
            //camera.LookAt(targe);
            //移动摄像机位置
            trans.position = targe.position + dir;
            //设置角度
            trans.rotation = rot * trans.rotation;
            //设置角度另一种方法
            //transform.rotation *= Quaternion.Inverse(myrot) * rot * camera.rotation;

            ////另一种写法，x>90会有bug
            //camera.rotation = Quaternion.LookRotation(-dir);
        }

        /// <summary>
        /// 将transform转换为float数组存储
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static float[] serTransform(this Transform trans)
        {
            float[] arr = {trans.position.x, trans.position.y, trans.position.z,
                           trans.rotation.x, trans.rotation.y, trans.rotation.z,trans.rotation.w,
                           trans.localScale.x, trans.localScale.y, trans.localScale.z };
            return arr;
        }

        //public static void setTransform(this Transform trans ,SerTransform ser)
        //{
        //    trans.position = ser.position.setSerVector();
        //    trans.rotation = ser.rotation.setSerQua();
        //    trans.localScale = ser.scale.setSerVector();
        //}

        //public static Vector3 setSerVector(this SerVector sv)
        //{
        //    return new Vector3(sv.x, sv.y, sv.z);
        //}

        //public static Quaternion setSerQua(this SerVector sv)
        //{
        //    return new Quaternion(sv.x, sv.y, sv.z,sv.w);
        //}
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="transform"></param>
        /// <param name="Velue">赋值</param>
        /// <param name="velueName">属性名称</param>
        public static object GetObjectAttribute<T1>(this Transform? tran, string velueName)
        {
            object velue = null;
            if (tran != null)
            {
                var item = tran.GetComponent<T1>();
                if (item != null)
                {
                    velue = GetPropertyValue<T1>(item, velueName);
                }
            }
            return velue;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="transform"></param>
        /// <param name="Velue">赋值</param>
        /// <param name="velueName">属性名称</param>
        public static void SetObjectAttribute<T1, T2>(this Transform? transform, string velueName, T2 Velue)
        {
            if (transform != null)
            {
                var textMeshPro = transform.GetComponent<T1>();
                if (textMeshPro != null)
                {
                    SetPropertyValue<T1, T2>(textMeshPro, Velue, velueName);
                }
            }
        }

        /// <summary>
        /// 根据属性名称获取属性值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(T entity, string propertyName)
        {
            object result = null;
            Type entityType = typeof(T);
            try
            {
                PropertyInfo proInfo = entityType.GetProperty(propertyName);
                result = proInfo.GetValue(entity);
            }
            catch (Exception)
            {
                Debug.Log($"没没有{propertyName}属性");
            }
            return result;
        }

        /// <summary>
        /// 根据属性名称设置属性值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="Velue">修改值</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        private static void SetPropertyValue<T1, T2>(T1 entity, T2 Velue, string propertyName)
        {
            object result = null;
            Type entityType = typeof(T1);
            try
            {
                PropertyInfo proInfo = entityType.GetProperty(propertyName);
                result = proInfo.GetValue(entity);
                if (Velue is T2)
                    proInfo.SetValue(entity, Velue);
                else
                {
                    Debug.Log($"{entity}是{entity.GetType()}类型,{Velue}是{Velue.GetType()}类型");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        public static List<Vector3> v3_list(this Vector3[] pos)
        {
            List<Vector3> list = new List<Vector3>();
            for (int i = 0; i < pos.Length; i++)
            {
                list.Add(pos[i]);
            }
            return list;

        }

        /// <summary>
        /// 销毁子物体
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="startIndex"></param>
        public static void destroyImmediateChild(this Transform? transform, int startIndex = 0)
        {
            if (transform != null)
            {
                for (int i = startIndex; i < transform.childCount; i++)
                {
                    GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
                    i--;
                }
            }
        }
        /// <summary>
        /// 销毁子物体
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="startIndex">从第几个开始销毁，默认为0</param>
        public static void destroyImmediateChild(this GameObject? gameObject, int startIndex = 0)
        {
            if (gameObject != null)
            {
                gameObject.transform.destroyImmediateChild(startIndex);
            }
        }

        /// <summary>
        /// 隐藏子物体
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="callback"></param>
        /// <param name="startIndex"></param>
        public static void disableChild(this Transform? transform, Action<Transform?>? callback = null, int startIndex = 0)
        {
            if (transform != null)
            {
                for (int i = startIndex; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    if (child != null)
                    {
                        child.gameObject.SetActive(false);
                    }
                    callback?.Invoke(child);
                }
            }
        }

        /// <summary>
        /// 获取符合规则的子物体
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="func"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static Transform? GetChild(this Transform? transform, Func<Transform?, bool> func, int startIndex = 0)
        {
            if (transform != null && func != null)
            {
                Transform? result = null;
                for (int i = startIndex; i < transform.childCount; i++)
                {
                    var child = transform.GetChild(i);
                    if (func.Invoke(child))
                    {
                        result = child;
                        break;
                    }
                }
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取符合规则的子物体或者创建一个新的子物体
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="item"></param>
        /// <param name="func"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static Transform? getChildOrCreate(this Transform? transform, Transform? item, Func<Transform?, bool> func, int startIndex = 0)
        {
            var result = transform.GetChild(func, startIndex);
            if (result == null && item != null && transform != null)
            {
                result = GameObject.Instantiate(item, transform);
            }
            return result;
        }



        public static void setText(this Transform? transform, string childName, string text)
        {
            if (transform != null)
            {
                //var child = transform.Find(childName);
                //var textMeshPro = child?.GetComponent<TextMeshProUGUI>();
                //if (textMeshPro != null)
                //{
                //    textMeshPro.text = text;
                //}
                //else
                //{
                //    var textCom = child?.GetComponent<Text>();
                //    if (textCom != null)
                //    {
                //        textCom.text = text;
                //    }
                //}
            }
        }


        public static void setInteractable<T>(this Transform? transform, bool key)
        {
            if (transform != null)
            {
                var butt = transform.GetComponent<T>();
                if (butt != null)
                {

                }
            }
        }

        public static void setTextSize(this Transform? transform, float size)
        {
            if (transform != null)
            {
                //var textMeshPro = transform?.GetComponent<TextMeshProUGUI>();
                //if (textMeshPro != null)
                //{
                //    textMeshPro.fontSize = size;
                //}
                //else
                //{
                //    var textCom = transform?.GetComponent<Text>();
                //    if (textCom != null)
                //    {
                //        textCom.fontSize = (int)size;
                //    }
                //}
            }
        }
        /// <summary>
        /// 返回text color
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Color getTextColor(this Transform? transform)
        {
            if (transform != null)
            {
                //var textMeshPro = transform?.GetComponent<TextMeshProUGUI>();
                //if (textMeshPro != null)
                //{
                //    return textMeshPro.color;
                //}
                //else
                //{
                //    var textCom = transform?.GetComponent<Text>();
                //    if (textCom != null)
                //    {
                //        return textCom.color;
                //    }
                //}
            }
            return Color.white;
        }
        /// <summary>
        /// 返回text Size
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static object getTextSize<T>(this Transform? transform)
        {
            if (transform != null)
            {
                var data = transform.GetComponent<T>();
                if (data != null)
                {
                    var item = transform.GetObjectAttribute<T>("size");
                    if (item != null)
                    {
                        var size = item;
                        return size;
                    }
                }
            }
            return null;
        }

        public static void setTextColor(this Transform? transform, Color color)
        {
            if (transform != null)
            {
                //var textMeshPro = transform?.GetComponent<TextMeshProUGUI>();
                //if (textMeshPro != null)
                //{
                //    textMeshPro.color = color;
                //}
                //else
                //{
                //    var textCom = transform?.GetComponent<Text>();
                //    if (textCom != null)
                //    {
                //        textCom.color = color;
                //    }
                //}
            }
        }


        public static void setTextFontSize(this Transform? transform, string childName, float fontSize)
        {
            if (transform != null)
            {
                ////var child = transform.Find(childName);
                ////var textMesh = child?.GetComponent<TextMeshProUGUI>();
                ////if (textMesh != null)
                ////{
                ////    textMesh.fontSize = fontSize;
                ////}
                //else
                //{
                //    var text = transform?.GetComponent<Text>();
                //    if (text != null)
                //    {
                //        text.fontSize = (int)fontSize;
                //    }
                //}
            }
        }

        public static void setTextFontSize(this Transform? transform, float fontSize)
        {
            if (transform != null)
            {
                //var textMesh = transform?.GetComponent<TextMeshProUGUI>();
                //if (textMesh != null)
                //{
                //    textMesh.fontSize = fontSize;
                //}
                //else
                //{
                //    var text = transform?.GetComponent<Text>();
                //    if (text != null)
                //    {
                //        text.fontSize = (int)fontSize;
                //    }
                //}
            }
        }

        public static RectTransform? asRectTransform(this Transform? transform)
        {
            if (transform != null)
            {
                return transform.GetComponent<RectTransform>();
            }
            return null;
        }

        public static string? getActionValue(this string[]? datas, string actionName)
        {
            string? result = null;
            if (datas != null)
            {
                foreach (var item in datas)
                {
                    if (item.Contains(","))
                    {
                        var action = item.Split(",");
                        if (action[0] == actionName && action.Length > 1)
                        {
                            result = action[1];
                        }
                    }
                }
            }
            return result;
        }

        public static void deleteAction(this string[]? datas, string actionName)
        {
            if (datas != null)
            {
                for (int i = 0; i < datas.Length; i++)
                {
                    if (datas[i].Contains($"{actionName},"))
                    {
                        datas[i] = "delete";
                    }
                }
            }
        }

        public static int? StringShiftInt(this string str)
        {
            if (int.TryParse(str, out int j))
            {
                return j;
            }
            return null;
        }

        public static T? createMonoObject<T>(this Transform? transform) where T : MonoBehaviour
        {
            if (transform != null)
            {
                var monoObject = new GameObject(typeof(T).Name);
                monoObject.transform.SetParent(transform);
                return monoObject.AddComponent<T>();
            }
            return null;
        }


        //public static void panelStartTween(this Transform? transform, OpenUIAnimationType openUIAnimationType, float durationTime, Action? callBack)
        //{
        //    if (transform != null && openUIAnimationType != OpenUIAnimationType.None)
        //    {
        //        var animation = transform.GetComponent<PanelAnimation>();
        //        if (animation == null)
        //        {
        //            animation = transform.gameObject.AddComponent<PanelAnimation>();
        //        }
        //        animation.play(openUIAnimationType, durationTime, callBack);
        //    }
        //    else
        //    {
        //        callBack?.Invoke();
        //    }
        //}

        //public static Vector3 toPosition(this Vector2 coordinate, float height)
        //{
        //    if (MapTypeController.IsSphere)
        //    {
        //        return EarthCoordinatesUtils.coordinateToTranslationNormalized(coordinate, MapTypeController.IsSphere)
        //        * (Constants.RealEarthRadius + height);
        //    }
        //    else
        //    {
        //        var pos = EarthCoordinatesUtils.coordinateToTranslationNormalized(coordinate, MapTypeController.IsSphere)
        //        * (Constants.RealEarthRadius);
        //        pos.y = height;
        //        return pos;
        //    }
        //}

        //public static Vector2 toCoordinate(this Vector3 position, bool isSphere)
        //{
        //    return EarthCoordinatesUtils.translationToCoordinate(position, isSphere);
        //}

        public static bool isPointerOverGameObject()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;
            var result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, result);
            int count = result.FindAll(obj => obj.gameObject.layer == 5).Count;
            return count > 0;
        }
    }

}

