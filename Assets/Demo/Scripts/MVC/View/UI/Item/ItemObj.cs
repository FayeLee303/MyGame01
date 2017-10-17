using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;

public class ItemObj : MonoBehaviour {

	public ItemModel Item { get;private set; }//外部只读，只能在这个类内部更改
    public int Amount { get; private set; }
    public bool interactable = true; //是否可以点击，当道具冷却时不可以点击
    public CountDownTimer cdTimer; //计时器，在set自身的时候创建

    private Canvas canvas;

    public void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        SetCoollingTimer();
    }

    public void Update()
    {
        if (InventoryManager.Instance.IsPickedItem == true)
        {
            //捡起物品就让物品跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);//用out输出
            InventoryManager.Instance.PickedItem.SetLocalPosition(position);
        }
        if (transform.localScale.x!=targetScale)
        {
            //动画
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing*Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < 0.02f) //小于一定值就认为插值成功了，可以节约性能
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }

        SetCoolingImage();//一直更新图片显示
    }

#region UI Component
    private Image itemImage;
    private Text amountText;
    private float targetScale = 1f;//原来比例
    private Vector3 animationScle = new Vector3(1.4f,1.4f,1.4f);//动画放大比例
    private float smoothing = 5;//动画速度

    //放在这里获得比在Start里好
    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = this.GetComponent<Image>();
            }
            return itemImage;
        }
    }
    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = this.GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }

    private Image CoolingTimeImage { get { return transform.Find("CoolingMask").GetComponent<Image>(); } } //冷却遮罩图片

    #endregion

    #region 控制显示
    //自身发生变化
    public void SetItem(ItemModel item, int amount)
    {
        this.Item = item;
        this.Amount = amount;
      
        //Update UI
        ItemImage.sprite =  Resources.Load<Sprite>(item.SpritePath);
        AmountText.text = Amount.ToString();
        transform.localScale = animationScle;
    }

    public void SetAmount(int amount)
    {
        this.Amount = amount;
        //Update UI
        if (Item.MaxLimit > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
        transform.localScale = animationScle;
    }

    //增加数量
    public void AddAmount(int amount)
    {
        this.Amount += amount;
        //Update UI
        if (Item.MaxLimit > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
        transform.localScale = animationScle;
    }

    //减少数量
    public void ReduceAmount(int amount)
    {
        this.Amount -= amount;
        //Update UI
        if (Item.MaxLimit > 1)
        {
            AmountText.text = Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
        transform.localScale = animationScle;
    }

    //设置定时器
    private void SetCoollingTimer()
    {
        cdTimer = new CountDownTimer(Item.CoolingTime); //这里开始计时
    }
    //设置冷却显示
    public void SetCoolingImage()
    {
        if (!cdTimer.IsTimeUp)
        {
            CoolingTimeImage.fillAmount = 1 - cdTimer.GetPercent(); //图片的显示
            interactable = false;  //冷却期间不可交互
        }
        else
        {
            CoolingTimeImage.fillAmount = 0; //时间到了设为0
            interactable = true; //时间到了可以交互
        }
    }

    //控制显示
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }
    #endregion

#region 实际作用效果

    public void EffectOnRole(ItemObj itemobj, RoleModel role,Transform transform)
    {
        InventoryManager.Instance.InstantiateEffect(itemobj.Item.EffectId, transform);//实例化
        InventoryManager.Instance.ShowInfoBox("使用了1个" + itemobj.Item.Name);
        //对人做加减
        if (role.Hp != role.MaxHp)
        {
            role.Hp += itemobj.Item.RecoverHp;
            if (role.Hp >= role.MaxHp) role.Hp = role.MaxHp;
        }
        if (role.Mp != role.MaxMp)
        {
            role.Mp += itemobj.Item.RecoverMp;
            if (role.Mp >= role.MaxMp) role.Mp = role.MaxMp;
        }
        itemobj.ReduceAmount(1);     
        if (itemobj.Amount <= 0) //用完了就销毁自身
        {
            Destroy(gameObject);
        }
    }
#endregion
}

