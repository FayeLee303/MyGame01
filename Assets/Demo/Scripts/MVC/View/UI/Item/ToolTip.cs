using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour {

    private Text toolTipText; //这个文字主要是用大小改变面板大小
    private Text contentText; //这个文字用来显示，这两个文字要一样
    private CanvasGroup canvasGroup;
    private float targetAlpha = 0;
    public float smoothingTime = 5;

    //提示板
    private Canvas canvas;
    public float toolTipPositionOffsetX = 5; //偏移量
    public float toolTipPositionOffsetZ = 5; //偏移量

    public void Start()
    {
        toolTipText = this.GetComponent<Text>();
        contentText = transform.Find("ContentText").GetComponent<Text>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothingTime*Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f) canvasGroup.alpha = targetAlpha;
        }

        //更新偏移量
        if (InventoryManager.Instance.isToolTipShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);//用out输出
            InventoryManager.Instance.toolTip.SetLocalPosition(position + new Vector2(toolTipPositionOffsetX, toolTipPositionOffsetZ));//设置局部坐标，二维向量赋值给三维向量，Z轴默认为0
        }
    }

    public void ShowToolTip(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        targetAlpha = 1;        
    }

    public void HideToolTip()
    {
        targetAlpha = 0;
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
