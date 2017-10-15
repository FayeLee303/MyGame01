using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : BasePanel
{

    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void OnPushPanel(string panelTypeString)
    {
        //转成枚举类型
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIPanelManager.Instance.PushPanel(panelType);
    }

    public void OnClosePanel()
    {
        UIPanelManager.Instance.PopPanel();
    }

    public override void OnExit()
    {
        //关闭显示
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        //开启显示
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    } 

    public override void OnEnter()
    {
        //开启显示
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
