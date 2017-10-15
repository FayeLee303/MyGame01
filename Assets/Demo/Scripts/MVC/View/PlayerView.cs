using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : FightingBaseView
{
    [Inject]
    public MapModel map { get; set; }

    private bool moving;

    //Gui组件
    private Image hpSlider;
    private Text hpText;

    //重写父类里的Start函数，在这里初始化
    protected override void Start()
    {
        if (hpSlider == null || hpText == null)
        {
            hpSlider = GameObject.Find("RoleHp").GetComponent<Image>();//初始化
            hpText = GameObject.Find("HpText").GetComponent<Text>();
            if (hpSlider == null || hpText == null)
            {
                Debug.Log("hpSlider或者hpText没找到");
            }
        }
        base.Start();
    }

    public void Update()
    {
        GameUpdate();
        //Debug.Log(DataBaseManager.Instance.FindRole(0).Atk);
    }

    public override void GameUpdate()
    {
        Move();
        //更新UI
        hpSlider.fillAmount = DataBaseManager.Instance.FindRole(0).Hp / DataBaseManager.Instance.FindRole(0).MaxHp;
        hpText.text = DataBaseManager.Instance.FindRole(0).Hp.ToString();
        //Debug.Log(hpSlider.fillAmount);
        //Debug.Log(DataBaseManager.Instance.FindRole(0).Hp);
        //Debug.Log(DataBaseManager.Instance.FindRole(0).MaxHp);
    }


    public void MoveToDirection(IEvent e)
    {
        var cd = e as CustomOperationEventData;
        moving = cd.ismoving;
    }
    
    //真正的移动代码
    public void Move()
    {
        //在这里更新速度
        if (moving)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            for (int i = 0; i < 4; i++)
            {
                if (map.MapDir[i] == "North")
                {
                    if (i == 0)
                    {
                        break;
                    }
                    if (i == 1)
                    {
                        float temp;
                        temp = v;
                        v = -h;
                        h = temp;
                        break;
                    }
                    if (i == 2)
                    {
                        h = -h;
                        v = -v;
                        break;
                    }
                    if (i == 3)
                    {
                        float temp;
                        temp = h;
                        h = -v;
                        v = temp;
                        break;
                    }
                }
            }

            GetComponent<CharacterController>().SimpleMove(new Vector3(h * 5, 0, v * 5));
            //GetComponent<CharacterController>().SimpleMove(new Vector3(h * role.MoveSpeed, 0, v * role.MoveSpeed));

        }
        if (!moving)
        {
            GetComponent<CharacterController>().SimpleMove(new Vector3(0, 0, 0));
        }

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
