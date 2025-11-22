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

        if (enemySpawnData == null)
            enemySpawnData = EnemySpawnFilter.GetEnemyData(dSD.EnemySpawnData, dSD.Dungeon.GetParentCount);

        if (room.GetParent != null) return null;

        int minIterations = Mathf.CeilToInt(enemySpawnData.MinCount * enemySpawnData.SpawnWeight);
        int maxIterations = Mathf.FloorToInt(enemySpawnData.MaxCount * enemySpawnData.SpawnWeight);
        int totalIterations = Mathf.Clamp(dSD.Dungeon.GetParentCount, minIterations, maxIterations);

        int i = 0;
        while (i < totalIterations)
        {
            Vector3 spawnPos;

            if (room.GetChildRooms.Count > 0)
            {
                var roomAreas = SpawnPositionGenerator.GetRoomAndChildBounds(room, dSD);
                var (pos, bounds) = roomAreas[RandomService.Range(0, roomAreas.Length)];

                if (enemySpawnData.IsCentered)
                    spawnPos = SpawnPositionGenerator.GetCenteredPosition(pos.y, bounds);
                else
                    spawnPos = SpawnPositionGenerator.GetRandomPosition(pos.y, bounds, dSD.EnemySpawnMargin);
            }
            else
            {
                Vector3 roomPos = room.GetRoomGameObject.transform.position;
                var bounds = SpawnAreaCalculator.Calculate(roomPos, dSD.RoomSize);

                if (enemySpawnData.IsCentered)
                    spawnPos = SpawnPositionGenerator.GetCenteredPosition(roomPos.y, bounds);
                else
                    spawnPos = SpawnPositionGenerator.GetRandomPosition(roomPos.y, bounds, dSD.EnemySpawnMargin);
            }

            SetupEnemy(spawned, spawnPos);

            i += Mathf.Max(1, Mathf.CeilToInt(enemySpawnData.SpawnWeight));
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
        enemyObj.GetComponent<DungeonEnemy>().InitForDungeon(room, enemySpawnData.DifficultyMultiplier * dSD.Dungeon.GetParentCount);
        spawned.Add(enemyObj);
        enemySpawnData.ResetSpawn();
    }

    public IEnemySpawnBuilder WithSpecificEnemyData(EnemySpawnData enemySpawnData) { this.enemySpawnData = enemySpawnData; return this; }
    public IEnemySpawnBuilder WithRoom(Room room) { this.room = room; return this; }
}
