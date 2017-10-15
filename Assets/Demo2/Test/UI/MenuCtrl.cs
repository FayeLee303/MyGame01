using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuCtrl : MonoBehaviour {

    public GameObject menu;
    private DOTweenAnimation menuAni;
    public bool isMenuOn;
    public bool isOptionShow;

    public GameObject normalMenu;
    public GameObject optionMenu;

    // Use this for initialization
    void Start() {
        if (menu != null) {
            menuAni = menu.GetComponent<DOTweenAnimation>();
        }
        isMenuOn = false;
        isOptionShow = false;
        ShowOptionMenu(isOptionShow);
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isMenuOn)
            {
                MenuComeIn();
                isMenuOn = true;
                isOptionShow = false;
                ShowOptionMenu(isOptionShow);
            }
            else {
                MenuComeOut();
                isMenuOn = false;
            }
        }
    }

    private void MenuComeIn()
    {
        menuAni.DOPlayForward();
    }

    private void MenuComeOut() {
        menuAni.DOPlayBackwards();
    }

    public void OnOptionMenuClick() {
        isOptionShow = true;
        ShowOptionMenu(isOptionShow);        
    }

    public void OnBackClick() {
        isOptionShow = false;
        ShowOptionMenu(isOptionShow);
    }

    void ShowOptionMenu(bool e) {
        if (!e)
        {
            normalMenu.SetActive(true);
            optionMenu.SetActive(false);
        }
        else {
            normalMenu.SetActive(false);
            optionMenu.SetActive(true);
        }
    }
}
