using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;

public class FightingBaseView : View {

    [Inject]
    public IEventDispatcher dispatcher { get; set; } //局部派发器
    public virtual void GameUpdate() { } //让子类实现
    public virtual void Init() { } //让子类实现
}
