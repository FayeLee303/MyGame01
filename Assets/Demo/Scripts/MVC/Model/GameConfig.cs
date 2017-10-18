using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig  {

    //这里可以定义一些游戏数据
    public static int levelCount = 3;

    public enum MapSize
    {
        SMALL, //400*400
        MIDDLE, //800*800
        LARGE //1000*1000
    }

    public enum Level
    {
        LEVEL1,
        LEVEL2,
        LEVEL3
    }

    public enum LevelState
    {
        PASS,
        FAILE
    }

    public enum PropsState
    {
        STARTGAME,
        RESTART,
        ONPAUSE,
        PAUSE_RELEASE,
        RUNTIME,
    }

    public enum PlayerState
    {
        NORMAL,
        DIE,
        MOVE,
        STAND,
        FIGHT,
        TURNDIRECTION
    }

    public enum TurnDirection
    {
        LEFT,
        RIGHT
    }

    public enum CoreEvent
    {
        GAME_START,
        GAME_UPDATE,
        GAME_OVER,
        GAME_RESTART,
        LOADING,
        PAUSE,
        USER_INPUT
    }

    public enum OperationEvent
    {
        MOVE,
        STOP,
        ATTACK,
        BEATTACKED,
        PICK,
        USEITEM,
        USEWEAPON
    }

}
