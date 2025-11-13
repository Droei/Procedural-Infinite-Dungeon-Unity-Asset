using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnBuilder
{
    IEnemySpawnBuilder WithEnemyData(EnemySpawnData data);
    IEnemySpawnBuilder WithRoom(Room room);
    IEnemySpawnBuilder WithRoomSize(float roomSize);
    IEnemySpawnBuilder WithWave(int wave);
    List<GameObject> Build();
}
