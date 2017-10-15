using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class MonsterMediator : FightingBaseMediator
{
    [Inject]
    public MonsterView monsterView { get; set; }
    public override void OnRegister()
    {
        UpdateListeners(true);
    }
    public override void OnRemove()
    {
        UpdateListeners(false);
    }
    protected override void UpdateListeners(bool enable)
    {

        dispatcher.UpdateListener(enable, GameConfig.CoreEvent.GAME_UPDATE, monsterView.GameUpdate);
        dispatcher.UpdateListener(enable, GameConfig.PropsState.ONPAUSE, OnPause);
        dispatcher.UpdateListener(enable, GameConfig.PropsState.PAUSE_RELEASE, Pause_Release);
        base.UpdateListeners(enable);
    }
    private void OnPause(IEvent payload)
    {
    }
    private void Pause_Release(IEvent payload)
    {
    }

}
