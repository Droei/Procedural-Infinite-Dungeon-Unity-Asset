using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] DungeonSettingsData dSD;

    private IRoomFactory roomFactory;
    private IRoomBuilder roomBuilder;

    private void Awake()
    {
        if (dSD.GetDebugMode && dSD.GetUseStaticSeed) RandomService.Initialize(dSD.GetSeed);
    }

    private void Start()
    {
        dSD.SetDungeon(new Dungeon(this));

        IEnemySpawnBuilder builder = new EnemySpawnBuilder(dSD);
        EnemySpawnFactory enemyFactory = new EnemySpawnFactory(builder);

        roomBuilder = new RoomBuilder(dSD, enemyFactory);
        roomFactory = new RoomFactory(roomBuilder);

        roomFactory.CreateInitialRoom(Vector2Int.zero);
    }


    private void Update()
    {
        waveText.text = "Wave: " + dSD.GetDungeon.GetWaveCount;
    }

    // TODO: Find a cleaner solution for this later
    public void SpawnRoom(DirectionEnum direction, Room room)
    {
        roomFactory.SpawnConnectedRoom(direction, room);
    }

    public bool GetMaxDebugRoomsReached()
    {
        if (dSD.GetDungeon.GetWaveCount > dSD.GetDebugRoomCount) return false;

        return dSD.GetDebugMode && dSD.GetUseBatchSpawning;
    }
    public bool GetDebugMode => dSD.GetDebugMode;
}
