using UnityEngine;
using TMPro;

public class Critsystem : MonoBehaviour
{
    public int playerAttack = 30;
    public int totalHits = 0;
    public int critHits = 0;
    public float targetRate = 0.3f;         //30%의 목표 확률
    bool isCritical;
    public TextMeshProUGUI[] criticalUI;
    public MonsterRandom damageTake;

    public bool RollCrit()
    {
        totalHits++;
        float currentRate = 0;
        if(critHits > 0)
        {
            currentRate = (float)critHits / totalHits;
        }

        if(currentRate < targetRate && (float)(critHits + 1)  / totalHits <= targetRate)
        {
            critHits++;
            isCritical = true;
            return true;        //치명타가 발생한 이후에도 현재 비율이 여전히 낮다면 무조건 발생시킴
        }

        if(currentRate > targetRate && (float)critHits / totalHits >= targetRate)
        {
            return false;
        }

        if(Random.value < targetRate)
        {
            critHits++;
            isCritical = true;
            return true;            //기본 확률 처리
        }
        return false;
    }

    public void SimulateCritical()
    {
        isCritical = false;
        RollCrit();
        criticalUI[0].text = $"총 공격 횟수 : {totalHits}";
        criticalUI[1].text = $"발생한 치명타 횟수 : {critHits}";
        criticalUI[2].text = $"목표 치명타 확률 : {targetRate * 100} %";
        criticalUI[3].text = $"발생한 치명타 비율 : {((float)critHits / totalHits) * 100}";
        DamageInput();
    }

    public void DamageInput()
    {       
        float outputDamage = 0;
        if(isCritical == true)
        {
            outputDamage = playerAttack * 2f;
        }
        else
        {
            outputDamage = playerAttack;
        }
        damageTake.Damaged(outputDamage);
        outputDamage = 0;
    }

    void Start()
    {
        criticalUI[0].text = $"총 공격 횟수 : {totalHits}";
        criticalUI[1].text = $"발생한 치명타 횟수 : {critHits}";
        criticalUI[2].text = $"목표 치명타 확률 : {targetRate * 100} %";
        criticalUI[3].text = $"발생한 치명타 비율 : 0";
    }

    void Update()
    {
        
    }
}
