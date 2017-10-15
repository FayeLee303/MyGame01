using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    //public Direction MapNowDir { get; set; }//地图当前方向
    //public int ID { get; set; }//地图ID
    public string[] MapDir = new string[4] { "North", "West", "South", "East" };//地图方向
    public  string[] DirArray = new string[4] { "W", "A", "S", "D" };

    //public enum Direction
    //{
    //    None = 0,
    //    North = 1,
    //    South = 2,
    //    West = 3,
    //    East = 4,
    //    NorthWest = 5,
    //    NprthEast = 6,
    //    SouthWset = 7,
    //    SouthEast = 8
    //}
}
