using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NormalMenuPanel : BasePanel {

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
    //暂停
    public override void OnPause()
    {
        //当弹出新的面板时，让主菜单不再和鼠标交互
        canvasGroup.blocksRaycasts = false;
    }
    //关闭
    public void OnClosePanel()
    {
        UIPanelManager.Instance.PopPanel();
    }

    public override void OnExit()
    {
        //关闭显示
        //canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        //表示动画播放完之后调用的语句
        transform.DOLocalMoveX(1000, 0.5f).OnComplete(()=>canvasGroup.alpha = 0);
;    }
    //恢复交互
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
        //渐入动画
        Vector3 temp = this.transform.localPosition;
        temp.x = 1000;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0,0.5f);
    }
}
