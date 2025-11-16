using UnityEngine;

public abstract class DungeonEnemy : MonoBehaviour
{
    protected Room room;
    public float difficultyIncrement = 1.0f;

    public virtual void InitForDungeon(Room room, float difficultyIncrement)
    {
        this.room = room;
        this.difficultyIncrement = difficultyIncrement;
    }

    public virtual void OnDestroy()
    {
        room.EnemyDied(gameObject);
    }
}
