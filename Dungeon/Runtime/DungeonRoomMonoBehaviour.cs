using System.Linq;
using UnityEngine;

public abstract class DungeonRoomMonoBehaviour : MonoBehaviour
{
    protected Room room;
    protected DungeonManager dungeonManager;
    protected DoorView[] doors;
    protected RoomOpening[] roomOpening;
    private Vector2Int gridPos;

    protected DirectionEnum sourceDirection;

    public virtual void Init(Room room, DungeonManager dungeonManager)
    {
        this.room = room;
        this.dungeonManager = dungeonManager;
        doors = GetComponentsInChildren<DoorView>();
    }

    public virtual void SpawnRoom(DirectionEnum direction)
    {
        dungeonManager.SpawnRoom(direction, room);
    }

    public virtual void DisableDoorTrigger(DirectionEnum doorDirection)
    {
        var door = doors.FirstOrDefault(d => d.GetDirection() == doorDirection);
        if (door != null)
        {
            door.MarkTriggered();
        }
        else
        {
            Debug.LogWarning($"No door found in direction {doorDirection} for {gameObject.name}");
        }
    }

    public virtual void SetDoorState(DirectionEnum dir, bool open)
    {
        if (this == null) return;

        Transform doorsParent = transform.Find("Doors");
        if (doorsParent == null) return;

        Transform doorTransform = doorsParent.Find(dir.ToString() + "Door");
        if (doorTransform == null) return;

        DoorManager dm = doorTransform.GetComponent<DoorManager>();
        if (dm == null) return;

        if (open)
        {
            dm.OpenDoor();
        }
        else
            dm.CloseDoor();
    }

    public virtual bool GetDebugMode => dungeonManager.GetMaxDebugRoomsReached();
}
