using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediator : FightingBaseMediator {
    [Inject]
    public PlayerView playerView { get; set; }

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
        dispatcher.UpdateListener(enable, GameConfig.PlayerState.MOVE, playerView.MoveToDirection);
        dispatcher.UpdateListener(enable, GameConfig.CoreEvent.GAME_UPDATE, playerView.GameUpdate);
        dispatcher.UpdateListener(enable, GameConfig.PropsState.ONPAUSE, OnPause);
        dispatcher.UpdateListener(enable, GameConfig.PropsState.PAUSE_RELEASE, Pause_Release);
        base.UpdateListeners(enable);
    }
    private void OnPause(IEvent payload)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void Pause_Release(IEvent payload)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    //protected override void InitData()
    //{   
    //    GetComponent<Rigidbody>().isKinematic = false;
    //}
    //protected override void OnGameRestart()
    //{
    //    InitData();
    //    GetComponent<Rigidbody>().isKinematic = false;
    //    base.OnGameRestart();
    //}
}
