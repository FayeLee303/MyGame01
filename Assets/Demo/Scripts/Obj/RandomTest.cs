using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Demos;
using UnityEngine;

public class RandomTest : MonoBehaviour
{

    private MapGenerator mapGenerator;
    private ObjectPlacer element_1;
    private ObjectPlacer element_2;
    private ObjectPlacer monsters;
    private ItemAndWeapon itemAndWeapon;

    // Use this for initialization
    void Start ()
	{
	    mapGenerator = GameObject.Find("Edge").GetComponent<MapGenerator>();
	    element_1 = GameObject.Find("Element").GetComponent<ObjectPlacer>();
	    element_2 = GameObject.Find("Element").GetComponent<ObjectPlacer>();
	    //monsters = GameObject.Find("Monsters").GetComponent<ObjectPlacer>();
	    itemAndWeapon = GameObject.Find("ItemAndWeapon").GetComponent<ItemAndWeapon>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Random()
    {
        //地图
        mapGenerator.survivingRooms.Clear();
        mapGenerator.GenerateMap();
        element_1.ClearAndRepositionObjects();
        element_2.ClearAndRepositionObjects();
        //monsters.ClearAndRepositionObjects();
        //itemAndWeapon.ClearAll();
        //itemAndWeapon.InitInstantiateItemAndWeapon();
    }
}
