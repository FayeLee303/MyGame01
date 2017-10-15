using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleModel {

    public int Hp { get; set; }//生命值
    public int Mp { get; set; }//法力值
    public int MaxHp { get; set; }//生命值上限
    public int MaxMp { get; set; }//Mp上限
    public int Atk { get; set; }//攻击力
    public int Def { get; set; }//防御力
    public int MoveSpeed { get; set; }//移动速度
    public int AtkSpeed { get; set; }//攻击速度
    public int HpRecoveryRate { get; set; }//生命恢复速率
    public int MpRecoveryRate { get; set; }//法力恢复速率
    public List<WeaponModel> Weapon { get; set; } //武器
    public List<ItemModel> ItemID { get; set; }//道具
    public bool IsWeaponForbidden { get; set; }//是否禁用武器
    public bool IsItemForbidden { get; set; }//是否禁用道具
    public int AnimationId { get; set; }//角色动画编号
    public Direction RoleDir { get; set; }//角色方向
    public int ID { get; set; }//角色ID

    public enum Direction{
        None = 0,
        North = 1,
        South = 2,
        West = 3,
        East = 4,
        NorthWest = 5,
        NprthEast = 6,
        SouthWset = 7,
        SouthEast = 8
    }
    ///// <summary>
    ///// 构造方法
    ///// </summary>
    ///// <param name="hp"></param>
    ///// <param name="mp"></param>
    ///// <param name="maxhp"></param>
    ///// <param name="maxmp"></param>
    ///// <param name="atk"></param>
    ///// <param name="def"></param>
    ///// <param name="movespeed"></param>
    ///// <param name="atkspeed"></param>
    ///// <param name="hprecoverrate"></param>
    ///// <param name="mprecoverrate"></param>
    ///// <param name="weaponList"></param>
    ///// <param name="itemList"></param>
    ///// <param name="isweaponforbidden"></param>
    ///// <param name="isitemforbidden"></param>
    ///// <param name="aniid"></param>
    //public RoleModel(int hp,int mp,int maxhp,int maxmp,int atk,int def,int movespeed,int atkspeed,int hprecoverrate,int mprecoverrate,List<WeaponModel> weaponList,List<ItemModel> itemList,bool isweaponforbidden,bool isitemforbidden,int aniid,Direction roleDir)
    //{
    //    this.Hp = hp;
    //    this.Mp = mp;
    //    this.MaxHp = maxhp;
    //    this.MaxMp = maxmp;
    //    this.Atk = atk;
    //    this.Def = def;
    //    this.MoveSpeed = movespeed;
    //    this.AtkSpeed = atkspeed;
    //    this.HpRecoveryRate = hprecoverrate;
    //    this.MpRecoveryRate = mprecoverrate;
    //    this.Weapon = weaponList;
    //    this.Item = itemList;
    //    this.IsWeaponForbidden = isweaponforbidden;
    //    this.IsItemForbodden = isitemforbidden;
    //    this.AnimationId = aniid;
    //    this.RoleDir = roleDir;
    //}

    //public RoleModel(Direction dir)
    //{
    //    this.RoleDir = dir;
    //}

    //public virtual void PlayAnimation(int animationId)
    //{
    //    animationId = this.AnimationId;
    //    //根据动画Id播放动画
    //}

    public override string ToString()
    {
        return string.Format("Hp:{0},Mp:{1},MaxHp:{2},MaxMp:{3},Atk:{4},Def:{5},MoveSpeed:{6},AtkSpeed:{7},HpRecoveryRate:{8},MpRecoveryRate:{9},Weapon:{10},ItemID:{11},IsWeaponForbidden:{12},IsItemForbidden:{13},AnimationId:{14},RoleDir:{15},ID:{16}",
            Hp,Mp,MaxHp,MaxMp,Atk,Def,MoveSpeed,AtkSpeed,HpRecoveryRate,MpRecoveryRate,Weapon,ItemID,IsWeaponForbidden,IsWeaponForbidden,AnimationId,RoleDir,ID);
    }
}
