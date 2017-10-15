using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class UIPanelManager
{
    /// <summary>
    /// 单例模式的核心
    /// 定义一个静态对象，在外界访问，在内部访问
    /// 构造方法私有化，保证只能在内部构造
    /// 做成单例模式的类绝对不能继承monobehaver!!!!!!!
    /// </summary>
    private static UIPanelManager _instance;

    public static UIPanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = GameObject.Find("UIPanelManager").GetComponent<UIPanelManager>();
                _instance = new UIPanelManager(); //相当于调用下面的私有构造方法
            }
            return _instance;
        }
    }

    //画布
    private Transform canvasTransform;

    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    //用字典储存所有面板prefab的路径
    private Dictionary<UIPanelType, string> panelPathDic;

    //用字典储存所有实例化的面板的游戏物体上的BasePanel组件
    private Dictionary<UIPanelType, BasePanel> panelDic;

    //BasePanel类型的栈，后进先出
    private Stack<BasePanel> panelStack;

    //私有的构造方法
    private UIPanelManager()
    {
        ParseUIPanelTypeJson();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //Do nothing

    }

    /// <summary>
    /// 解析Json文件
    /// </summary>
    private void ParseUIPanelTypeJson()
    {
        panelPathDic = new Dictionary<UIPanelType, string>(); //把字典置空
        TextAsset ta = Resources.Load<TextAsset>("Localization/UIPanelType");
        if (ta != null)
        {
            //解析Json文件的两种方法
            List<UIPanelInfo> panelInfoList = JsonMapper.ToObject<List<UIPanelInfo>>(ta.text);
            //List<UIPanelInfo> panelInfoList = JsonUtility.FromJson<List<UIPanelInfo>>(ta.text);
            foreach (UIPanelInfo info in panelInfoList)
            {
                //存到字典里
                panelPathDic.Add(info.PanelType, info.path);
            }
        }
        else
        {
            Debug.Log("读取文件失败");
        }
        //string path;
        //panelPathDic.TryGetValue(UIPanelType.ItemPanel, out path);
        //Debug.Log(path);
        ////Debug.Log("555555");
    }

    /// <summary>
    /// 根据面板类型，得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {

        if (panelDic == null)
        {
            panelDic = new Dictionary<UIPanelType, BasePanel>(); //创建空字典

        }
        //根据传过来的面板类型去面板字典里找
        //BasePanel panel;
        //panelDic.TryGetValue(panelType, out panel);
        BasePanel panel = panelDic.TryGet(panelType);
        if (panel == null)
        {
            //如果找不到，就在面板路径字典里找prefab的路径
            //string path = null;
            //panelPathDic.TryGetValue(panelType, out path);//TODO
            string path = panelPathDic.TryGet(panelType);
            //根据prefab的路径实例化物体
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            //放到画布下面,false表示保持局部位置
            instPanel.transform.SetParent(CanvasTransform, false);
            panelDic.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

    /// <summary>
    /// 入栈，其实就是把某个页面显示在界面上
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
        //先检查栈是不是空的
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //判断一下栈里是否有页面
        if (panelStack.Count > 0)
        {
            //取出栈顶页面，并不删除,让他暂停
            BasePanel topPanel = panelStack.Peek();
            Debug.Log(topPanel.name);//TODO
            topPanel.OnPause();
        }
        //然后再入栈新的页面
        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        //panel.OnResume();
        panelStack.Push(panel);
        //Debug.Log(panelStack.Count);
    }

    /// <summary>
    /// 出栈，把某个页面从界面上隐藏或删除
    /// </summary>
    public void PopPanel()
    {
        //先检查栈是不是空的
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //判断一下栈里是否有页面,如果没有的话直接return
        if (panelStack.Count < 0) return;
        //如果count>0，就取出栈顶元素,并移除
        //关闭栈顶页面的显示，具体的关闭方法在该页面对应的函数里重构
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();
        //判断下面有没有页面，如果有的话就把这个页面的Pause关掉
        //让这个页面可以正常交互
        if (panelStack.Count <= 0) return; 
        BasePanel topPanel_2 = panelStack.Peek();
        topPanel_2.OnResume();
    }

    //检查某个面板是否在栈里
    public bool FindPanle(string panelNameString)
    {
        //转成枚举类型
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), "MiniMapPanel");
        //先检查栈是不是空的
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        //判断一下栈里是否有页面,如果没有的话直接return
        if (panelStack.Count <= 0) return false;
        else
        {
            BasePanel panel = GetPanel(panelType);
            foreach (BasePanel p in panelStack)
            {
                if (p.Equals(panel))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
