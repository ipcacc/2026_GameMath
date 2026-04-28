using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnBasedSimulation : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    public int maxHitsPerTurn = 5;
    public float hitRate = 0.6f;
    public float critChance = 0.2f;
    public float meanDamage = 20f;
    public float stdDevDamage = 5f;
    public float enemyHP = 100f;
    public float poissonLambda = 2f;

    int turn = 0;
    bool rareItemObtained = false;
    float rareChance = 0.2f; // 시작 20%

    int totalEnemyCount = 0;
    int killCount = 0;
    int totalHits = 0;
    int totalAttacks = 0;
    int totalCrits = 0;

    float maxDamage = float.MinValue;
    float minDamage = float.MaxValue;

    int potionCount = 0;
    int goldCount = 0;
    int weaponNormal = 0;
    int weaponRare = 0;
    int armorNormal = 0;
    int armorRare = 0;

    string[] rewards = { "Potion", "Gold", "Weapon", "Armor" };

    public void StartSimulation()
    {
        ResetData();

        while (!rareItemObtained)
        {
            SimulateTurn();
            turn++;

            rareChance += 0.05f;
            rareChance = Mathf.Clamp01(rareChance);
        }

        UpdateResultUI();
    }

    void ResetData()
    {
        turn = 0;
        rareItemObtained = false;
        rareChance = 0.2f;

        totalEnemyCount = 0;
        killCount = 0;
        totalHits = 0;
        totalAttacks = 0;
        totalCrits = 0;

        maxDamage = float.MinValue;
        minDamage = float.MaxValue;

        potionCount = 0;
        goldCount = 0;
        weaponNormal = 0;
        weaponRare = 0;
        armorNormal = 0;
        armorRare = 0;
    }

    void SimulateTurn()
    {
        int enemyCount = SamplePoisson(poissonLambda);
        totalEnemyCount += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            int hits = SampleBinomial(maxHitsPerTurn, hitRate);
            totalHits += hits;
            totalAttacks += maxHitsPerTurn;

            float totalDamage = 0f;

            for (int h = 0; h < hits; h++)
            {
                float damage = SampleNormal(meanDamage, stdDevDamage);

                bool isCrit = Random.value < critChance;
                if (isCrit)
                {
                    damage *= 2f;
                    totalCrits++;
                }

                totalDamage += damage;

                if (damage > maxDamage) maxDamage = damage;
                if (damage < minDamage) minDamage = damage;
            }

            if (totalDamage >= enemyHP)
            {
                killCount++;

                string reward = rewards[Random.Range(0, rewards.Length)];

                bool isRare = Random.value < rareChance;

                if (reward == "Potion") potionCount++;
                else if (reward == "Gold") goldCount++;
                else if (reward == "Weapon")
                {
                    if (isRare)
                    {
                        weaponRare++;
                        rareItemObtained = true;
                    }
                    else weaponNormal++;
                }
                else if (reward == "Armor")
                {
                    if (isRare)
                    {
                        armorRare++;
                        rareItemObtained = true;
                    }
                    else armorNormal++;
                }
            }
        }
    }

    void UpdateResultUI()
    {
        float hitRateResult = (float)totalHits / totalAttacks;
        float critRateResult = totalHits > 0 ? (float)totalCrits / totalHits : 0f;

        resultText.text =
            "전투 결과\n\n" +

            $"총 진행 턴 수 : {turn}\n" +
            $"발생한 적 : {totalEnemyCount}\n" +
            $"처치한 적 : {killCount}\n" +
            $"공격 명중 결과 : {hitRateResult * 100f:F2}%\n" +
            $"발생한 치명타율 결과 : {critRateResult * 100f:F2}%\n" +
            $"최대 데미지 : {maxDamage:F2}\n" +
            $"최소 데미지 : {minDamage:F2}\n\n" +

            "획득한 아이템\n" +
            $"포션 : {potionCount}개\n" +
            $"골드 : {goldCount}개\n" +
            $"무기 - 일반 : {weaponNormal}개\n" +
            $"무기 - 레어 : {weaponRare}개\n" +
            $"방어구 - 일반 : {armorNormal}개\n" +
            $"방어구 - 레어 : {armorRare}개\n";
    }

    int SamplePoisson(float lambda)
    {
        float L = Mathf.Exp(-lambda);
        float p = 1f;
        int k = 0;

        do
        {
            k++;
            p *= Random.value;
        } while (p > L);

        return k - 1;
    }

    int SampleBinomial(int n, float p)
    {
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            if (Random.value < p)
                count++;
        }
        return count;
    }

    float SampleNormal(float mean, float stdDev)
    {
        float u1 = Random.value;
        float u2 = Random.value;

        float randStdNormal = Mathf.Sqrt(-2f * Mathf.Log(u1)) *
                              Mathf.Sin(2f * Mathf.PI * u2);

        return mean + stdDev * randStdNormal;
    }
}

