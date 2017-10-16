using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 物品槽
/// </summary>
public class ItemSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler {

    public GameObject itemPrefab;
    

    //把item放在自身下面，如果已经有了就让数量增加
    public void StoreItem(ItemModel item)
    {
        //如果没有就实例化出来
        if (transform.childCount == 0)
        {
            GameObject itemObj = Instantiate(itemPrefab) as GameObject;
            itemObj.transform.SetParent(this.transform);
            itemObj.transform.localPosition = Vector3.zero; //位置归零
            itemObj.transform.localScale = Vector3.one; //比例设为1
            itemObj.GetComponent<ItemObj>().SetItem(item, 1);
        }
        else
        {
            transform.Find("ItemObjUI(Clone)").GetComponent<ItemObj>().AddAmount(1);
        }
    }

    //返回当前物品槽储存的物品ID
    public int GetItemId()
    {
        return transform.Find("ItemObjUI(Clone)").GetComponent<ItemObj>().Item.Id;
    }

    //如果True就是当前格子已经装满某物品了
    public bool IsFilled()
    {
        ItemObj itemObj = transform.Find("ItemObjUI(Clone)").GetComponent<ItemObj>();
        return itemObj.Item.MaxLimit <= itemObj.Amount;
    }

    //重写Unity自带的事件触发函数
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string text = transform.Find("ItemObjUI(Clone)").GetComponent<ItemObj>().Item.GetToolTipText();
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

    //自身是空，点击物品就拿起，点击空格不做操作
    //自身不是空，点击其他物品就进行交换，点击空格就放到格子里
    //当选中物体时如果按下Ctrl，只选中物体数目的一半
    //当放下物体时按住Ctrl键，一个一个放下
    public void OnPointerDown(PointerEventData eventData)
    {
        //1自身是空
        //1.1 isPicked = true
        //  1.1.1按下Ctrl 放下当前鼠标上物品的一个
        //  1.1.2没有按下Ctrl 放下当前鼠标上物品的所有
        //1.2 isPicked = false 不做任何处理

        //2自身不是空
        //2.1 isPicked = true
        //  2.1.1自身的id == pickedItem.Id 
        //      2.1.1.1按下Ctrl 放下当前鼠标上物品的一个
        //      2.1.1.2没有按下Ctrl
        //          2.1.1.2.1放下当前鼠标上物品的所有
        //          2.1.1.2.2只能放下其中一部分
        //  2.1.2自身的id ！= pickedItem.Id，pickedItem跟当前物品交换
        //2.2 isPicked = false
        //  2.2.1按下Ctrl，取得当前物品槽中物品的一半
        //  2.2.2没有按下Ctrl，把当前物品里面的物品放在鼠标上

        if (transform.childCount > 0) //格子里有东西
        {
            ItemObj currentItem = transform.Find("ItemObjUI(Clone)").GetComponent<ItemObj>();//当前格子里的东西
            if (InventoryManager.Instance.IsPickedItem == false) //当前没有选中任何物品，鼠标上没有物品
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    int amountPicked = (currentItem.Amount + 1) / 2; //得到整数，如果是奇数会多捡起一个
                    InventoryManager.Instance.PickUpItem(currentItem.Item, amountPicked); //捡起
                    int amountRemained = currentItem.Amount - amountPicked;//格子里的数量减少
                    if (amountRemained <= 0)
                    {
                        Destroy(currentItem.gameObject); //如果全拿走了，就把格子里的东西销毁
                    }
                    else
                    {
                        currentItem.SetAmount(amountRemained); //如果没有全拿走就更新格子里的东西的数量
                    }
                }
                else//没有按下Ctrl全拿走
                {
                    InventoryManager.Instance.PickUpItem(currentItem.Item,
                        currentItem.Amount); //把当前格子的物品信息赋值给PickedItem，让它跟随鼠标移动
                    Destroy(currentItem.gameObject); //把格子里的东西销毁

                }
            }
            else
            {
                //当前已经选中物品，鼠标上有物品
                if (currentItem.Item.Id == InventoryManager.Instance.PickedItem.Item.Id) //当前格子的物品和鼠标上的物品一样时
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        if (currentItem.Item.MaxLimit > currentItem.Amount) //物品槽还有容量就放下
                        {
                            currentItem.AddAmount(1); //格子里的东西数量+1
                            InventoryManager.Instance.RemoveItem(1); //鼠标上的东西数量-1
                        }
                        else
                        {
                            return;
                        } //没有容量就放不下不做操作
                    }
                    else
                    {
                        if (currentItem.Item.MaxLimit > currentItem.Amount)
                        {
                            int amountRemain = currentItem.Item.MaxLimit - currentItem.Amount; //当前物品槽剩余空间
                            if (amountRemain > InventoryManager.Instance.PickedItem.Amount) //全都能放下
                            {
                                currentItem.SetAmount(currentItem.Amount + InventoryManager.Instance.PickedItem.Amount);
                                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                            }
                            else
                            {
                                //只能放下一部分
                                currentItem.SetAmount(currentItem.Item.MaxLimit);
                                InventoryManager.Instance.RemoveItem(amountRemain); //鼠标上的东西数量减少
                            }
                        }
                        else
                        {
                            return;
                        } //没有容量就放不下不做操作
                    }
                }
            }
        }
        else//是个空格
        {
            if (InventoryManager.Instance.IsPickedItem == true)
            {
                if (Input.GetKey(KeyCode.LeftControl))//按下crtl一次只放下一个
                {
                    this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    InventoryManager.Instance.RemoveItem(1);
                }
                else
                {
                    //遍历当前鼠标有多少个然后都存进去
                    for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                    {
                        this.StoreItem(InventoryManager.Instance.PickedItem.Item);
                    }
                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                }
            }
            else { return; }//当前没有选中任何物品，鼠标上没有物品,点击空格没有反应

        }
    }
}
