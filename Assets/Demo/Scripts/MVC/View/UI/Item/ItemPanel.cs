using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanel : Inventory
{

    #region
    private static ItemPanel _instance;
    public static ItemPanel Instance {
        get {
            if(_instance == null)
            {
                _instance = GameObject.Find("ItemPanel").GetComponent<ItemPanel>();
            }
            return _instance;
        }
    }
    #endregion



}
