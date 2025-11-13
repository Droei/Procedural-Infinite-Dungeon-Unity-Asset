using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private float roomSize = 25f;
    [SerializeField] private List<EnemySpawnData> enemySpawnData;
    [SerializeField] private List<RoomSpawnData> roomSpawnData;
    [SerializeField] private TMP_Text waveText;

    [Header("Debugging")]
    [SerializeField] bool debugMode = false;

    [Space(1)]
    [Header("Seed Settings")]
    [SerializeField] bool useStaticSeed = true;
    [SerializeField] int seed = 6969;

    [Space(1)]
    [Header("Batch Spawning")]
    [SerializeField] bool useBatchSpawning = true;
    [SerializeField][Range(0, 100)] int debugRoomCount = 50;

    [Header("MultiSpawning")]
    //    [SerializeField] bool multiSpawningEnabled = false;

    Dungeon dungeon;

    private int currentWave = 0;

    private IRoomFactory roomFactory;
    private IRoomBuilder roomBuilder;

    private void Awake()
    {
        if (debugMode && useStaticSeed) RandomService.Initialize(seed);
    }

    private void Start()
    {

        dungeon = new();

        IEnemySpawnBuilder builder = new EnemySpawnBuilder();
        IEnemySpawnPlanner planner = new EnemySpawnPlanner();
        IEnemySpawnFactory enemyFactory = new EnemySpawnFactory(builder, planner, enemySpawnData);

        roomBuilder = new RoomBuilder(roomSize, this, enemyFactory, dungeon);
        roomFactory = new RoomFactory(roomSpawnData, roomBuilder);

        roomFactory.CreateRoom(Vector2Int.zero);
    }


    private void Update()
    {
        waveText.text = "Wave: " + currentWave;
    }

    public void SpawnRoom(DirectionEnum direction, Room room)
    {
        roomFactory.SpawnConnectedRoom(direction, room);
    }

    public void NextWave()
    {
        currentWave++;
    }
    public Dungeon GetDungeon => dungeon;
    public int GetCurrentWave => currentWave;

    public bool GetMaxDebugRoomsReached()
    {
        if (currentWave > debugRoomCount) return false;

        return debugMode && useBatchSpawning;
    }
    public bool GetDebugMode => debugMode;
}
