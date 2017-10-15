using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

public class FightingBaseMediator : Mediator {

    [Inject(ContextKeys.CONTEXT_DISPATCHER)] //全局派发器
    public IEventDispatcher dispatcher { get; set; }

    //protected virtual 都是让子类重写的方法
    protected virtual void UpdateListeners(bool enable)
    {
        //就是说在继承自它的子类里全都会监听游戏是否开始，是否结束，是否重新开始
        dispatcher.UpdateListener(enable, GameConfig.CoreEvent.GAME_START, InitData);
        dispatcher.UpdateListener(enable, GameConfig.CoreEvent.GAME_OVER, OnGameOver);
        dispatcher.AddListener(GameConfig.CoreEvent.GAME_RESTART, OnGameRestart);
    }

    protected virtual void OnGameRestart() { UpdateListeners(true); }

    protected virtual void OnGameOver() { UpdateListeners(false); }

    protected virtual void InitData() { }
}
