using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBuilder : IEnemySpawnBuilder
{
    private EnemySpawnData data;
    private Room room;
    private float roomSize;
    private int currentWave;

    public List<GameObject> Build()
    {
        var spawned = new List<GameObject>();
        if (data == null || room == null) return spawned;

        /*Debug.Log($"Enemy: <color=red>{data.EnemyObject.name}</color> | " +
                  $"Room: <color=yellow>{room.GetRoomGameObject().name}</color> ");*/

        Vector3 roomPos = room.GetRoomGameObject.transform.position;
        var bounds = SpawnAreaCalculator.Calculate(roomPos, roomSize);

        int i = 0;
        while ((i + data.SpawnWeight <= currentWave || i < data.MinCount) && (i < data.MaxCount))
        {
            Vector3 spawnPos = data.IsCentered
                ? SpawnPositionGenerator.GetCenteredPosition(data.EnemyObject, bounds, roomPos.y)
                : SpawnPositionGenerator.GetRandomPosition(data.EnemyObject, roomPos.y, bounds);

            var enemyObj = Object.Instantiate(data.EnemyObject, spawnPos, Quaternion.identity);
            enemyObj.transform.parent = room.GetRoomGameObject.transform;

            enemyObj.GetComponent<DungeonEnemyMonoBehaviour>().InitForDungeon(room, data.DifficultyMultiplier * currentWave);

            spawned.Add(enemyObj);
            data.ResetSpawn();
            i += data.SpawnWeight;
        }

        return spawned;
    }

    public IEnemySpawnBuilder WithEnemyData(EnemySpawnData data) { this.data = data; return this; }
    public IEnemySpawnBuilder WithRoom(Room room) { this.room = room; return this; }
    public IEnemySpawnBuilder WithRoomSize(float roomSize) { this.roomSize = roomSize; return this; }
    public IEnemySpawnBuilder WithWave(int currentWave) { this.currentWave = currentWave; return this; }
}
