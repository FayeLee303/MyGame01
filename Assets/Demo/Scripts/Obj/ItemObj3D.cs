using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObj3D : MonoBehaviour {

    public ItemModel Item { get; private set; }//外部只读，只能在这个类内部更改

    private Sprite currentSprite;
    private BoxCollider boxCollider;//实际的碰撞盒

    private Sprite Sprite
    {
        get {return GetComponentInChildren<SpriteRenderer>().sprite;}
        set { GetComponentInChildren<SpriteRenderer>().sprite = value; }
    }
    private BoxCollider BoxCollider
    {
        get { return GetComponent<BoxCollider>(); }
    }

    public void Awake ()
    {
        Invoke("DestorySelf", InventoryManager.Instance.instantiateObj.itemStayTime);//不拾取就自动消失
    }

    // Update is called once per frame
    public void Update ()
    {
    }

    //设置Item
    public void SetItem(ItemModel item)
    {
        this.Item = item;
        SetSprite(item);
        SetColliderBox();//重新设置碰撞盒子
    }
    //设置图片
    private void SetSprite(ItemModel item)
    {
        Sprite = Resources.Load<Sprite>(item.SpritePath);
    }
    //设置碰撞盒
    private void SetColliderBox()
    {
        float width = transform.localScale.x * Sprite.bounds.size.x;
        float hight = transform.localScale.y * Sprite.bounds.size.y;
        BoxCollider.size = new Vector3(width, (float)(hight / 1.414), (float)(hight / 1.414));
        SetLocalPositionY((float) (hight / 1.414 / 2));//重设y坐标
    }
    //调整y坐标
    private void SetLocalPositionY(float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    //碰撞拾取
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ItemSlot slot = ItemPanel.Instance.FindSameIdSlot(this.Item);
            if (slot != null) //如果碰到的东西已经在道具槽里有一个格子装着一样的东西
            {
                if (slot.IsFilled()) //如果已经满了就找空格子
                {
                    ItemSlot _slot = ItemPanel.Instance.FindEmptyItemSlot();
                    if (_slot != null) //如果有空格子就存进去
                    {
                        _slot.StoreItem(this.Item);
                        InventoryManager.Instance.ShowInfoBox("拾取了1个" + this.Item.Name);
                        Destroy(gameObject);
                    }
                    else //没有空格子就提示背包已满
                    {
                        InventoryManager.Instance.ShowInfoBox("道具格子已经满了");
                    }
                }
                else //不满就存进去
                {
                    slot.StoreItem(this.Item);
                    InventoryManager.Instance.ShowInfoBox("拾取了1个" + this.Item.Name);
                    Destroy(gameObject);
                }
            }
            else//四个格子都没有装同样的东西
            {
                ItemSlot _slot = ItemPanel.Instance.FindEmptyItemSlot();
                if (_slot != null) //如果有空格子就存进去
                {
                    _slot.StoreItem(this.Item);
                    InventoryManager.Instance.ShowInfoBox("拾取了1个" + this.Item.Name);
                    Destroy(gameObject);
                }
                else//没有空格子就提示背包已满
                {
                    InventoryManager.Instance.ShowInfoBox("道具格子已经满了");
                }
            }
        }
    }
    //销毁自身
    private void DestorySelf()
    {
        Destroy(gameObject);
    }
}
