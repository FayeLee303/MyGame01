using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillModel {
    
    public int Id { get; set; }//技能编号
    public string Name { get; set; }//技能名
    public int Damage { get; set; }//技能伤害
    public string Description { get; set; }//描述
    public List<BuffModel> Buff { get; set; }//附加buff
    public int AnimationId { get; set; }//技能动画编号
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="damage"></param>
    /// <param name="buff"></param>
    /// <param name="animationID"></param>
    public SkillModel(int id,string name,int damage,List<BuffModel> buff,int animationID)
    {
        this.Id = id;
        this.Name = name;
        this.Damage = damage;
        this.Buff = buff;
        this.AnimationId = animationID;
    }
}
