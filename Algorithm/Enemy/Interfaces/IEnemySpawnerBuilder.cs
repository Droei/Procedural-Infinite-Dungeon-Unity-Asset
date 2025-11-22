using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnBuilder
{
    IEnemySpawnBuilder WithSpecificEnemyData(EnemySpawnData data);
    IEnemySpawnBuilder WithRoom(Room room);
    List<GameObject> Build();
}
