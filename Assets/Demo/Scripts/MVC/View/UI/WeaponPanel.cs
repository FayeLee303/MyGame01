using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : Inventory
{
    #region
    private static WeaponPanel _instance;
    public static WeaponPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                //这里不能用this来获取
                _instance = GameObject.Find("WeaponPanel").GetComponent<WeaponPanel>();
            }
            return _instance;
        }
    }
    #endregion


}
