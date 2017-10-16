using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBornPoint : MonoBehaviour {

    public AreaBase area;
    public GameObject monsterPrefab;
    public int monsterAmount = 10;
    private List<MonsterObj> monsterObjList = new List<MonsterObj>();
    private Point temP;
    public bool inEgde;
    public int updateTime = 5; //刷新间隔
    private float elapsed; //计时器

    private void Start()
    {
        MonstersBorn(monsterAmount);//初始的怪
        elapsed = updateTime;
    }

    private void Update()
    {
        //得到当前数量,如果有怪死了就等一段间隔重新生成怪
        int currentAmount = monsterObjList.Capacity; 
        if (currentAmount != monsterAmount)
        {
            elapsed -= Time.deltaTime;
            if (elapsed <= 0)
            {
                MonstersBorn(monsterAmount - currentAmount + 1);
                elapsed = updateTime;
            }
        }
    }
    
    //生成怪物
    private void MonstersBorn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject go = GameObject.Instantiate(monsterPrefab);
            monsterObjList.Add(go.GetComponent<MonsterObj>());
            go.transform.SetParent(transform);
            ResetMonsters(monsterObjList[i]);
        }
    }

    //设置位置
    private void ResetMonsters(MonsterObj m)
    {
        temP = inEgde ? area.GetRandomPointInEdge() : area.GetRandomPointInArea();
        m.transform.position = temP.position;
        m.transform.rotation = temP.rotation;
    }


}
