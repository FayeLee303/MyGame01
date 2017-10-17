using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponObj : MonoBehaviour {

    public WeaponModel Weapon { get; set; }
    public int CoolingTime { get; set; }//冷却时间

    private Image weaponImage;
 //   private Image coolingMaskImage;//冷却遮罩
 //   private Text coolingTimeText;

    //放在这里获得比在Start里好
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
    //private Text CoolingTimeText
    //{
    //    get
    //    {
    //        if (coolingTimeText == null)
    //        {
    //            coolingTimeText = this.GetComponentInChildren<Text>();
    //        }
    //        return coolingTimeText;
    //    }
    //}
    //private Image CoolingMaskImage
    //{
    //    get
    //    {
    //        if (coolingMaskImage == null)
    //        {
    //            coolingMaskImage = this.GetComponentInChildren<Image>();
    //        }
    //        return coolingMaskImage;
    //    }
    //}

    private Canvas canvas;

    private float targetScale = 1f;//原来比例
    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);//动画放大比例
    private float smoothing = 5;//动画速度

    public void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
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

    //设置冷却时间
    public void SetCoolingTime(int coolingTime)
    {
        this.CoolingTime = coolingTime;
        //CoolingTimeText.text = coolingTime.ToString();
        //重置
        //TODO
    }

    //冷却时间计时器，冷却图片跟着动
    //TODO

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
    #endregion
}
