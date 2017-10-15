using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager {
    //做成单例模式
    private static DataBaseManager _instance;
    public static DataBaseManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DataBaseManager();
            }
            return _instance;
        }

    }

    //私有的构造方法
    private DataBaseManager()
    {
        ParseItemJson();
    }

    public void Init()
    {
        //Do Nothing
    }

    public List<RoleModel> roleList;

    //读取数据
    private void ParseItemJson()
    {
        //文本在Unity里面是TextAsset类型
        TextAsset roleJsonText = Resources.Load<TextAsset>("Config/RoleConfig");
        if (roleJsonText != null)
        {
            string roleJsonString = roleJsonText.text;
            roleList = JsonMapper.ToObject<List<RoleModel>>(roleJsonString);
            if (roleList == null) {
                Debug.Log("储存文件失败");
            }
        }
        else
        {
            Debug.Log("读取文件失败");
        }
    }

    //根据Id找RoleModel
    public RoleModel FindRole(int id)
    {
        RoleModel role = roleList[id];
        return role;
    }
}
