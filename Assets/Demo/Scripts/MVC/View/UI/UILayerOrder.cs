using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用来控制不同UI的层级显示
/// </summary>
public class UILayerOrder : MonoBehaviour {
    //主面板在最底层
    //如果有系统菜单就在最顶层
    //如果没有系统菜单只有小地图，就小地图就在最顶层
    //如果没有小地图就ToolTip在最顶层
    //PickObj在ToolTip的下面
    // Use this for initialization

    private Transform mainMenuPanel; //主界面
    private Transform normalMenuPanel; //系统菜单1
    private Transform optionPanel; //系统菜单2
    private Transform miniMapPanel; //小地图菜单
    private Transform toolTip; //提示页面
    private Transform pickedItem; //捡起物品
    private Transform pickedWeapon;//捡起武器
    private Transform infoBox; //信息框

    void Start ()
    {
        mainMenuPanel = GameObject.FindObjectOfType<MainMenuPanel>().transform;
        toolTip = InventoryManager.Instance.toolTip.transform;
        pickedItem = InventoryManager.Instance.PickedItem.transform;
        pickedWeapon = InventoryManager.Instance.PickedWeapon.transform;
        infoBox = InventoryManager.Instance.infoBox.transform;
        //把主界面放在最下面，把tooltip和pickedItem放在主界面下面
        mainMenuPanel.SetAsFirstSibling();
        toolTip.SetParent(mainMenuPanel);
        pickedItem.SetParent(mainMenuPanel);
        pickedWeapon.SetParent(mainMenuPanel);
        infoBox.SetParent(mainMenuPanel);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    
	    ////一直把PickedItem放在画布的ToolTip的下面
	    //pickedItem.SetSiblingIndex(toolTip.GetSiblingIndex());
	}
}
