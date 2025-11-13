using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpawnData", menuName = "Dungeon/RoomSpawnData")]
public class RoomSpawnData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private DungeonRoomMonoBehaviour roomObject;
    [SerializeField] private EnemySpawnData specificEnemy;

    [Header("Room Specific")]
    [SerializeField] private bool is2x2 = false;

    public DungeonRoomMonoBehaviour RoomObject => roomObject;
    public EnemySpawnData SpecificEnemy => specificEnemy;
    public bool Is2x2 => is2x2;
}
