using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAndWeapon : MonoBehaviour {

    public GameObject itemObj3DPrefab;

    public float stayTime = 60;
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

}
