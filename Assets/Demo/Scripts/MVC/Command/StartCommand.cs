using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using Sirenix.OdinInspector.Demos;
using UnityEngine;

public class StartCommand : EventCommand {

    [Inject]
    public MapModel map { get; set; }

    [Inject]
    public RoleModel role { get; set; }

    //开始命令，用来做一些初始化工作，框架外的东西也可以在这里Init
    //当这个命令执行时，默认调用Execute方法
    public override void Execute()
    {
        Debug.Log("StartCommand Execute");

        map.MapDir = new string[4] { "North", "West", "South", "East" };
        map.DirArray = new string[4] { "W", "A", "S", "D" };

        //生成地图
       //MapGenerator mapGenerator = GameObject.Find("Edge").GetComponent<MapGenerator>();
        ObjectPlacer objGenerator = GameObject.Find("Element").GetComponent<ObjectPlacer>();
        //mapGenerator.GenerateMap();
        objGenerator.ClearAndRepositionObjects();

        //派发游戏开始事件
        dispatcher.Dispatch(GameConfig.CoreEvent.GAME_START);

        DataBaseManager.Instance.Init();//初始化并读取数据
        InventoryManager.Instance.Init();
       
        UIPanelManager.Instance.Init();
        UIPanelManager.Instance.PushPanel(UIPanelType.MainMenuPanel);
    }
}
