using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] TMP_Text waveText;
    [SerializeField] DungeonSettingsData dSD;

    private IRoomFactory roomFactory;
    private IRoomBuilder roomBuilder;
    public NavMeshSurface navSurface;
    private void Awake()
    {
        if (dSD.GetDebugMode && dSD.GetUseStaticSeed) RandomService.Initialize(dSD.GetSeed);
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
        waveText.text = "Rooms spawned: " + dSD.GetDungeon.GetWaveCount + " | Seperate rooms: " + dSD.GetDungeon.GetParentCount;
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
        if (dSD.GetDungeon.GetWaveCount > dSD.GetDebugRoomCount) return false;

        return dSD.GetDebugMode && dSD.GetUseBatchSpawning;
    }
    public bool GetDebugMode => dSD.GetDebugMode;
}
