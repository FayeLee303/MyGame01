using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 武器槽
/// </summary>
public class WeaponSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{

    public GameObject weaponPrefab;


    //把weapon放在自身下面，重置冷却时间
    public void StoreWeapon(WeaponModel weapon)
    {
        //如果没有就实例化出来
        if (transform.childCount == 0)
        {
            GameObject weaponObj = Instantiate(weaponPrefab) as GameObject;
            weaponObj.transform.SetParent(this.transform);
            weaponObj.transform.localPosition = Vector3.zero; //归零
            weaponObj.GetComponent<WeaponObj>().SetWeapon(weapon);

            //重置冷却时间
            //weaponObj.GetComponent<WeaponObj>().SetCoolingTime();
        }
    }

    //返回当前武器槽储存的武器ID
    public int GetWeaponId()
    {
        return transform.Find("WeaponObjUI(Clone)").GetComponent<WeaponObj>().Weapon.Id;
    }

    //如果True就是这个武器槽装备了武器
    //要在其他地方判断两个武器槽都装备了武器
    public bool IsFilled()
    {
        //transform.Find来找到Child，不用GetChild(index)
        if (transform.Find("WeaponObjUI(Clone)") == null || transform.Find("WeaponObjUI(Clone)").GetComponent<WeaponObj>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //重写Unity自带的事件触发函数
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0 )
        {
            string text = transform.Find("WeaponObjUI(Clone)").GetComponent<WeaponObj>().Weapon.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(text); //要传递数据

        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            InventoryManager.Instance.HideToolTip();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount > 0) //格子里有东西
        {
            WeaponObj currentWeapon = transform.Find("WeaponObjUI(Clone)").GetComponent<WeaponObj>();//当前格子里的东西
            if (InventoryManager.Instance.IsPickedWeapon == false) //当前没有选中任何物品，鼠标上没有物品
            {
                InventoryManager.Instance.PickUpWeapon(currentWeapon.Weapon); //捡起
                Destroy(currentWeapon.gameObject); //如果全拿走了，就把格子里的东西销毁
            }
            else//当前已经选中物品，鼠标上有物品
            {
                return;
            }
        }
        else //格子里没有东西
        {
            if (InventoryManager.Instance.IsPickedWeapon == true) //当前鼠标上有东西就放下
            {
                this.StoreWeapon(InventoryManager.Instance.PickedWeapon.Weapon);
                InventoryManager.Instance.RemoveWeapon();
            }
            else { return; }//当前没有选中任何物品，鼠标上没有物品,点击空格没有反应
        }
    }
}
