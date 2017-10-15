using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class InventoryManager  {
    //做成单例模式
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
                _instance = new InventoryManager();
            }
            return _instance;
        }
        
    }

    //私有的构造方法
    private InventoryManager()
    {
        ParseItemJson();
        ParseWeaponJson();
    }

    public void Init()
    {
        toolTip = GameObject.Find("ToolTip").GetComponent<ToolTip>();
        pickedItem = GameObject.Find("PickedObj").GetComponent<ItemObj>();
        infoBox = GameObject.Find("InfoBox").GetComponent<InfoBox>();
        instantiateObj = GameObject.Find("ItemAndWeapon").GetComponent<ItemAndWeapon>();
    }

#region Item相关
    public List<ItemModel> itemList;
    /// <summary>
    /// 解析物品信息
    /// </summary>
    private void ParseItemJson()
    {
        //文本在Unity里面是TextAsset类型
        TextAsset itemJsonText = Resources.Load<TextAsset>("Localization/ItemJson");
        if (itemJsonText != null)
        {
            string itemJsonString = itemJsonText.text;
            itemList = JsonMapper.ToObject<List<ItemModel>>(itemJsonString);
            //itemList = JsonUtility.FromJson<List<ItemModel>>(itemJsonString);
            if (itemList == null) return;
            //foreach (ItemModel item in itemList)
            //{
            //    Debug.Log(item);
            //}
        }
        else
        {
            Debug.Log("读取文件失败");
        }
    }

    //根据id从列表里取得物品
    public ItemModel GetItemById(int id)
    {
        foreach (ItemModel item in itemList)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }
    #endregion

#region Weapon相关
    public List<WeaponModel> weaponList;
    /// <summary>
    /// 解析武器信息
    /// </summary>
    private void ParseWeaponJson()
    {
        //文本在Unity里面是TextAsset类型
        TextAsset weaponJsonText = Resources.Load<TextAsset>("Localization/WeaponJson");
        if (weaponJsonText != null)
        {
            string weaponJsonString = weaponJsonText.text;
            //Debug.Log(weaponJsonString);
            weaponList = JsonMapper.ToObject<List<WeaponModel>>(weaponJsonString);
            //if (weaponList == null) Debug.Log("读取文件失败");
        }
        else
        {
            Debug.Log("读取文件失败");
        }
    }  

    //根据id从列表里取武器
    public WeaponModel GetWeaponById(int id)
    {
        foreach (WeaponModel weapon in weaponList)
        {
            if (weapon.Id == id)
            {
                return weapon;
            }
        }

        return null;
    }
    #endregion

#region PickedItem相关
    private ItemObj pickedItem;//鼠标选中的物体
    private bool isPicked = false; //标志是否选中

    public bool IsPicked
    {
        get { return isPicked; }
    }

    public ItemObj PickedItem
    {
        get { return pickedItem; }
        set { pickedItem = value; }
    }

    //拿起物品槽中指定数量的物品
    public void PickUpItem(ItemModel item,int amount)
    {
        PickedItem.SetItem(item,amount);
        isPicked = true;
        this.toolTip.HideToolTip(); //隐藏信息面板
        PickedItem.Show();
       
    }

    //当前鼠标上的的物品拿一部分放在格子里
    public void RemoveItem(int amount)
    {
        PickedItem.ReduceAmount(amount);
        if (pickedItem.Amount == 0)//全都放下了
        {
            isPicked = false;
            PickedItem.Hide();
        }
    }

    //在人的脚边丢弃物品
    public void DripItem(int amount,Vector3 dropPos)
    {
        PickedItem.ReduceAmount(amount);
        if (pickedItem.Amount == 0)//全都丢掉了
        {
            isPicked = false;
            PickedItem.Hide();
        }
        int dropRange = 10;//丢弃的范围

        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = dropPos + new Vector3(Random.Range(1, dropRange), Random.Range(1, dropRange),
                Random.Range(1, dropRange));
            InstantiateItemObj3D(PickedItem.Item.Id, pos); //丢了多少个实例化多少个出来
        }
        ShowInfoBox("丢弃了" + amount.ToString() + "个" + pickedItem.Item.Name); //显示提示
    }

    #endregion

#region ToolTip相关
    public ToolTip toolTip;
    public bool isToolTipShow = false;

    public void ShowToolTip(string content)
    {
        if (this.isPicked) return;//如果当前鼠标上有选中的物品就不显示信息面板
        isToolTipShow = true;
        toolTip.ShowToolTip(content);
    }

    public void HideToolTip()
    {
        isToolTipShow = false;
        toolTip.HideToolTip();
    }

    #endregion

#region InfoBox相关

    public InfoBox infoBox;
    public bool isInfoBoxShow = false;

    public void ShowInfoBox(string content)
    {
        isInfoBoxShow = true;
        infoBox.ShowInfoBox(content);
    }

    //public void HideInfoBox()
    //{
    //    isInfoBoxShow = false;
    //    infoBox.HideInfoBox();
    //}

    #endregion

#region 实例化物体相关

    public  ItemAndWeapon instantiateObj;

    public void InstantiateItemObj3D(int itemID, Vector3 pos)
    {
        instantiateObj.InstantiateItemObj3D(itemID,pos);
    }

    #endregion
}
