using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Probability
{
    public static void Execute(float probability, Action action)
    {
        if (probability <= 0 || probability >= 100)
        {
            throw new ArgumentException("Probability should be between 0 and 100");
        }

        if (Random.Range(0f, 100f) <= probability)
        {
            action.Invoke();
        }
    }
}
