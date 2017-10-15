using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBonesTest : MonoBehaviour {

    private UnityArmatureComponent armatureComponent = null;
    // Use this for initialization
    void Start () {
        //// 读取数据
        //UnityFactory.factory.LoadDragonBonesData("Resources/Ubbie_ske");// DragonBones file path (without suffix)
        //UnityFactory.factory.LoadTextureAtlasData("Resources/Ubbie_tex");//Texture atlas file path (without suffix)
        //// Create armature.
        //armatureComponent = UnityFactory.factory.BuildArmatureComponent("Ubbie"); // Input armature names
        //if (armatureComponent == null)
        //{
        //    Debug.Log("没有载入");
        //}
        //else {
        //    armatureComponent.animation.Play("walk");// Play animation.
        //    armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //    // Set flip.
        //    armatureComponent.armature.flipX = true;
        //}

        armatureComponent = GetComponent<UnityArmatureComponent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("111111");
            UnityFactory.factory.ReplaceSlotDisplay(
                       "02",
                       "ubbie",
                       "itemA",
                       "apple_",
                       armatureComponent.armature.GetSlot("item")
                   );
        }
	}
}
