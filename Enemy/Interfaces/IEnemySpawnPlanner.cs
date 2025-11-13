using System.Collections.Generic;

public interface IEnemySpawnPlanner
{
    List<EnemySpawnData> GetSpawnPlan(int currentWave, List<EnemySpawnData> allEnemies);
}
