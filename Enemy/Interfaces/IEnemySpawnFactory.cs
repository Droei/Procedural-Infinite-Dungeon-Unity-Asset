using UnityEngine;

public interface IEnemySpawnFactory
{
    GameObject[] SpawnForWave(Room room, float roomSize, int currentWave);
    GameObject[] SpawnSpecific(Room room, float roomSize, int currentWave, EnemySpawnData data);
}