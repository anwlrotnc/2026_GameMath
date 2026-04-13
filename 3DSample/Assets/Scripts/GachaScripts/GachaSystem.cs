using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{

    int[] currentItem = {0,0,0,0};
    float legendRatio = 0.015f;
    float itemRatio = 0.005f;
    float[] itemPercent = {0.5f, 0.3f, 0.15f, 0.05f};
    string[] gachaResult = { "Common", "Uncommon", "Rare", "Legendary" };
    public TextMeshProUGUI[] percentTexts;
    public TextMeshProUGUI[] inventoryTexts;
 

    public void Simulate()
    {
        float r = Random.value;
        string result = string.Empty;

        float total = 0f;

        for (int i = 0; i < itemPercent.Length; i++)
        {
            if(r < itemPercent[i] + total)
            {
                result = gachaResult[i];
                currentItem[i]++;
                inventoryTexts[i].text = $"{gachaResult[i]} : {currentItem[i]}";
                if (i == 3)
                {
                    itemPercent[0] = 0.5f;
                    itemPercent[1] = 0.3f;
                    itemPercent[2] = 0.15f;
                    itemPercent[3] = 0.05f;
                }
                break;
            }
            total = total + itemPercent[i];
            
        }

        Debug.Log(result);
        for (int i = 0; i < 3; i++)
        {
            itemPercent[i] = itemPercent[i] - itemRatio;
        }
        itemPercent[3] = itemPercent[3] + legendRatio;
        for (int i = 0; i < percentTexts.Length; i++)
        {
            percentTexts[i].text = $"{gachaResult[i]} È®·ü : {(itemPercent[i] * 100)}%";
        }

    }
    private void Start()
    {
        for (int i = 0; i < percentTexts.Length; i++)
        {
            percentTexts[i].text = $"{gachaResult[i]} È®·ü : {(itemPercent[i]*100)}%";
        }
        for (int i = 0; i < inventoryTexts.Length; i++)
        {
            inventoryTexts[i].text = $"{gachaResult[i]} : 0";
        }
    }

}
