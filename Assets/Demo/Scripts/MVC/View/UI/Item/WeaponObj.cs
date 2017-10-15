using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponObj : MonoBehaviour {

    public WeaponModel Weapon { get; set; }
    public int CoolingTime { get; set; }//冷却时间

    private Image weaponImage;
    private Image coolingMaskImage;//冷却遮罩
    private Text coolingTimeText;

    ////放在这里获得比在Start里好
    //private Image WeaponImage
    //{
    //    get
    //    {
    //        if (weaponImage == null)
    //        {
    //            weaponImage = this.GetComponent<Image>();
    //        }
    //        return weaponImage;
    //    }
    //}

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

    public void Awake()
    {
        weaponImage = this.GetComponent<Image>();
        coolingTimeText = this.GetComponentInChildren<Text>();
        coolingMaskImage = this.GetComponentInChildren<Image>();
    }

    //设置武器
    public void SetWeapon(WeaponModel weapon)
    {
        this.Weapon = weapon;
        //Update UI
        weaponImage.sprite = Resources.Load<Sprite>(Weapon.SpritePath);

    }

    //设置冷却时间
    public void SetCoolingTime(int coolingTime)
    {
        this.CoolingTime = coolingTime;
        coolingTimeText.text = coolingTime.ToString();
        //重置
        //TODO
    }

    //冷却时间计时器，冷却图片跟着动
    //TODO

}
