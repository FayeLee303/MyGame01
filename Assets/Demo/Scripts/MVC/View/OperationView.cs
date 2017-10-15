using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationView : FightingBaseView {


    [Inject]
    public MapModel map { get; set; }

    public void Update()
    {
        GameUpdate();
        UIPanelUpdate();
        Test();
    }

    //监听玩家对角色的输入
    public override void GameUpdate()
    {
        //移动
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.MOVE,
                ismoving = true,
                OperationEventType = GameConfig.OperationEvent.MOVE
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.MOVE, data);
        }
        //停止
        if (Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S))
        {
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.STOP,
                ismoving = false,
                OperationEventType = GameConfig.OperationEvent.STOP
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.STOP, data);
        }
        //被攻击
        if (Input.GetMouseButtonDown(0))
        {
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.BEATTACKED,
                ismoving = false,
                OperationEventType = GameConfig.OperationEvent.BEATTACKED
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.BEATTACKED, data);
        }     
    }

    //更新UI面板，主要是键盘按键，UIPanle的预制体里自己写了鼠标点击屏幕的事件
    public void UIPanelUpdate()
    {
        //地图面板
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //先检查当前的栈里有没有这个界面
            if (UIPanelManager.Instance.FindPanle("MiniMapPanel"))
            {
                //有就出栈
                UIPanelManager.Instance.PopPanel();
                //Debug.Log("出栈");
            }
            else if (!UIPanelManager.Instance.FindPanle("MiniMapPanel"))
            {
                //没有就入栈
                //转成枚举类型
                UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), "MiniMapPanel");
                UIPanelManager.Instance.PushPanel(panelType); //推送到顶端
                //Debug.Log("入栈");
            }
        }      
    }

    public void Test()
    {
        //道具面板
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 3);
            ItemPanel.Instance.StoreItem(id);
        }
        //武器面板
        if (Input.GetKeyDown(KeyCode.H))
        {
            int id = Random.Range(1, 4);
            WeaponPanel.Instance.StoreWeapon(id);
        }
        //掉落物品
        if (Input.GetKeyDown(KeyCode.O))
        {
            int id = Random.Range(1, 3);
            Vector3 pos = new Vector3(Random.Range(-5, 5), 3, Random.Range(-5, 5));
            InventoryManager.Instance.InstantiateItemObj3D(id, pos);
        }
    }
}
