using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 得到怪物信息
/// </summary>
public class MonsterObj : MonoBehaviour {

    //这个Id是在面板里设置的，加上PlayMaker后做成预制体
    public int id;
    public RoleModel Monster{ get; set; }
    // Use this for initialization
    void Start () {
        //获取怪物的属性
        SetMonster(id);
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMonster(int id)
    {
        this.Monster = DataBaseManager.Instance.FindRole(id);
    }
}
