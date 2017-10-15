using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtnCtrl : MonoBehaviour {

    public float skill_1_coldTime = 1f;
    public float skill_2_coldTime = 5f;
    private float timer_1 = 0;
    private float timer_2 = 0;
    private bool isStartTimer_1 = false;
    private bool isStartTimer_2 = false;
    private Image filledImage_1;
    private Image filledImage_2;
    
    public GameObject skill_1;
    public GameObject skill_2;

    public KeyCode Skill_1 = KeyCode.Alpha1;
    public KeyCode Skill_2 = KeyCode.Alpha2;

	// Use this for initialization
	void Start () {
        filledImage_1 = skill_1.transform.Find("filledImage").GetComponent<Image>();
        filledImage_2 = skill_2.transform.Find("filledImage").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
       
        if (Input.GetKeyDown(Skill_1)) {
            Skill_1_Event();
        }
        if (Input.GetKeyDown(Skill_2))
        {
            Skill_2_Event();

        }
        if (isStartTimer_1) {
            timer_1 += Time.deltaTime;
            filledImage_1.fillAmount = (skill_1_coldTime - timer_1) / skill_1_coldTime;
            if(timer_1 > skill_1_coldTime){
                filledImage_1.fillAmount = 0;
                timer_1 = 0;
                isStartTimer_1 = false;
            }
        }
        if (isStartTimer_2)
        {
            timer_2 += Time.deltaTime;
            filledImage_2.fillAmount = (skill_2_coldTime - timer_2) / skill_2_coldTime;
            if (timer_2 > skill_2_coldTime)
            {
                filledImage_2.fillAmount = 0;
                timer_2 = 0;
                isStartTimer_2 = false;
            }
        }
    }

    void Skill_1_Event()
    {
        isStartTimer_1 = true;
    }
    void Skill_2_Event()
    {
        isStartTimer_2 = true;
    }
}
