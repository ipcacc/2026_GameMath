using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class DamageSim : MonoBehaviour
{
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI logDisplay;
    public TextMeshProUGUI resultDisplay;
    public TextMeshProUGUI rangeDisplay;

    private int level = 1;
    private float totalDamage = 0, baseDamage = 20f;
    private int attackCount = 0;
    private int missCount = 0;
    private int weakCount = 0;
    private int critCount = 0;
    private float maxDamage = 0;

    private string weaponName;
    private float stdDevMult, critRate, critMult;

    void Start()
    {
        SetWeapon(0);
    }
    private void ResetData()
    {
        totalDamage = 0;
        attackCount = 0;
        level = 1;
        baseDamage = 20f;
        missCount = 0;
        weakCount = 0;
        critCount = 0;
        maxDamage = 0;
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

        logDisplay.text = string.Format("{0} 장착", weaponName);
        UpdateUI();
    }

    private void SetStats(string _name, float _stdDev, float _critRate, float _critMult)
    {
        weaponName = _name;
        stdDevMult = _stdDev;
        critRate = _critRate;
        critMult = _critMult;
    }

    public void LevelUP()
    {
        totalDamage = 0;
        attackCount = 0;
        level++;
        baseDamage = level * 20f;
        logDisplay.text = string.Format("레벨업! 현재 레벨: {0}", level);
        UpdateUI();
    }

    public void OnAttack()
    {
        float sd = baseDamage * stdDevMult;
        float normalDamage = GetNormalStdDevDamage(baseDamage, sd);

        bool isMiss = normalDamage < baseDamage - (2 * sd);
        bool isWeak = normalDamage > baseDamage + (2 * sd);

        float finalDamage = 0;
        bool isCrit = false;

        if (isMiss)
        {
            missCount++;
            logDisplay.text = "[MISS]";
            UpdateUI();
            return;
        }

        // 기본 크리 판정
        isCrit = Random.value < critRate;

        // 약점 공격이면 무조건 크리 + 2배
        if (isWeak)
        {
            weakCount++;
            isCrit = true;
            finalDamage = normalDamage * critMult * 2f;
        }
        else
        {
            finalDamage = isCrit ? normalDamage * critMult : normalDamage;
        }

        if (isCrit) critCount++;

        // 최대 데미지 기록
        if (finalDamage > maxDamage)
            maxDamage = finalDamage;

        attackCount++;
        totalDamage += finalDamage;

        string log = "";
        if (isWeak) log += "[약점공격!]";
        else if (isCrit) log += "<color=red>[치명타!]</color> ";

        logDisplay.text = string.Format("{0}데미지: {1:F1}", log, finalDamage);

        UpdateUI();
    }

    private void UpdateUI()
    {
        statusDisplay.text = string.Format("Level: {0} / 무기: {1}\n기본 데미지: {2} / 치명타: {3}% (x{4})", level, weaponName, baseDamage, critRate * 100, critMult);

        rangeDisplay.text = string.Format("예상 일반 데미지 범위 : [{0:F1} ~ {1:F1}]", baseDamage - (3 * baseDamage * stdDevMult), baseDamage + (3 * baseDamage * stdDevMult));

        float dpa = attackCount > 0 ? totalDamage / attackCount : 0;
        resultDisplay.text = string.Format("누적 데미지: {0:F1}\n" + "공격 횟수: {1}\n" + "평균 DPA: {2:F2}\n\n" + "약점 공격: {3}\n" + "MISS: {4}\n" + "크리티컬: {5}\n" + "최대 데미지: {6:F1}",
            totalDamage, attackCount, dpa, weakCount, missCount, critCount, maxDamage);
    }

    private float GetNormalStdDevDamage(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
        return mean + stdDev * randStdNormal;
    }

    public void Attack1000()
    {
        for (int i = 0; i < 1000; i++)
        {
            OnAttack();
        }

        UpdateUI();
    }
}
