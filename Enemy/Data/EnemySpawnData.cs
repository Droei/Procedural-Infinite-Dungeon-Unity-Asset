using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Dungeon/EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    #region Basic Info
    [Header("Basic Info")]
    [SerializeField] private DungeonEnemy enemyObject;
    [SerializeField] private int spawnWeight = 1;
    #endregion

    #region Wave Settings
    [Header("Wave Settings")]
    [SerializeField] private int minWave = 0;
    [SerializeField] private int maxWave = int.MaxValue;
    #endregion

    #region Batch Settings
    [Header("Batch Settings")]
    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = int.MaxValue;
    [SerializeField] private int spawnInterval = 0;
    #endregion

    #region Spawn Placement Rules
    [Header("Spawn Placement Rules")]
    [SerializeField] private bool isCentered = false;
    #endregion

    #region Difficulty Scaling
    [Header("Difficulty Scaling")]
    [SerializeField] private float difficultyMultiplier = 1f;
    #endregion

    #region Runtime
    private int spawnIntervalTicker = 0;
    #endregion


    public GameObject EnemyObject => enemyObject.gameObject;
    public int SpawnWeight => spawnWeight;
    public int MinWave => minWave;
    public int MaxWave => maxWave;
    public int MinCount => minCount;
    public int MaxCount => maxCount;
    public int SpawnInterval => spawnInterval;
    public bool IsCentered => isCentered;
    public float DifficultyMultiplier => difficultyMultiplier;


    #region Methods
    public bool CanSpawn()
    {
        spawnIntervalTicker++;
        return spawnIntervalTicker >= spawnInterval;
    }

    public void ResetSpawn()
    {
        spawnIntervalTicker = 0;
    }
    #endregion
}