using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class DamageSimul : MonoBehaviour
{
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI logDisplay;
    public TextMeshProUGUI resultDisplay;
    public TextMeshProUGUI rangeDisplay;
    public TextMeshProUGUI missDisplay;
    public TextMeshProUGUI bestDamgeDisplay;
    public TextMeshProUGUI missCountDisplay;
    public TextMeshProUGUI weakPointDisplay;
    public TextMeshProUGUI critCountDisplay;

    private int level = 1;
    private float totalDamage = 0, baseDamage = 20f;
    private int attackCount = 0;
    private float baseStdNormal;
    private int weakPointCount = 0;
    private int missCount = 0;
    private int critCount = 0;
    private float bestDamage = 0f;

    private string weaponName;
    private float stdDevMult, critRate, critMult;
    void Start()
    {
        SetWeapon(0);           //단검 스타트
    }

    private void ResetData()
    {
        totalDamage = 0;
        attackCount = 0;
        level = 1;
        baseDamage = 20f;
    }

    public void SetWeapon(int id)
    {
        ResetData();
        if (id == 0)
        {
            SetStats("단검", 0.1f, 0.4f, 1.5f);
        }
        else if (id == 1)
        {
            SetStats("장검", 0.2f, 0.3f, 2.0f);
        }
        else
        {
            SetStats("도끼", 0.3f, 0.2f, 3.0f);
        }

        logDisplay.text = string.Format("{0} 장착!", weaponName);
        UpdateUI();
    }

    public void LevelUp()
    {
        totalDamage = 0;
        attackCount = 0;
        level++;
        baseDamage = level * 20f;
        logDisplay.text = string.Format("레벨업! 레벨 : {0}", level);
        UpdateUI();
    }

    public void On1000Attack()
    {
        bestDamage = 0f;
        weakPointCount = 0;
        missCount = 0;
        critCount = 0;
        for (int i = 0; i < 1000; i++)
        {
            OnAttack();
        }
        bestDamgeDisplay.text = $"최대 데미지 : {bestDamage}";
        missCountDisplay.text = $"간나빗 공격 : {missCount}";
        weakPointDisplay.text = $"발생한 약점공격 : {weakPointCount}";
        critCountDisplay.text = $"발생한 치명타 : {critCount}";
    }
    public void OnAttack()
    {
        bestDamgeDisplay.text = "";
        missCountDisplay.text = "";
        weakPointDisplay.text = "";
        critCountDisplay.text = "";
        //정규분포 데미지 계산
        float sd = baseDamage * stdDevMult;
        float normalDamage = GetNormalIsStdDevDamage(baseDamage, sd);

        //치명타 판정
        bool isCrit = Random.value < critRate;
        float finalDamage = isCrit ? normalDamage * critMult : normalDamage;
        if (isCrit)
        {
            critCount++;
        }
        //통계 누적
        attackCount++;
        totalDamage += finalDamage;

        //로그 및 UI 업데이트
        string critMark = isCrit ? "<color=red>[치명타!]</color>" : "";
        logDisplay.text = string.Format("{0}데미지: {1:F1}", critMark, finalDamage);
        if(baseStdNormal < -2.0f)
        {
            missDisplay.text = "감나빗!";
            missCount++;
        }
        else if(baseStdNormal > 2f)
        {
            missDisplay.text = "약점공격! 데미지 두배!";
            weakPointCount++;
            if(finalDamage > bestDamage)
            {
                bestDamage = finalDamage;
            }
        }
        else
        {
            missDisplay.text = "";
            if (finalDamage > bestDamage)
            {
                bestDamage = finalDamage;
            }
        }

            UpdateUI();
    }

    private float GetNormalIsStdDevDamage(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        if (randStdNormal > 2.0f)
        {
            baseStdNormal = randStdNormal;
            return (mean + stdDev * randStdNormal) * 2f;
        }
        else if(randStdNormal < -2.0f)
        {
            baseStdNormal = randStdNormal;
            return 0f;
        }
        else
        {
            baseStdNormal = randStdNormal;
            return mean + stdDev * randStdNormal;
        }
    }


    private void SetStats(string _name, float _stdDev, float _critRate, float _critMult)
    {
        weaponName = _name;
        stdDevMult = _stdDev;
        critRate = _critRate;
        critMult = _critMult;
    }

    private void UpdateUI()
    {
        statusDisplay.text = string.Format("Level : {0} / 무기: {1}\n기본 데미지 : {2} / 치명타 : {3}% (x{4})", level, weaponName, baseDamage, critRate * 100, critMult);
        rangeDisplay.text = string.Format("예상 일반 데미지 범위 : [{0:F1} ~ {1:F1}]", baseDamage - (3 * baseDamage * stdDevMult), baseDamage + (3 * baseDamage * stdDevMult));

        float dpa = attackCount > 0 ? totalDamage / attackCount : 0;
        resultDisplay.text = string.Format("누적 데미지 : {0:F1}\n공격 횟수 : {1}\n평균 DPA : {2:F2}", totalDamage, attackCount, dpa);

    }
}


