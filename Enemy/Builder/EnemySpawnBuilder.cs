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

        if (room.GetParent != null) return null;
        Debug.Log("Start room: " + room.GetRoomName);
        int i = 0;
        while (i <= dSD.GetDungeon.GetParentCount)
        {
            Debug.Log(i + " " + room.GetRoomName);
            if (room.GetChildRooms.Count > 0)
            {
                // TODO clean this up so I don't need to constantly get bounds
                // Also create a Bounds class lazy ****
                var roomAreas = SpawnPositionGenerator.GetRoomAndChildBounds(room, dSD);
                var (pos, bounds) = roomAreas[RandomService.Range(0, roomAreas.Length)];
                Vector3 spawnPos = SpawnPositionGenerator.GetRandomPosition(data.EnemyObject, pos.y, bounds);
                SetupEnemy(spawned, spawnPos);
            }
            else
            {
                Vector3 roomPos = room.GetRoomGameObject.transform.position;
                var bounds = SpawnAreaCalculator.Calculate(roomPos, dSD.GetDungeon.GetWaveCount);
                Vector3 spawnPos = SpawnPositionGenerator.GetRandomPosition(data.EnemyObject, roomPos.y, bounds);
                SetupEnemy(spawned, spawnPos);
            }
            i += data.SpawnWeight;
        }
        return spawned;
    }

    private void SetupEnemy(List<GameObject> spawned, Vector3 spawnPos)
    {
        var enemyObj = Object.Instantiate(data.EnemyObject, spawnPos, Quaternion.identity);
        enemyObj.transform.parent = room.GetRoomGameObject.transform;
        enemyObj.GetComponent<DungeonEnemyMonoBehaviour>().InitForDungeon(room, data.DifficultyMultiplier * dSD.GetDungeon.GetWaveCount);
        spawned.Add(enemyObj);
        data.ResetSpawn();
    }

    public IEnemySpawnBuilder WithSpecificEnemyData(EnemySpawnData data) { this.data = data; return this; }
    public IEnemySpawnBuilder WithRoom(Room room) { this.room = room; return this; }
}
