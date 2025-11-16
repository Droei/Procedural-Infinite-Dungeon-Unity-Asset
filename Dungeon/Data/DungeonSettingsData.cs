using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSettingsData", menuName = "Dungeon/DungeonSettingsData")]
public class DungeonSettingsData : ScriptableObject
{
    [SerializeField] float roomSize = 25f;
    [SerializeField] List<EnemySpawnData> enemySpawnData;
    [SerializeField] List<RoomSpawnData> roomSpawnData;
    [SerializeField][Range(0.0f, 1f)] float doorSpawnChance = .5f;
    [SerializeField] float enemySpawnMargin = 15f;

    [Header("Big room settings")]
    [Tooltip("Chances for a chain of rooms to be created")]
    [SerializeField][Range(0.0f, 0.95f)] float roomChainLikelyhood = .5f;
    [Tooltip("Chances for another room to spawn after a chain is started")]
    [SerializeField][Range(0.0f, 0.95f)] float extendedRoomChainLikelyhood = .3f;
    [Tooltip("Every available direction will have a set amount of chance of actually spawning a room")]
    [SerializeField][Range(0.0f, 0.95f)] float chancePerDirection = .5f;

    [SerializeField] bool crossGenMode = false;

    [Space(1)]
    [Header("Debugging")]
    [SerializeField] bool debugMode = false;

    [Header("Seed Settings")]
    [SerializeField] bool useStaticSeed = true;
    [SerializeField] int seed = 6969;

    [Header("Batch Spawning")]
    [SerializeField] bool useBatchSpawning = true;
    [SerializeField][Range(0, 100)] int debugRoomCount = 50;

    private Dungeon dungeon;

    public float GetRoomSize => roomSize;
    public List<EnemySpawnData> GetEnemySpawnData => enemySpawnData;
    public EnemySpawnData GetRandomEnemySpawnData => enemySpawnData[RandomService.Range(0, enemySpawnData.Count)];


    public List<RoomSpawnData> GetRoomSpawnData => roomSpawnData;
    public RoomSpawnData GetRandomRoomSpawnData => roomSpawnData[RandomService.Range(0, roomSpawnData.Count)];

    public bool GetCrossGenMode => crossGenMode;
    public float GetRoomChainLikelyhood => roomChainLikelyhood;
    public float GetExtendedRoomChainLikelyhood => extendedRoomChainLikelyhood;
    public float GetChancePerDirection => chancePerDirection;

    public bool GetDebugMode => debugMode;
    public bool GetUseStaticSeed => useStaticSeed;
    public int GetSeed => seed;

    public bool GetUseBatchSpawning => useBatchSpawning;
    public int GetDebugRoomCount => debugRoomCount;

    public void SetDungeon(Dungeon dungeon) { this.dungeon = dungeon; }
    public Dungeon GetDungeon => dungeon;

    public float GetDoorSpawnChance => doorSpawnChance;
    public float GetEnemySpawnMargin => enemySpawnMargin;
}
