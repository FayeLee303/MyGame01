using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MiniMapPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private Camera mapCam;//拍小地图的摄像机
    private Texture2D mapTex;//摄像机的渲染纹理转成的Texture2D形式
    //private string filePath; //存临时地图的路径
    private Image mapImg;

    public int width = 900;
    public int height = 450;


    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
        //初始化
        mapCam = GameObject.Find("MapCam").GetComponent<Camera>();
        this.mapImg = GameObject.Find("MiniMap").GetComponent<Image>();
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
        transform.DOLocalMoveX(1000, 0.5f).OnComplete(() => canvasGroup.alpha = 0);

    }
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
        //截图并赋值
        mapTex = CaptureCamera(mapCam, new Rect(0, 0, width, height));
        Sprite sp = Sprite.Create(mapTex, new Rect(0, 0, mapTex.width, mapTex.height), new Vector2(0.5f, 0.5f));
        mapImg.sprite = sp;
        //渐入动画
        Vector3 temp = this.transform.localPosition;
        temp.x = 1000;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, 0.5f);
    }


    private Texture2D CaptureCamera(Camera camera, Rect rect)
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 0);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = rt;
        camera.Render();
        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);
        return screenShot;
    }
}
