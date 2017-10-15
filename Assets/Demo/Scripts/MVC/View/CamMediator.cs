using strange.extensions.dispatcher.eventdispatcher.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMediator : FightingBaseMediator {

    [Inject]
    public CamView cam { get; set; }

    public override void OnRegister()
    {
        //执行CamView的初始化
        cam.Init();

        UpdateListeners(true);
    }
    public override void OnRemove()
    {
        UpdateListeners(false);
    }
    //监听器
    protected override void UpdateListeners(bool enable)
    {
        //这里写CamView的监听器
        cam.dispatcher.UpdateListener(enable, ViewEvent.CamClockwise, Camclockwise);
        cam.dispatcher.UpdateListener(enable, ViewEvent.CamCounterClockwise, Camcounterclockwise);

        //这里写Command的监听器

        //base.UpdateListeners(enable)不能删除，因为要继承父类的监听器
        base.UpdateListeners(enable);
    }
   
    void Camclockwise(IEvent e)
    {
        CustomEventData data = e as CustomEventData;
        dispatcher.Dispatch(CommandEvent.CamCW, data);
    }

    void Camcounterclockwise(IEvent e)
    {
        CustomEventData data = e as CustomEventData;
        dispatcher.Dispatch(CommandEvent.CamCCW, data);
    }
}
