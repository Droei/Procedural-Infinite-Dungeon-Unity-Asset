using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSettingsData", menuName = "Dungeon/DungeonSettingsData")]
public class DungeonSettingsData : ScriptableObject
{
    [SerializeField] float roomSize = 25f;
    [SerializeField] List<EnemySpawnData> enemySpawnData;
    [SerializeField] List<RoomSpawnData> roomSpawnData;

    [Header("Big room settings")]
    [SerializeField] bool crossGenMode = false;

    [Header("Debugging")]
    [Space(1)]
    [Header("Debugging")]
    [SerializeField] bool debugMode = false;

    [Header("Seed Settings")]
    [SerializeField] bool useStaticSeed = true;
    [SerializeField] int seed = 6969;

    [Header("Batch Spawning")]
    [SerializeField] bool useBatchSpawning = true;
    [SerializeField][Range(0, 100)] int debugRoomCount = 50;


    public float GetRoomSize => roomSize;
    public List<EnemySpawnData> GetEnemySpawnData => enemySpawnData;
    public List<RoomSpawnData> GetRoomSpawnData => roomSpawnData;

    public bool GetCrossGenMode => crossGenMode;

    public bool GetDebugMode => debugMode;
    public bool GetUseStaticSeed => useStaticSeed;
    public int GetSeed => seed;

    public bool GetUseBatchSpawning => useBatchSpawning;
    public int GetDebugRoomCount => debugRoomCount;

}
