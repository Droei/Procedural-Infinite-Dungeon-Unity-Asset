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

        room = room.GetParent ?? room;

        Debug.Log(room.GetRoomGameObject.name);

        spawned.AddRange(builder
            .WithRoom(room.GetParent ?? room)
            .Build());

        room.AddEnemies(spawned.ToArray());
        return spawned.ToArray();
    }

    public GameObject[] SpawnSpecific(Room room, EnemySpawnData data)
    {
        room = room.GetParent ?? room;

        var spawned = builder
            .WithSpecificEnemyData(data)
            .WithRoom(room)
            .Build();

        room.AddEnemies(spawned.ToArray());
        return spawned.ToArray();
    }
}
