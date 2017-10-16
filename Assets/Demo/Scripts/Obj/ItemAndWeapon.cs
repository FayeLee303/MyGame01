using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAndWeapon : MonoBehaviour {

    public GameObject itemObj3DPrefab;
    public GameObject weaponObj3DPrefab;

    public float itemStayTime = 60;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //实例化ItemObj3D
    public void InstantiateItemObj3D(int itemID, Vector3 pos)
    {
        GameObject itemObj3D = Instantiate(itemObj3DPrefab) as GameObject;
        itemObj3D.transform.SetParent(this.transform);
        itemObj3D.transform.position = pos;//设置位置
        itemObj3D.transform.localScale = Vector3.one; //比例设为1
        itemObj3D.GetComponent<ItemObj3D>().SetItem(InventoryManager.Instance.GetItemById(itemID));//设置item
    }

    //实例化Weaponj3D
    public void InstantiateWeapon3D(int weaponId, Vector3 pos)
    {
        GameObject itemObj3D = Instantiate(weaponObj3DPrefab) as GameObject;
        itemObj3D.transform.SetParent(this.transform);
        itemObj3D.transform.position = pos;//设置位置
        itemObj3D.transform.localScale = Vector3.one; //比例设为1
        itemObj3D.GetComponent<WeaponObj3D>().SetWeapon(InventoryManager.Instance.GetWeaponById(weaponId));//设置item
    }

}
