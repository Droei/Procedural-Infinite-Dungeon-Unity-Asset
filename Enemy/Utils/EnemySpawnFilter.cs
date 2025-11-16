using System.Collections.Generic;
using System.Linq;

public static class EnemySpawnFilter
{
    public static List<EnemySpawnData> FilterAll(List<EnemySpawnData> data, int currentWave)
    {
        if (data == null || data.Count == 0)
            return new List<EnemySpawnData>();

        List<EnemySpawnData> filtered = data;

        filtered = FilterByWave(filtered, currentWave);
        filtered = FilterBySpawnInterval(filtered);

        return filtered;
    }

    private static List<EnemySpawnData> FilterByWave(List<EnemySpawnData> data, int currentWave)
    {
        return data.Where(d =>
            currentWave >= d.MinWave &&
            currentWave <= d.MaxWave
        ).ToList();
    }

    private static List<EnemySpawnData> FilterBySpawnInterval(List<EnemySpawnData> data)
    {
        return data.Where(d => d.CanSpawn()).ToList();
    }

    public static EnemySpawnData GetEnemyData(List<EnemySpawnData> data, int currentWave)
    {
        List<EnemySpawnData> filtered = FilterAll(data, currentWave);

        if (filtered == null || filtered.Count == 0)
            return null;

        int totalWeight = filtered.Sum(d => d.SpawnWeight);
        int roll = RandomService.Range(0, totalWeight);

        int cumulative = 0;

        foreach (var d in filtered)
        {
            cumulative += d.SpawnWeight;

            if (roll < cumulative)
            {
                d.ResetSpawn();
                return d;
            }
        }

        return filtered[filtered.Count - 1];
    }
}
