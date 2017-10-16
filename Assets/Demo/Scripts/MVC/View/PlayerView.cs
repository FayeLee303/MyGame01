using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : FightingBaseView
{
    //Gui组件
    private Image hpSlider;
    private Text hpText;
    private Text hpMaxText;
    private Image mpSlider;
    private Text mpText;
    private Text mpMaxText;
    private Text Atk;
    private Text Def;
    private Text moveSpeed;
    private Text money;
    

    //重写父类里的Start函数，在这里初始化
    protected override void Start()
    {

        hpSlider = GameObject.Find("RoleHp").GetComponent<Image>();
        hpText = GameObject.Find("HpText").GetComponent<Text>();
        hpMaxText = GameObject.Find("maxHp").GetComponent<Text>();

        mpSlider = GameObject.Find("RoleMp").GetComponent<Image>();
        mpText = GameObject.Find("MpText").GetComponent<Text>();
        mpMaxText = GameObject.Find("maxMp").GetComponent<Text>();

        Atk = GameObject.Find("AtkNum").GetComponent<Text>();
        Def = GameObject.Find("DefNum").GetComponent<Text>();
        moveSpeed = GameObject.Find("MoveSpeedNum").GetComponent<Text>();
        money = GameObject.Find("MoneyNum").GetComponent<Text>();
        base.Start();
    }

    public void Update()
    {
        GameUpdate();
    }

    public override void GameUpdate()
    {
        //更新UI
        hpSlider.fillAmount = (float)DataBaseManager.Instance.FindRole(0).Hp / (float)DataBaseManager.Instance.FindRole(0).MaxHp;
        hpText.text = DataBaseManager.Instance.FindRole(0).Hp.ToString();
        hpMaxText.text = DataBaseManager.Instance.FindRole(0).MaxHp.ToString();

        mpSlider.fillAmount = (float)DataBaseManager.Instance.FindRole(0).Mp / (float)DataBaseManager.Instance.FindRole(0).MaxMp;
        mpText.text = DataBaseManager.Instance.FindRole(0).Mp.ToString();
        mpMaxText.text = DataBaseManager.Instance.FindRole(0).MaxMp.ToString();

        Atk.text = DataBaseManager.Instance.FindRole(0).Atk.ToString();
        Def.text = DataBaseManager.Instance.FindRole(0).Def.ToString();
        moveSpeed.text = DataBaseManager.Instance.FindRole(0).MoveSpeed.ToString();
        money.text = DataBaseManager.Instance.FindRole(0).Money.ToString();
    }

    //攻击流程思路
    //【框架处理消息传递和数据加减，PlayMaker处理怪物自己的行动逻辑和命中，命中事件是PlayMaker分别发给玩家View和怪物View的】
    //玩家进入任何一个怪物的视野中时，怪物给玩家发消息，玩家进入战斗状态，并且这个怪物也进入战斗状态
    //然后玩家对所有进入战斗状态的怪物进行遍历，哪个离他最近，哪个就是当前玩家正在攻击的怪物
    //当玩家发动攻击并命中，给这个正在被玩家攻击的怪物发被攻击事件和玩家的属性
    //被攻击的怪物处理被攻击事件
    //当任意一个怪物发动攻击并命中时，给玩家发消息，把被攻击事件和该怪物的属性一起发给玩家
    //玩家自身处理被攻击事件，先看是否有Buff，然后生命值减等于攻击力
}
