using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour {
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        
    }
    /// <summary>
    /// 界面暂停，或弹出其他页面
    /// </summary>
    public virtual void OnPause()
    {
        
    }
    /// <summary>
    /// 界面恢复，或其他页面被移除
    /// </summary>
    public virtual void OnResume()
    {
        
    }
    /// <summary>
    /// 页面被移除，不再显示
    /// </summary>
    public virtual void OnExit()
    {
        
    }
}
