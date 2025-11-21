using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpawnData", menuName = "Dungeon/RoomSpawnData")]
public class RoomSpawnData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private DungeonRoom roomObject;
    [SerializeField] private EnemySpawnData specificEnemy;
    [SerializeField] private int maxCooldown = 1;
    private int spawnCooldown = 1;

    [Header("Room Specific")]
    [SerializeField] private bool is1x1 = false;
    [SerializeField] private bool is2x2 = false;
    [SerializeField] private bool isLootRoom = false;
    [SerializeField] private bool canHaveLootroomInSecondarySpawn = true;
    [SerializeField] private GameObject specificLootChest;
    [SerializeField] private bool roomLocks = false;

    public DungeonRoom RoomObject => roomObject;
    public EnemySpawnData SpecificEnemy => specificEnemy;
    public bool Is2x2 => is2x2;
    public bool Is1x1 => is1x1;
    public bool IsLootRoom => isLootRoom;
    public bool CanHaveLootRoomInSecondarySpawn;
    public GameObject SpecificLootChest => specificLootChest;
    public bool RoomLocks => roomLocks;

    public int SpawnCooldown => spawnCooldown;
    public bool IsAvailable => spawnCooldown <= 0;

    public void SetCooldown()
    {
        spawnCooldown = maxCooldown;
    }

    public void DecreaseCooldown()
    {
        if (spawnCooldown > 0)
            spawnCooldown--;
    }

    public void ResetCooldown()
    {
        spawnCooldown = 0;
    }
}
