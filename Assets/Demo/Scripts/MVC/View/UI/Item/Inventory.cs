using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    //private List<ItemSlot> itemSlotList;
    private ItemSlot[] itemSlotList;
    private WeaponSlot[] weaponSlotList;

    public virtual void Start()
    {
        itemSlotList = transform.GetComponentsInChildren<ItemSlot>();
        weaponSlotList = transform.GetComponentsInChildren<WeaponSlot>();

    }

#region Item
    //根据ID存东西，如果存成功就返回true
    public bool StoreItem(int id)
    {
        ItemModel item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }

    //根据物品存东西，如果存成功就返回true
    public bool StoreItem(ItemModel item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存的东西不存在");
            return false;
        }
        //看这个物体是不是只能存一个
        if (item.MaxLimit == 1)
        {
            ItemSlot slot = FindEmptyItemSlot();
            if (slot == null)
            {
                //Debug.LogWarning("格子已经满了");
                InventoryManager.Instance.ShowInfoBox("道具格子已经满了");
                return false;
            }
            else
            {
                slot.StoreItem(item); //把物品存进去
            }
        }
        else
        {
            ItemSlot slot = FindSameIdSlot(item);
            if (slot == null)
            {
                //如果找不到存相同东西的格子就找一个新格子存
                ItemSlot _slot = FindEmptyItemSlot();
                if (_slot == null)
                {
                    //Debug.LogWarning("格子已经满了");
                    InventoryManager.Instance.ShowInfoBox("道具格子已经满了");
                    return false;
                }
                else
                {
                    _slot.StoreItem(item); //把物品存进去
                }
            }
            else
            {
                slot.StoreItem(item); //把物品存进去
            }
        }
        return true;
    }

    //找一个空的格子
    public ItemSlot FindEmptyItemSlot()
    {
        foreach (ItemSlot itemSlot in itemSlotList)
        {
            if (itemSlot.transform.childCount == 0)
            {
                return itemSlot;
            }
        }
        return null;
    }

    //找一个装同样东西的格子
    public ItemSlot FindSameIdSlot(ItemModel item)
    {
        foreach (ItemSlot slot in itemSlotList)
        {
            //当格子不为空，格子装同样东西，并且还没有装满时
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.Id && slot.IsFilled() == false)
            {
                return slot;
            }
        }
        return null;
    }

    //根据顺序在链表中返回ItemObj
    public ItemObj FindItemObjInSlot(int index)
    {
        if (itemSlotList[index].transform.Find("ItemObjUI(Clone)") == null) return null;
        return itemSlotList[index].transform.GetChild(0).GetComponent<ItemObj>();
    }
    #endregion

#region Weapon
    //根据ID存东西，如果存成功就返回true
    public bool StoreWeapon(int id)
    {
        WeaponModel weapon = InventoryManager.Instance.GetWeaponById(id);
        return StoreWeapon(weapon);
    }

    //根据武器存东西，如果存成功就返回true
    public bool StoreWeapon(WeaponModel weapon)
    {
        if (weapon == null)
        {
            Debug.LogWarning("要存的东西不存在");
            return false;
        }else
        {
            //先看当前是否已经装备了相同类型的武器，如果是就debug，如果不是再找空格子
            if (FindSameTypeSlot(weapon))
            {
                //Debug.Log("想装备的武器类型重复");
                InventoryManager.Instance.ShowInfoBox("想装备的武器类型重复");
                return false;
            }
            else
            {
                //找一个新格子存
                WeaponSlot _slot = FindEmptyWeaponSlot();
                if (_slot == null)
                {
                    //Debug.LogWarning("格子已经满了");
                    InventoryManager.Instance.ShowInfoBox("武器格子已经满了");
                    return false;
                }
                else
                {
                    _slot.StoreWeapon(weapon); //把物品存进去
                }
            }
        }      
        return true;
    }

    //找一个空的武器格子
    public WeaponSlot FindEmptyWeaponSlot()
    {
        foreach (WeaponSlot weaponSlot in weaponSlotList)
        {
            if (weaponSlot.transform.childCount == 0)
            {
                return weaponSlot;
            }
        }
        return null;
    }

    //看当前是否已经装备了相同类型的武器子
    public bool FindSameTypeSlot(WeaponModel weapon)
    {
        foreach (WeaponSlot weaponSlot in weaponSlotList)
        {
            //当格子不为空
            if (weaponSlot.IsFilled())
            {
                //格子装的武器的类型是否和当前要存的武器类型一样
                if (weaponSlot.transform.Find("WeaponObjUI(Clone)").transform.GetComponent<WeaponObj>().Weapon.Type ==
                    weapon.Type)
                {
                    return true; //已经有了
                }
                else
                {
                    return false; //还没有
                }
            }
            else
            {
                return false; //还没有
            }
        }
        return false;
    }

    //根据顺序在链表中返回WeaponObj
    public WeaponObj FindWeaponObjInSlot(int index)
    {
        if (weaponSlotList[index].transform.Find("WeaponObjUI(Clone)") == null) return null;
        return weaponSlotList[index].transform.GetChild(0).GetComponent<WeaponObj>();
    }

    #endregion

}
