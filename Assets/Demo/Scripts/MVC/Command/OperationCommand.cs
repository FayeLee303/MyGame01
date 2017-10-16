using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class OperationCommand : EventCommand {

    [Inject]
    public MapModel map { get; set; }

    public override void Execute()
    {
        var cd = evt as CustomOperationEventData;

        if (cd.OperationEventType == GameConfig.OperationEvent.BEATTACKED) {
            DataBaseManager.Instance.FindRole(0).Hp -= 1;
        }
        else if (cd.OperationEventType == GameConfig.OperationEvent.MOVE || cd.OperationEventType == GameConfig.OperationEvent.STOP)
        {
            if (cd.ismoving)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (map.MapDir[i] == "North")
                    {
                        if (i == 0)
                        {
                            break;
                        }
                        if (i == 1)
                        {
                            float temp;
                            temp = cd.input_V;
                            cd.input_V = -cd.input_H;
                            cd.input_H = temp;
                            break;
                        }
                        if (i == 2)
                        {
                            cd.input_H = -cd.input_H;
                            cd.input_V = -cd.input_V;
                            break;
                        }
                        if (i == 3)
                        {
                            float temp;
                            temp = cd.input_H;
                            cd.input_H = -cd.input_V;
                            cd.input_V = temp;
                            break;
                        }
                    }
                }
                cd.playerTransform.GetComponent<CharacterController>().SimpleMove(
                    new Vector3(cd.input_H * DataBaseManager.Instance.FindRole(0).MoveSpeed, 0,
                        cd.input_V * DataBaseManager.Instance.FindRole(0).MoveSpeed));
            }
            else
            {
                cd.playerTransform.GetComponent<CharacterController>().SimpleMove(new Vector3(0, 0, 0));
            }
        }
        else if (cd.OperationEventType == GameConfig.OperationEvent.ATTACK)
        {
            //dispatcher.Dispatch(GameConfig.PlayerState.FIGHT);
        }
        else if (cd.OperationEventType == GameConfig.OperationEvent.PICK)
        {
        }else if (cd.OperationEventType == GameConfig.OperationEvent.USEITEM)
        {
            ItemObj itemObj = cd.itemObj;
            itemObj.EffectOnRole(itemObj, DataBaseManager.Instance.FindRole(0), cd.playerTransform);
        }

    }
}
