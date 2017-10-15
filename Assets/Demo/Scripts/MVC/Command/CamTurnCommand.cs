using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTurnCommand : EventCommand{

    [Inject]
    public MapModel map { get; set; }

    /// <summary>
    /// 这个脚本用于更改map的数据，不作回调
    /// </summary>
    public override void Execute()
    {
        var cd = evt as CustomEventData;
        if(cd.str == "CCW")
        {
            moveRight(map.DirArray);
            moveRight(map.MapDir);
        }
        if (cd.str == "CW")
        {
            moveLeft(map.DirArray);
            moveLeft(map.MapDir);
        }
    }

    //数组元素逆时针移动
    void moveRight(string[] Arr)
    {
        //保存下数组的最后一个数 
        string endNum = Arr[3];
        //将0~2位数向后移动一位
        int i;
        for (i = 3; i > 0; i--)
        {
            Arr[i] = Arr[i - 1];
        }
        //将最后一位数放在第一位
        Arr[0] = endNum;
    }

    //数组元素顺时针移动
    void moveLeft(string[] Arr)
    {
        //保存下数组的第一个数 
        string firstNum = Arr[0];
        //将1~3位数向前移动一位
        int i;
        for (i = 0; i < 3; i++)
        {
            Arr[i] = Arr[i + 1];
        }
        //将第一位数放在最后一位
        Arr[3] = firstNum;
    }
}
