using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour {

    private Text infoBoxText; //这个文字主要是用大小改变面板大小
    private Text contentText; //这个文字用来显示，这两个文字要一样
    private CanvasGroup canvasGroup;
    private float targetAlpha = 0;
    public float smoothingTime = 5;
    public float stayTime = 2;


    public void Start () {
        //初始化
        infoBoxText = this.GetComponent<Text>();
        contentText = transform.Find("ContentText").GetComponent<Text>();
        canvasGroup = this.GetComponent<CanvasGroup>();
        
    }
	
	// Update is called once per frame
	public void Update () {
	    if (canvasGroup.alpha != targetAlpha)//淡出淡入
	    {
	        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothingTime * Time.deltaTime);
	        if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f) canvasGroup.alpha = targetAlpha;
	    }
	    if (InventoryManager.Instance.isInfoBoxShow)
	    {
	        InventoryManager.Instance.infoBox.SetLocalPosition(new Vector3(0, 0, 0));
	    }
	}

    public void ShowInfoBox(string text)
    {
        infoBoxText.text = text;
        contentText.text = text;
        targetAlpha = 1;
        this.Invoke("HideInfoBox", stayTime);//Invoke函数只存在于继承monobehavior的类
    }

    public void HideInfoBox()
    {
        InventoryManager.Instance.isInfoBoxShow = false;
        targetAlpha = 0;
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
}
