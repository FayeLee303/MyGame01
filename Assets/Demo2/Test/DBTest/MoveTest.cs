using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

[RequireComponent(typeof(CharacterController))]

public class MoveTest : MonoBehaviour {

    private CharacterController controller = null;
    private UnityArmatureComponent armatureComponent = null;
    public GameObject role;

    //private const string NORMAL_ANIMATION_GROUP = "normal";
    //private const string AIM_ANIMATION_GROUP = "aim";
    //private const string ATTACK_ANIMATION_GROUP = "attack";

    //键位
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode DirToLeft = KeyCode.Q;
    public KeyCode DirToRight = KeyCode.E;
    public KeyCode changeWeapon = KeyCode.Space;

    public float walkspeed;
    public bool faceDir;//false是右边，true是左边

    //private static readonly string[] WEAPON_LEFT_LIST = { "weapon_1502b_l", "weapon_1005", "weapon_1005b", "weapon_1005c", "weapon_1005d" };
    //private static readonly string[] WEAPON_RIGHT_LIST = { "weapon_1502b_r", "weapon_1005", "weapon_1005b", "weapon_1005c", "weapon_1005d", "weapon_1005e" };

    public int _weaponLeftIndex;//武器编号
    //private Armature _weaponLeft = null;//武器挂件

    //private string[] _replaceDisplays = {
    //    // Replace mesh display.
    //    "itemA", "itemB"
    //};


    // Use this for initialization
    void Start () {
        if (controller == null) {
            controller = this.GetComponent<CharacterController>();
        }
        if (armatureComponent == null && role != null) {
            armatureComponent = role.GetComponent<UnityArmatureComponent>();
        }
        walkspeed = 3f;
        faceDir = false;
        armatureComponent.armature.flipX = false;
        armatureComponent.animation.Play("stand", -1);
        _weaponLeftIndex = 0;

        // 初始化武器挂件
        //_weaponLeft = armatureComponent.armature.GetSlot("weapon_l").childArmature;


    }
	
	// Update is called once per frame
	void Update () {

        bool moveKeyPressed = Input.GetKeyDown(left) || Input.GetKeyDown(right) || Input.GetKeyDown(up) || Input.GetKeyDown(down);
        bool moveKeyUp = Input.GetKeyUp(left) || Input.GetKeyUp(right) || Input.GetKeyUp(up) || Input.GetKeyUp(down);
        bool moving = Input.GetKey(left) || Input.GetKey(right) || Input.GetKey(up) || Input.GetKey(down);
        // if any move key was pressed down this frame, start walk animation
        if (moveKeyPressed)
        {
            armatureComponent.animation.Play("walk", -1);
           
        }
        // else if not moving at all and a movement key was released
        else if (!moving && moveKeyUp)
        {
            armatureComponent.animation.Play("stand", -1);
        }
        if (moving) {
            if (Input.GetKey(left))
            {
                MoveCtrl("left");
                faceDir = false;
                armatureComponent.armature.flipX = false;
            }
            else if (Input.GetKey(right))
            {
                MoveCtrl("right");
                faceDir = true;
                armatureComponent.armature.flipX = true;
            }
            else if (Input.GetKey(up))
            {
                MoveCtrl("up");
            }
            else if (Input.GetKey(down))
            {
                MoveCtrl("down");
            }
        }

        if (Input.GetKeyDown(changeWeapon)) {
            ChangeWeapon();
        }
    }

    void ChangeWeapon() {
        //_weaponLeftIndex++;
        //if (_weaponLeftIndex >= WEAPON_LEFT_LIST.Length)
        //{
        //    _weaponLeftIndex = 0;
        //}

        //var weaponName = WEAPON_LEFT_LIST[_weaponLeftIndex];
        //_weaponLeft = UnityFactory.factory.BuildArmature(weaponName);
        //armatureComponent.armature.GetSlot("weapon_l").childArmature = _weaponLeft;
        UnityFactory.factory.ReplaceSlotDisplay(
                       "Ubbie_ske",//工程名字
                       "items",//骨架名字
                       "itemB",//要替换插槽的插槽名字或涂层名字
                       "_0002_乌云哥哥的大腿",//要替换的图片名字
                       armatureComponent.armature.GetSlot("item")//被替换的插槽实例
                   );



    }

    void MoveCtrl(string dir) {
        switch(dir){
            case "left":
                controller.Move(new Vector3(-walkspeed, 0, 0) * Time.deltaTime);
                break;
            case "right":
                controller.Move(new Vector3(walkspeed, 0, 0) * Time.deltaTime);
                break;
            case "up":
                controller.Move(new Vector3(0, 0, walkspeed) * Time.deltaTime);
                break;
            case "down":
                controller.Move(new Vector3(0, 0, -walkspeed) * Time.deltaTime);
                break;
        }
    }

    
}
