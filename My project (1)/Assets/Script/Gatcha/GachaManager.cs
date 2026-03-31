using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    public TextMeshProUGUI[] labels = new TextMeshProUGUI[1];
    int[] counts = new int[1];
    public void SimulateGachaSingle()
    {
        Debug.Log("Gacha Result: " + Simulate());
    }
    
    public void SimulateGachaTenTime()
    {
        List<string> results = new List<string>();
        for (int i = 0; i < 9; i++)
        {
            results.Add(Simulate());
        }
            
        float r2 = Random.value;
        string results2 = string.Empty;
        if (r2 < 2f / 3f) results2 = "A";
        else results2 = "S";
        results.Add(results2);

        Debug.Log("Gacha Results: " + string.Join(", ", results));
    }

    string Simulate()
    {
        float r = Random.value;
        string results = string.Empty;


        if (r < 0.4f) results = "C";
        else if (r < 0.7f) results = "B";
        else if (r < 0.9f) results = "A";
        else results = "S";

        return results  ;
    }
}
