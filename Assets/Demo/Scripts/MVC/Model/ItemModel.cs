using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel
{
  
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string Description { get; set; }
    public int MaxLimit { get; set; }
    public int RecoverHp { get; set; }//回血
    public int RecoverMp { get; set; }//回蓝
    public int Atk { get; set; }//武器攻击力加成
    public int Def { get; set; }//防御力加成
    public int AtkSpeed { get; set; }//攻击速度加成
    public int MoveSpeed { get; set; }//移动速度加成
    public int BuyPrice { get; set; }//买入价格
    public int SellPrice { get; set; }//卖出价格
    public string SpritePath { get; set; } //图片路径
    public int EffectId { get; set; }//使用时会产生的特效
    public float CoolingTime { get; set; }//冷却时间

    public enum ItemType
    {
        Consumable = 0,//消耗品
        Active = 1, //主动使用道具
        Normal = 2 //不能使用的
    }

    public override string ToString()
    {
        return string.Format("Id: {0}, Name: {1}, Type:{2}, Description: {3}, MaxLimit: {4}, SpritePath: {5}", Id, Name,Type, Description, MaxLimit, SpritePath);
    }

    //得到显示面板应该显示的内容
    public virtual string GetToolTipText()
    {
        string color = "";
        switch (Type)
        {
            case ItemType.Consumable:
                color = "red";
                break;
            case ItemType.Active:
                color = "blue";
                break;
        }
        string text = string.Format("<size=16>{0}</size>\n<color={5}>{1}</color>\n<color=yellow>描述：{2}</color>\n<color=green>购买价格：{3} 出售价格：{4}</color>", Name, Type.ToString(), Description, BuyPrice,
            SellPrice,color);
        
        return text;
    }
}
