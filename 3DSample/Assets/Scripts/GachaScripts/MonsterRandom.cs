using UnityEngine;
using TMPro;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MonsterRandom : MonoBehaviour
{
    int currentMonsterType;
    float monsterHP;
    float currentMonsterHP;
    [SerializeField] Sprite[] monsterImage;
    public TextMeshProUGUI currentHp;
    public GachaSystem itemDrop;

    public void Damaged(float value)
    {
        currentMonsterHP = currentMonsterHP - value;
    }

    void Start()
    {
        GetNewMonster();
        currentHp.text = $"체력 : {currentMonsterHP} / {monsterHP}";

    }

    void MonsterSetUp(int value)
    {
        currentMonsterHP = value;
        monsterHP = value;
        GetComponent<Image>().sprite = monsterImage[currentMonsterType];

    }

    void GetNewMonster()
    {
        currentMonsterType = Random.Range(0, 2);

        if (currentMonsterType == 0)
        {
            MonsterSetUp(300);
        }
        else if (currentMonsterType == 1)
        {
            MonsterSetUp(360);
        }
    }

    void Update()
    {
        currentHp.text = $"체력 : {currentMonsterHP} / {monsterHP}";
        if (currentMonsterHP <= 0)
        {
            itemDrop.Simulate();
            GetNewMonster();
        }
    }
}
