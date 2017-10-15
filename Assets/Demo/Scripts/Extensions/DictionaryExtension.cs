using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 字典扩展类
/// </summary>
public static class DictionaryExtension  {

    /// <summary>
    /// 尝试根据key得到value，得到了直接返回value,没有得到就返回null
    /// this Dictionary<Tkey,Tvalue> dict是要获取值的字典
    /// 当通过某字典.TryGet()调用时就不用再填写类型了，只需要填key就可以
    /// </summary>
    public static Tvalue TryGet<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict,Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

}
