using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBuilder : IEnemySpawnBuilder
{
    private EnemySpawnData enemySpawnData;
    private Room room;
    private readonly DungeonSettingsData dSD;

    public EnemySpawnBuilder(DungeonSettingsData dSD)
    {
        this.dSD = dSD;
    }

    public List<GameObject> Build()
    {
        var spawned = new List<GameObject>();

        Debug.Log(room.GetRoomName + " spawns with predifined enemy?: " + (enemySpawnData == null));
        if (enemySpawnData == null)
            enemySpawnData = dSD.GetRandomEnemySpawnData;

        if (room.GetParent != null) return null;

        int i = 0;
        while (i <= dSD.Dungeon.GetParentCount)
        {
            if (room.GetChildRooms.Count > 0)
            {
                var roomAreas = SpawnPositionGenerator.GetRoomAndChildBounds(room, dSD);
                var (pos, bounds) = roomAreas[RandomService.Range(0, roomAreas.Length)];
                Vector3 spawnPos = SpawnPositionGenerator.GetRandomPosition(enemySpawnData.EnemyObject, pos.y, bounds, dSD.EnemySpawnMargin);
                SetupEnemy(spawned, spawnPos);
            }
            else
            {
                Vector3 roomPos = room.GetRoomGameObject.transform.position;
                var bounds = SpawnAreaCalculator.Calculate(roomPos, dSD.Dungeon.GetWaveCount);
                Vector3 spawnPos = SpawnPositionGenerator.GetRandomPosition(enemySpawnData.EnemyObject, roomPos.y, bounds, dSD.EnemySpawnMargin);
                SetupEnemy(spawned, spawnPos);
            }
            i += enemySpawnData.SpawnWeight;
        }
        ResetBuilder();
        return spawned;
    }

    private void ResetBuilder()
    {
        enemySpawnData = null;
        room = null;
    }

    private void SetupEnemy(List<GameObject> spawned, Vector3 spawnPos)
    {
        var enemyObj = Object.Instantiate(enemySpawnData.EnemyObject, spawnPos, Quaternion.identity);
        enemyObj.transform.parent = room.GetRoomGameObject.transform;
        enemyObj.GetComponent<DungeonEnemy>().InitForDungeon(room, enemySpawnData.DifficultyMultiplier * dSD.Dungeon.GetWaveCount);
        spawned.Add(enemyObj);
        enemySpawnData.ResetSpawn();
    }

    public IEnemySpawnBuilder WithSpecificEnemyData(EnemySpawnData enemySpawnData) { this.enemySpawnData = enemySpawnData; return this; }
    public IEnemySpawnBuilder WithRoom(Room room) { this.room = room; return this; }
}
