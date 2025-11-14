using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBuilder : IEnemySpawnBuilder
{
    private EnemySpawnData data;
    private Room room;
    private readonly DungeonSettingsData dSD;

    public EnemySpawnBuilder(DungeonSettingsData dSD)
    {
        this.dSD = dSD;
    }

    public List<GameObject> Build()
    {
        var spawned = new List<GameObject>();
        data = dSD.GetRandomEnemySpawnData;
        if (data == null || room == null) return spawned;

        Vector3 roomPos = room.GetRoomGameObject.transform.position;
        var bounds = SpawnAreaCalculator.Calculate(roomPos, dSD.GetDungeon.GetWaveCount);

        int i = 0;
        while ((i + data.SpawnWeight <= dSD.GetDungeon.GetWaveCount || i < data.MinCount) && (i < data.MaxCount))
        {
            Vector3 spawnPos = data.IsCentered
                ? SpawnPositionGenerator.GetCenteredPosition(data.EnemyObject, bounds, roomPos.y)
                : SpawnPositionGenerator.GetRandomPosition(data.EnemyObject, roomPos.y, bounds);

            var enemyObj = Object.Instantiate(data.EnemyObject, spawnPos, Quaternion.identity);
            enemyObj.transform.parent = room.GetRoomGameObject.transform;

            enemyObj.GetComponent<DungeonEnemyMonoBehaviour>().InitForDungeon(room, data.DifficultyMultiplier * dSD.GetDungeon.GetWaveCount);

            spawned.Add(enemyObj);
            data.ResetSpawn();
            i += data.SpawnWeight;
        }

        return spawned;
    }

    public IEnemySpawnBuilder WithSpecificEnemyData(EnemySpawnData data) { this.data = data; return this; }
    public IEnemySpawnBuilder WithRoom(Room room) { this.room = room; return this; }
}
