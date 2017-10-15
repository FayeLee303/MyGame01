using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//包括buff和debuff，以及boss的狂暴效果等
public class BuffModel
{
    public int Id { get; set; } //buff编号
    public string Name { get; set; } //buff类型
    public int LastTime { get; set; } //持续时间
    public string Description { get; set; } //描述
    public int HpChange { get; set; } //血量变化
    public int MpChange { get; set; } //Mp变化
    public int MaxHpChange { get; set; }//血量上限变化
    public int MaxMpChange { get; set; }//Mp上限变化
    public int AtkChange { get; set; } //攻击力变化
    public int DefChange { get; set; } //防御力变化
    public int MoveSpeedChange { get; set; } //移动速度变化
    public int AtkSpeedChange { get; set; } //攻击速度变化
    public int HpRecoverRateChange { get; set; } //回血速率变化
    public int MpRecoverRateChange { get; set; } //回蓝速率变化
    public bool WeaponForbidden { get; set; } //是否禁用武器
    public bool ItemForbidden { get; set; } //是否禁用道具
    public int AnimationId { get; set; } //播放动画的编号，可以是player的动画或者Boss的动画

    ////构造函数
    //public BuffModel(int id,string name,int lastTime,int hpChange,int mpChange,int maxHpChange,int maxMpChange,int atkChange,int defChange,int moveSpeedChange,int atkSpeedChange,int hpRecoverRateChange,int mpRecoverRateChange,bool weapomForbidden,bool itemForbidden,int aniid,RoleModel role)
    //{
    //    this.Id = id;
    //    this.Name = name;
    //    this.LastTime = lastTime;
    //    this.HpChange = hpChange;
    //    this.MpChange = mpChange;
    //    this.MaxHpChange = maxHpChange;
    //    this.MaxMpChange = maxMpChange;
    //    this.AtkChange = atkChange;
    //    this.DefChange = defChange;
    //    this.MoveSpeedChange = moveSpeedChange;
    //    this.AtkSpeedChange = atkSpeedChange;
    //    this.HpRecoverRateChange = hpRecoverRateChange;
    //    this.MpRecoverRateChange = mpRecoverRateChange;
    //    this.WeaponForbidden = weapomForbidden;
    //    this.ItemForbidden = itemForbidden;
    //    this.AnimationId = aniid;
    //}

    //public void RoleStateChangeAndRecover(RoleModel role)
    //{
    //    RoleModel originalRoleState = role;//角色初始状态
    //    float timer = 0f;
    //    timer = Time.deltaTime;
    //    if (timer <= this.LastTime)
    //    {
    //        //改变属性，播放动画
    //        role.Hp += this.HpChange;
    //        role.Mp += this.MpChange;
    //        role.MaxHp += this.MaxHpChange;
    //        role.MaxMp += this.MaxMpChange;
    //        role.Atk += this.AtkChange;
    //        role.Def += this.DefChange;
    //        role.MoveSpeed += this.MoveSpeedChange;
    //        role.AtkSpeed += this.AtkSpeedChange;
    //        role.HpRecoveryRate += this.HpRecoverRateChange;
    //        role.MpRecoveryRate += this.MpRecoverRateChange;
    //        role.IsWeaponForbidden = this.WeaponForbidden;
    //        role.IsItemForbodden = this.ItemForbidden;
    //        role.AnimationId = this.AnimationId;
    //        role.PlayAnimation(role.AnimationId);//播放动画

    //    }
    //    else if(timer>this.LastTime)
    //    {
    //        timer = 0;//计时器归零
    //        role = originalRoleState;//玩家状态恢复
    //    }
    //}
}

