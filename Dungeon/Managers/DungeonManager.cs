using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;

    [SerializeField] DungeonSettingsData dungeonSettingsData;

    Dungeon dungeon;

    private int currentWave = 0;

    private IRoomFactory roomFactory;
    private IRoomBuilder roomBuilder;

    private void Awake()
    {
        if (dungeonSettingsData.GetDebugMode && dungeonSettingsData.GetUseStaticSeed) RandomService.Initialize(dungeonSettingsData.GetSeed);
    }

    private void Start()
    {

        dungeon = new();

        IEnemySpawnBuilder builder = new EnemySpawnBuilder();
        IEnemySpawnPlanner planner = new EnemySpawnPlanner();
        IEnemySpawnFactory enemyFactory = new EnemySpawnFactory(builder, planner, dungeonSettingsData.GetEnemySpawnData);

        roomBuilder = new RoomBuilder(dungeonSettingsData.GetRoomSize, this, enemyFactory, dungeon);
        roomFactory = new RoomFactory(dungeonSettingsData.GetRoomSpawnData, roomBuilder, dungeonSettingsData.GetCrossGenMode);

        roomFactory.CreateRoom(Vector2Int.zero);
    }


    private void Update()
    {
        waveText.text = "Wave: " + currentWave;
    }

    // TODO: Find a cleaner solution for this later
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
        if (currentWave > dungeonSettingsData.GetDebugRoomCount) return false;

        return dungeonSettingsData.GetDebugMode && dungeonSettingsData.GetUseBatchSpawning;
    }
    public bool GetDebugMode => dungeonSettingsData.GetDebugMode;
}
