using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSettingsData", menuName = "Dungeon/DungeonSettingsData")]
public class DungeonSettingsData : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField] private float roomSize = 25f;
    [SerializeField] private List<EnemySpawnData> enemySpawnData;
    [SerializeField] private List<RoomSpawnData> roomSpawnData;
    [SerializeField, Range(0f, 1f)] private float doorSpawnChance = 0.5f;
    [SerializeField] private float enemySpawnMargin = 15f;

    [Header("Big Room Settings")]
    [Tooltip("Chances for a chain of rooms to be created")]
    [SerializeField, Range(0f, 0.95f)] private float roomChainLikelyhood = 0.5f;

    [Tooltip("Chances for another room to spawn after a chain is started")]
    [SerializeField, Range(0f, 0.95f)] private float extendedRoomChainLikelyhood = 0.3f;

    [Tooltip("Every available direction will have a set amount of chance of actually spawning a room")]
    [SerializeField, Range(0f, 0.95f)] private float chancePerDirection = 0.5f;

    [SerializeField] private bool crossGenMode = false;

    [Header("Debugging")]
    [SerializeField] private bool debugMode = false;

    [Header("Seed Settings")]
    [SerializeField] private bool useStaticSeed = true;
    [SerializeField] private int seed = 6969;

    [Header("Batch Spawning")]
    [SerializeField] private bool useBatchSpawning = true;
    [SerializeField, Range(0, 100)] private int debugRoomCount = 50;

    private Dungeon dungeon;

    #region General
    public float RoomSize => roomSize;
    public List<EnemySpawnData> EnemySpawnData => enemySpawnData;
    public List<RoomSpawnData> RoomSpawnData => roomSpawnData;
    public float DoorSpawnChance => doorSpawnChance;
    public float EnemySpawnMargin => enemySpawnMargin;
    #endregion

    #region Chained Room
    public float RoomChainLikelyhood => roomChainLikelyhood;
    public float ExtendedRoomChainLikelyhood => extendedRoomChainLikelyhood;
    public float ChancePerDirection => chancePerDirection;
    public bool CrossGenMode => crossGenMode;
    #endregion

    #region Debugging
    public bool DebugMode => debugMode;

    public bool UseStaticSeed => useStaticSeed;
    public int Seed => seed;

    public bool UseBatchSpawning => useBatchSpawning;
    public int DebugRoomCount => debugRoomCount;
    #endregion

    #region Dungeon
    public Dungeon Dungeon => dungeon;
    public void SetDungeon(Dungeon dungeon) => this.dungeon = dungeon;
    #endregion

    #region HelperFunctions
    public EnemySpawnData GetRandomEnemySpawnData =>
        enemySpawnData != null && enemySpawnData.Count > 0
            ? enemySpawnData[RandomService.Range(0, enemySpawnData.Count)]
            : null;

    public RoomSpawnData GetRandomRoomSpawnData =>
        roomSpawnData != null && roomSpawnData.Count > 0
            ? roomSpawnData[RandomService.Range(0, roomSpawnData.Count)]
            : null;
    #endregion
}