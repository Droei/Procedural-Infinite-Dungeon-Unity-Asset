using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonSettingsData", menuName = "Dungeon/DungeonSettingsData")]
public class DungeonSettingsData : ScriptableObject
{
    // Runtime-only working copy
    private DungeonSettingsData original;

    [Header("General Settings")]
    [SerializeField] private int takeOverAtWave = 0;

    [SerializeField] private List<EnemySpawnData> enemySpawnData;
    [SerializeField] private List<RoomSpawnData> roomSpawnData;
    [SerializeField] private GameObject generalLootSpace;
    [SerializeField, Range(0f, 1f)] private float doorSpawnChance = 0.5f;
    [SerializeField] private float enemySpawnMargin = 5f;

    [Header("Big Room Settings")]
    [SerializeField, Range(0f, 0.95f)] private float roomChainLikelyhood = 0.5f;
    [SerializeField, Range(0f, 0.95f)] private float extendedRoomChainLikelyhood = 0.3f;
    [SerializeField, Range(0f, 0.95f)] private float chancePerDirection = 0.5f;
    [SerializeField, Range(0f, 0.95f)] private float lootRoomAppearanceChance = 0.2f;
    [SerializeField] private int lootRoomCooldown = 2;
    [SerializeField] private bool crossGenMode = false;

    [Header("Debugging")]
    [SerializeField] private bool debugMode = false;

    [Header("Seed Settings")]
    [SerializeField] private bool useStaticSeed = true;
    [SerializeField] private int seed = 311625;

    [Header("Batch Spawning")]
    [SerializeField] private bool useBatchSpawning = true;
    [SerializeField, Range(0, 100)] private int debugRoomCount = 50;

    private float roomSize = 25f;
    private Dungeon dungeon;

    #region Properties
    public void SetRoomSize(int roomSize) { this.roomSize = roomSize; }
    public int TakeOverAtWave => takeOverAtWave;
    public float RoomSize => roomSize;
    public List<EnemySpawnData> EnemySpawnData => enemySpawnData;
    public List<RoomSpawnData> RoomSpawnData => roomSpawnData;
    public float DoorSpawnChance => doorSpawnChance;
    public float EnemySpawnMargin => enemySpawnMargin;
    public GameObject GeneralLootSpace => generalLootSpace;

    public float RoomChainLikelyhood => roomChainLikelyhood;
    public float ExtendedRoomChainLikelyhood => extendedRoomChainLikelyhood;
    public float ChancePerDirection => chancePerDirection;
    public bool CrossGenMode => crossGenMode;
    public float LootRoomAppearChance => CanLootRoomSpawn ? lootRoomAppearanceChance : 0f;

    public bool DebugMode => debugMode;
    public bool UseStaticSeed => useStaticSeed;
    public int Seed => seed;

    public bool UseBatchSpawning => useBatchSpawning;
    public int DebugRoomCount => debugRoomCount;

    public Dungeon Dungeon => dungeon;
    public void SetDungeon(Dungeon dungeon) => this.dungeon = dungeon;
    #endregion

    #region Loot Room Logic
    private int lootRoomCooldownCounter = 0;

    public bool CanLootRoomSpawn => lootRoomCooldownCounter <= 0;

    public void CountDownLootRoom()
    {
        if (lootRoomCooldownCounter > 0)
            lootRoomCooldownCounter--;
    }
    #endregion

    #region Random Fetchers
    public EnemySpawnData GetRandomEnemySpawnData =>
        enemySpawnData != null && enemySpawnData.Count > 0
            ? enemySpawnData[RandomService.Range(0, enemySpawnData.Count)]
            : null;

    public RoomSpawnData GetRandomRoomSpawnData
    {
        get
        {
            if (roomSpawnData == null || roomSpawnData.Count == 0)
                return null;

            var available = roomSpawnData
                .Where(r => r.IsAvailable && (!r.IsLootRoom || CanLootRoomSpawn))
                .ToList();

            if (available.Count == 0)
            {
                foreach (var data in roomSpawnData)
                    data.ResetCooldown();

                available = roomSpawnData;
            }

            var chosen = available[RandomService.Range(0, available.Count)];

            chosen.SetCooldown();

            if (chosen.IsLootRoom)
                lootRoomCooldownCounter = lootRoomCooldown;

            return chosen;
        }
    }
    #endregion

    #region Runtime Safety System

    public DungeonSettingsData CreateRuntimeInstance()
    {
        var instance = Instantiate(this);
        instance.hideFlags = HideFlags.HideAndDontSave;

        return instance;
    }


    #endregion

    #region Replace & Restore Logic

    public void ReplaceSettings(DungeonSettingsData other)
    {
        if (other == null)
        {
            Debug.LogWarning("ReplaceSettings failed (source is null)");
            return;
        }

        enemySpawnData = new List<EnemySpawnData>(other.enemySpawnData);
        roomSpawnData = new List<RoomSpawnData>(other.roomSpawnData);
        generalLootSpace = other.generalLootSpace;
        doorSpawnChance = other.doorSpawnChance;
        enemySpawnMargin = other.enemySpawnMargin;

        roomChainLikelyhood = other.roomChainLikelyhood;
        extendedRoomChainLikelyhood = other.extendedRoomChainLikelyhood;
        chancePerDirection = other.chancePerDirection;
        lootRoomAppearanceChance = other.lootRoomAppearanceChance;
        lootRoomCooldown = other.lootRoomCooldown;

        crossGenMode = other.crossGenMode;
    }

    #endregion
}
