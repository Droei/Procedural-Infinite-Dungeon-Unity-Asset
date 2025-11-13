using System;
using System.Collections.Generic;
using UnityEngine;

public static class RandomService
{
    private static System.Random rng;
    private static int currentSeed = Environment.TickCount;
    private static bool initialized = false;

    public static void Initialize(int seed)
    {
        currentSeed = seed;
        rng = new System.Random(seed);
        initialized = true;
        Debug.Log($"[RandomService] Initialized with seed: {seed}");
    }

    private static void EnsureInitialized()
    {
        if (!initialized)
        {
            Initialize(Environment.TickCount);
        }
    }

    public static int Range(int min, int max)
    {
        EnsureInitialized();
        return rng.Next(min, max);
    }

    public static float Range(float min, float max)
    {
        EnsureInitialized();
        return Mathf.Lerp(min, max, (float)rng.NextDouble());
    }

    public static bool Chance(float probability)
    {
        EnsureInitialized();
        return rng.NextDouble() < probability;
    }

    public static void Shuffle<T>(IList<T> list)
    {
        EnsureInitialized();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public static int GetSeed() => currentSeed;
}
