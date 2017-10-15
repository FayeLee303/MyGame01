using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : FightingBaseView
{
    //这个Id是在面板里设置的，加上PlayMaker后做成预制体
    public int id;
    private RoleModel monster;

    //重写父类里的Start函数，在这里初始化
    protected override void Start()
    {
        //获取怪物的属性
        monster = DataBaseManager.Instance.FindRole(id);
        base.Start();
    }

    void Update () {
        GameUpdate();

    }

    public override void GameUpdate()
    {
    }
}
