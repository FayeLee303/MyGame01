using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.mediation.api;
using UnityEngine;

public class Demo_RootContextView : ContextView
{
    private void Awake()
    {
        context = new Demo_MVCSContext(this);//启动StrangeIoc框架       
    }
}
