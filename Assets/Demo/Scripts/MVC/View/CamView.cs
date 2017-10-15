using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class CamView : FightingBaseView
{
    private GameObject Player;
    private Quaternion _targetRotation;
    private Vector3 _rotation;
    private int _index;

    [Tooltip("镜头每次旋转的角度值，可修改数组长度和值")]
    public float[] _roteArray = new float[4] { 0, 90, 180, -90 };

    //初始化函数
    public override void Init()
    {
        Player = GameObject.Find("Player");
        _targetRotation = gameObject.transform.rotation;
    }

    public void Update()
    {
        //实现镜头跟随玩家
        this.gameObject.transform.position = Player.transform.position;

        GameUpdate();
    }

    //监听玩家的输入
    public override void GameUpdate()
    {    
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //派发镜头旋转事件
            CustomEventData data_CCW = new CustomEventData
            {
                str = "CCW"
            };
            dispatcher.Dispatch(ViewEvent.CamCounterClockwise, data_CCW);
            //执行镜头旋转
            if (_index == _roteArray.Length - 1)
                _index = 0;              
            else
                _index++;          
            _targetRotation = gameObject.transform.rotation;
            _rotation = _targetRotation.eulerAngles;
            _rotation = new Vector3(_rotation.x, _roteArray[_index], _rotation.z);
            _targetRotation = Quaternion.Euler(_rotation);
            gameObject.transform.rotation = _targetRotation;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //派发镜头旋转事件
            CustomEventData data_CW = new CustomEventData
            {
                str = "CW"
            };
            dispatcher.Dispatch(ViewEvent.CamClockwise, data_CW);
            //执行镜头旋转
            if (_index == 0)
                _index = _roteArray.Length - 1;
            else
                _index--;
            _targetRotation = gameObject.transform.rotation;
            _rotation = _targetRotation.eulerAngles;
            _rotation = new Vector3(_rotation.x, _roteArray[_index], _rotation.z);
            _targetRotation = Quaternion.Euler(_rotation);
            gameObject.transform.rotation = _targetRotation;
        }
    }
}
