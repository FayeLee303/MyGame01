using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAndWeapon : MonoBehaviour {

    public GameObject itemObj3DPrefab;
    public GameObject weaponObj3DPrefab;

    public AreaBase area;
    public int itemAmount = 10;
   // private List<ItemObj3D> item3DObjList = new List<ItemObj3D>();
    public int weaponAmount = 10;
    //private List<WeaponObj3D> weapon3DObjList = new List<WeaponObj3D>();

    private Point temP;
    public bool inEgde;

    public float itemStayTime = 60;


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

    //初始化随机生成
    public void InitInstantiateItemAndWeapon()
    {
        for (int i = 0; i < itemAmount; i++)
        {
            int id = Random.Range(0, 3);//随机ID
            //Debug.Log("物品" + id);
            temP = inEgde ? area.GetRandomPointInEdge() : area.GetRandomPointInArea();
            InstantiateItemObj3D(id, temP.position);
        }

        for (int i = 0; i < weaponAmount; i++)
        {
            int id = Random.Range(1, 6);//随机ID
            //Debug.Log("武器" + id);
            temP = inEgde ? area.GetRandomPointInEdge() : area.GetRandomPointInArea();
            InstantiateWeapon3D(id, temP.position);
        }
    }

    ////清除所有物体
    //public void ClearAll()
    //{
    //    var go = transform.GetComponentsInChildren<Transform>();
    //    foreach (Transform g in go)
    //    {
    //        Destroy(g.gameObject);
    //    }
    //}

}
