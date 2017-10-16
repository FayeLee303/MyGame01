using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationView : FightingBaseView {


    [Inject]
    public MapModel map { get; set; }

    private Transform playerTransform;

    //获取横竖轴上的输入
    float h ;
    float v ;

    public void Update()
    {
        playerTransform = GameObject.Find("Player").transform;
        h = Input.GetAxis("Horizontal");//获取横竖轴上的输入
        v = Input.GetAxis("Vertical");
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
                OperationEventType = GameConfig.OperationEvent.MOVE,
                playerTransform = this.playerTransform,
                input_H = h,
                input_V = v
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
                OperationEventType = GameConfig.OperationEvent.STOP,
                playerTransform = this.playerTransform,
                input_H = 0,
                input_V = 0
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.STOP, data);
        }
        //被攻击
        if (Input.GetMouseButtonDown(0))
        {
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.BEATTACKED,
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

        //使用道具，
        //还没有做道具的冷却时间
        //TODO
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (ItemPanel.Instance.FindItemObjInSlot(0) == null) return;
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.USEITEM,
                OperationEventType = GameConfig.OperationEvent.USEITEM,
                itemObj = ItemPanel.Instance.FindItemObjInSlot(0),//第一个格子里的物品
                playerTransform = this.playerTransform
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.USEITEM, data);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (ItemPanel.Instance.FindItemObjInSlot(1) == null) return;
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.USEITEM,
                OperationEventType = GameConfig.OperationEvent.USEITEM,
                itemObj = ItemPanel.Instance.FindItemObjInSlot(1),//第二个格子里的物品
                playerTransform = this.playerTransform
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.USEITEM, data);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (ItemPanel.Instance.FindItemObjInSlot(2) == null) return;
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.USEITEM,
                OperationEventType = GameConfig.OperationEvent.USEITEM,
                itemObj = ItemPanel.Instance.FindItemObjInSlot(2),//第三个格子里的物品
                playerTransform = this.playerTransform
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.USEITEM, data);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (ItemPanel.Instance.FindItemObjInSlot(3) == null) return;
            CustomOperationEventData data = new CustomOperationEventData
            {
                type = GameConfig.OperationEvent.USEITEM,
                OperationEventType = GameConfig.OperationEvent.USEITEM,
                itemObj = ItemPanel.Instance.FindItemObjInSlot(3),//第四个格子里的物品
                playerTransform = this.playerTransform
            };
            dispatcher.Dispatch(GameConfig.OperationEvent.USEITEM, data);
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
