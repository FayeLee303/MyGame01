﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel  {
    public int Id { get; set; }//武器编号
    public string Name { get; set; }//武器名字
    public WeaponType Type { get; set; }//武器类型
    public string Description { get; set; }
    public int Atk { get; set; }//武器攻击力加成
    public int Def { get; set; }//防御力加成
    public int AtkRadius { get; set; }//攻击距离半径
    public int AtkSpeed { get; set; }//攻击速度加成
    public int MoveSpeed { get; set; }//移动速度加成
    public int SkillId { get; set; }//附加技能
    public float CoolingTime { get; set; } //冷却时间
    public int BuyPrice { get; set; }//买入价格
    public int SellPrice { get; set; }//卖出价格
    public string SpritePath { get; set; } //图片存储位置

    public enum WeaponType
    {
        MainHand = 0, //主手武器
        OffHand = 1, //副手武器        
        Head = 2,//头戴的
        Foot = 3,//鞋
    }

    ///// <summary>
    ///// 构造函数
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="name"></param>
    ///// <param name="atk"></param>
    ///// <param name="atkradius"></param>
    ///// <param name="atkspeed"></param>
    ///// <param name="skill"></param>
    //public WeaponModel(int id, string name,int atk,int atkradius,int atkspeed,List<SkillModel> skill) {
    //    this.Id = id;
    //    this.Name = name;
    //    this.Atk = atk;
    //    this.AtkRadius = atkradius;
    //    this.AtkSpeed = atkspeed;
    //    this.Skill = skill;
    //}


    //得到显示面板应该显示的内容
    public string GetToolTipText()
    {
        SkillModel skill = InventoryManager.Instance.GetSkillById(SkillId);
        string skillText = string.Format("<size=16><color=red>武器附加技能：{0}</color></size>\n技能冷却：{4}\n技能范围：{1}\n技能消耗：{2}\n<color=yellow>技能描述：{3}</color>",skill.Name,skill.Range,skill.UseMp,skill.Description,CoolingTime);

        string color = "";
        switch (Type)
        {
            case WeaponType.MainHand:
                color = "red";
                break;
            case WeaponType.OffHand:
                color = "blue";
                break;
            case WeaponType.Foot:
                color = "green";
                break;
            case WeaponType.Head:
                color = "yellow";
                break;
        }
        string weaponText = string.Format("<size=16>{0}</size>\n<color={7}>{1}</color>\n攻击力：{2}\n攻击范围：{3}\n攻击速度：{4}\n移动速度：{9}\n<color=yellow>描述：{8}</color>\n<color=green>购买价格：{5} 出售价格：{6}</color>",Name,Type.ToString(),Atk,AtkRadius,AtkSpeed,BuyPrice,SellPrice,color,Description,MoveSpeed);

        return weaponText+"\n"+skillText;
    }
}
