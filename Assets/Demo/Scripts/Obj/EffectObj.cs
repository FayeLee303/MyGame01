using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObj : MonoBehaviour
{

    public int id = 1;
    EffectModel effect;
	// Use this for initialization
	void Start ()
	{
	    effect = InventoryManager.Instance.GetEffectById(id);
	    Invoke("DestorySelf", effect.LastTime);//自己管理自己的摧毁
	}

    private void DestorySelf()
    {
        Destroy(gameObject);
    }
}
