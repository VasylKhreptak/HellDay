using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Probability
{
    public static void Execute(float probability, Action action)
    {
        Mathf.Clamp(probability, 0, 100);

        if (Random.Range(0f, 100f) <= probability) action?.Invoke();
    }

    public static bool GetBoolean(float trueProbability)
    {
        Mathf.Clamp(trueProbability, 0, 100);

        return Random.Range(0f, 100f) <= trueProbability;
    }
}