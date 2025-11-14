using UnityEngine;

public interface IEnemySpawnFactory
{
    GameObject[] SpawnForWave(Room room);
    GameObject[] SpawnSpecific(Room room, EnemySpawnData data);
}