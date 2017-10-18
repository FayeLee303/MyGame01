using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponObj : MonoBehaviour {

    public WeaponModel Weapon { get; set; }
    public int CoolingTime { get; set; }//冷却时间

    public bool interactable = true; //是否可以点击，当道具冷却时不可以点击
    public CountDownTimer cdTimer; //计时器，在set自身的时候创建

    private Image CoolingTimeImage { get { return transform.Find("CoolingMask").GetComponent<Image>(); } } //冷却遮罩图片

    private Image weaponImage;
    private Image WeaponImage
    {
        get
        {
            if (weaponImage == null)
            {
                weaponImage = this.GetComponent<Image>();
            }
            return weaponImage;
        }
    }

    private Canvas canvas;

    private float targetScale = 1f;//原来比例
    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);//动画放大比例
    private float smoothing = 5;//动画速度

    public void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        SetCoollingTimer();
    }

    private void Update()
    {
        if (InventoryManager.Instance.IsPickedWeapon == true)
        {
            //捡起物品就让物品跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);//用out输出
            InventoryManager.Instance.PickedWeapon.SetLocalPosition(position);
        }
        if (transform.localScale.x != targetScale)
        {
            //动画
            float scale = Mathf.Lerp(transform.localScale.x, targetScale, smoothing * Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < 0.02f) //小于一定值就认为插值成功了，可以节约性能
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }

        SetCoolingImage();//一直更新图片显示
    }
#region 控制显示
    //设置武器
    public void SetWeapon(WeaponModel weapon)
    {
        this.Weapon = weapon;
        //Update UI
        WeaponImage.sprite = Resources.Load<Sprite>(Weapon.SpritePath);
        transform.localScale = animationScale;
    }

    //设置定时器
    private void SetCoollingTimer()
    {
        cdTimer = new CountDownTimer(Weapon.CoolingTime); //这里开始计时
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

    public void AdditionOnRole(WeaponModel weapon, RoleModel role)
    {
        //对人做加法
        role.Atk += weapon.Atk;
        role.Def += weapon.Def;
        role.MoveSpeed += weapon.MoveSpeed;
    }

    public void ResetOnRole(WeaponModel weapon, RoleModel role)
    {
        //对人做减法
        role.Atk -= weapon.Atk;
        role.Def -= weapon.Def;
        role.MoveSpeed -= weapon.MoveSpeed;
    }

    public void UseSkillToAttack(SkillModel skill)
    {
        InventoryManager.Instance.ShowInfoBox("放了一个" + skill.Name + "技能,对怪物造成" + skill.Damage + "点伤害");
    }

    #endregion
}
