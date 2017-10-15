using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class OperationCommand : EventCommand {

    public override void Execute()
    {
        var cd = evt as CustomOperationEventData;
        if (cd.OperationEventType == GameConfig.OperationEvent.BEATTACKED) {
            DataBaseManager.Instance.FindRole(0).Hp -= 1;
        }

        if (cd.OperationEventType == GameConfig.OperationEvent.MOVE || cd.OperationEventType == GameConfig.OperationEvent.STOP)
        {
            dispatcher.Dispatch(GameConfig.PlayerState.MOVE, cd);
        }
        else if (cd.OperationEventType == GameConfig.OperationEvent.ATTACK)
        {
            dispatcher.Dispatch(GameConfig.PlayerState.FIGHT);
        }
        else if (cd.OperationEventType == GameConfig.OperationEvent.PICK)
        {
            //
        }
        else if (cd.OperationEventType == GameConfig.OperationEvent.TURN_LEFT ||
                 cd.OperationEventType == GameConfig.OperationEvent.TURN_RIGHT)
        {
            dispatcher.Dispatch(GameConfig.PlayerState.TURNDIRECTION, cd.turnDir);
        }
        else
        {
            Debug.Log("失败");
        }

    }
}
