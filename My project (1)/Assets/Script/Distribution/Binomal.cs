using UnityEngine;

public class Binomal : MonoBehaviour
{
    int BinomialDistribution(int n, float p)
    {
        int successes = 0;
        for (int i = 0; i < n; i++)
        {
            if (Random.value < p)
            {
                successes++;
            }
        }
        return successes;
    }
    void Start()
    {
        int result = BinomialDistribution(10, 0.3f);
        Debug.Log($"Successes out of 10 trials: {result}");
    }
}
