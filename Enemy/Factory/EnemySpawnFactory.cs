using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnFactory : IEnemySpawnFactory
{
    private readonly IEnemySpawnBuilder builder;
    public EnemySpawnFactory(IEnemySpawnBuilder builder)
    {
        this.builder = builder;
    }

    public GameObject[] SpawnForWave(Room room)
    {
        var spawned = new List<GameObject>();

        spawned.AddRange(builder
            .WithRoom(room)
            .Build());

        room.SetEnemies(spawned.ToArray());
        return spawned.ToArray();
    }

    public GameObject[] SpawnSpecific(Room room, EnemySpawnData data)
    {
        if (room == null || data == null) return Array.Empty<GameObject>();

        var spawned = builder
            .WithSpecificEnemyData(data)
            .WithRoom(room)
            .Build();

        room.SetEnemies(spawned.ToArray());
        return spawned.ToArray();
    }
}
