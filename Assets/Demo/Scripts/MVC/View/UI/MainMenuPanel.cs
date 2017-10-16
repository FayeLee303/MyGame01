using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuPanel : BasePanel, IPointerDownHandler
{
#region 主界面UI显示
    //使用Awake来获取组件是最快的
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
       
    }
    public void OnPushPanel(string panelTypeString)
    {
        //转成枚举类型
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIPanelManager.Instance.PushPanel(panelType);
    }
    //暂停交互
    public override void OnPause()
    {
        //当弹出新的面板时，让主菜单不再和鼠标交互
        canvasGroup.blocksRaycasts = false;
    }
    //恢复交互
    public override void OnResume()
    {
        //开启显示  
        canvasGroup.blocksRaycasts = true;
    }
    public override void OnEnter()
    {
        //开启显示
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    #endregion

    #region 丢弃操作
    private Transform playerTransform;


    public void Start()
    {

    }
    public void Update()
    {
        playerTransform = GameObject.Find("Player").transform;//获取玩家的位置
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (InventoryManager.Instance.IsPickedItem == true)
        {
            if (Input.GetKey(KeyCode.LeftControl)) //按下Ctrl一次丢一个
            {
                InventoryManager.Instance.DripItem(1,playerTransform.position);
            }
            else//全部丢弃
            {
                InventoryManager.Instance.DripItem(InventoryManager.Instance.PickedItem.Amount, playerTransform.position);
            }
        }
        else if (InventoryManager.Instance.IsPickedWeapon == true)
        {
            InventoryManager.Instance.DripWeapon( playerTransform.position);
        }
        else
        {
            return;
        }

    }
#endregion
}
