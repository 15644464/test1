using System.Collections.Generic;
using System.Collections;
using System.Xml;

namespace StructData {
    /// <summary>
    /// xml容器
    /// </summary>
    
    public struct Xmldiction
    {
        //标签名
        public string? name;
        //标签内容
        public string? text;
        //属性
        public Dictionary<string, string>? Key;
        //子节点
        public Dictionary<string, Xmldiction>? Child;
    }

    
}