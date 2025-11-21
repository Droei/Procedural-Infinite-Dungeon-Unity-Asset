using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] DungeonSettingsData dSD;

    private IRoomFactory roomFactory;
    private IRoomBuilder roomBuilder;
    private NavMeshSurface navSurface;
    private void Awake()
    {
        if (dSD.DebugMode && dSD.UseStaticSeed) RandomService.Initialize(dSD.Seed);
    }

    private void Start()
    {
        navSurface = GetComponent<NavMeshSurface>();
        dSD.SetDungeon(new Dungeon(this));

        IEnemySpawnBuilder builder = new EnemySpawnBuilder(dSD);
        EnemySpawnFactory enemyFactory = new EnemySpawnFactory(builder);

        roomBuilder = new RoomBuilder(dSD, enemyFactory);
        roomFactory = new RoomFactory(roomBuilder);

        roomFactory.CreateInitialRoom(Vector2Int.zero);
    }


    private void Update()
    {
        waveText.text = "Rooms spawned: " + dSD.Dungeon.GetWaveCount + " | Seperate rooms: " + dSD.Dungeon.GetParentCount;
    }

    // TODO: Find a cleaner solution for this later
    public void SpawnRoom(DirectionEnum direction, Room room)
    {
        roomFactory.SpawnConnectedRoom(direction, room);
    }

    public void UpdateNavMesh()
    {
        if (navSurface != null)
            navSurface.BuildNavMesh();
    }

    public bool GetMaxDebugRoomsReached()
    {
        if (dSD.Dungeon.GetParentCount > dSD.DebugRoomCount) return false;

        return dSD.DebugMode && dSD.UseBatchSpawning;
    }
    public bool GetDebugMode => dSD.DebugMode;
}
