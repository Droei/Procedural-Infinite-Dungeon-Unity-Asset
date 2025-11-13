using System.Collections.Generic;
using System.Linq;

public class EnemySpawnPlanner : IEnemySpawnPlanner
{
    public List<EnemySpawnData> GetSpawnPlan(int currentWave, List<EnemySpawnData> allEnemies)
    {
        return allEnemies.Where(e =>
            e.CanSpawn() &&
            currentWave > e.MinWave &&
            currentWave < e.MaxWave
        ).ToList();
    }
}
