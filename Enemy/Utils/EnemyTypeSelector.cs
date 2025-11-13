using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeSelector
{
    private readonly List<EnemyType> enemyTypes;

    public EnemyTypeSelector(List<EnemyType> enemyTypes)
    {
        this.enemyTypes = enemyTypes ?? new List<EnemyType>();
    }

    public bool HasTypes => enemyTypes.Count > 0;

    public EnemyType GetRandomType()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Count)];
    }
}
