using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnFactory : IEnemySpawnFactory
{
    private readonly IEnemySpawnBuilder builder;
    private readonly IEnemySpawnPlanner planner;
    private readonly List<EnemySpawnData> allEnemies;
    public EnemySpawnFactory(IEnemySpawnBuilder builder, IEnemySpawnPlanner planner, List<EnemySpawnData> allEnemies)
    {
        this.builder = builder;
        this.planner = planner;
        this.allEnemies = allEnemies;
    }

    public GameObject[] SpawnForWave(Room room, float roomSize, int currentWave)
    {
        if (room == null || allEnemies == null || allEnemies.Count == 0) return Array.Empty<GameObject>();

        List<EnemySpawnData> plan = planner.GetSpawnPlan(currentWave, allEnemies);
        if (plan.Count == 0) return Array.Empty<GameObject>();

        var spawned = new List<GameObject>();

        spawned.AddRange(builder
            .WithEnemyData(plan[RandomService.Range(0, plan.Count)])
            .WithRoom(room)
            .WithRoomSize(roomSize)
            .WithWave(currentWave)
            .Build());

        room.SetEnemies(spawned.ToArray());
        return spawned.ToArray();
    }

    public GameObject[] SpawnSpecific(Room room, float roomSize, int currentWave, EnemySpawnData data)
    {
        if (room == null || data == null) return Array.Empty<GameObject>();

        var spawned = builder
            .WithEnemyData(data)
            .WithRoom(room)
            .WithRoomSize(roomSize)
            .WithWave(currentWave)
            .Build();

        room.SetEnemies(spawned.ToArray());
        return spawned.ToArray();
    }
}
