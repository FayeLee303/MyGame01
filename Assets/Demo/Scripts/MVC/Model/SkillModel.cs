using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel {
    
    public int Id { get; set; }//技能编号
    public string Name { get; set; }//技能名
    public float CoolingTime { get; set; }//冷却时间
    public float Range { get; set; }//技能范围
    public int Damage { get; set; }//技能伤害
    public int UseHp { get; set; }//技能消耗HP，有的耗血
    public int UseMp { get; set; }//技能消耗MP
    public string Description { get; set; }//描述
    public List<BuffModel> Buff { get; set; }//附加buff
    public int AnimationId { get; set; }//技能动画编号
    public string SpritePath { get; set; }//图片位置
    ///// <summary>
    ///// 构造函数
    ///// </summary>
    ///// <param name="id"></param>
    ///// <param name="name"></param>
    ///// <param name="damage"></param>
    ///// <param name="buff"></param>
    ///// <param name="animationID"></param>
    //public SkillModel(int id,string name,int damage,List<BuffModel> buff,int animationID)
    //{
    //    this.Id = id;
    //    this.Name = name;
    //    this.Damage = damage;
    //    this.Buff = buff;
    //    this.AnimationId = animationID;
    //}
}
