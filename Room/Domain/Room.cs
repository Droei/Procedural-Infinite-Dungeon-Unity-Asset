using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room
{
    private readonly Vector2Int GridPosition;
    private readonly GameObject RoomGameObject;
    private readonly DungeonRoomMonoBehaviour RoomView;

    public List<Room> ChildRooms = new();
    private Room ParentRoom;

    private GameObject[] Enemies;
    private readonly List<Door> Doors = new();
    private readonly List<RoomOpening> RoomOpenings = new();

    public Room(int x, int y, GameObject prefab)
    {
        GridPosition = new Vector2Int(x, y);
        RoomGameObject = prefab;
        RoomView = RoomGameObject.GetComponent<DungeonRoomMonoBehaviour>();
    }

    public Vector2Int GetGridPosition => GridPosition;
    public GameObject GetRoomGameObject => RoomGameObject;
    public DungeonRoomMonoBehaviour GetRoomView => RoomView;

    public void SetAsChildOfThisRoom(Room room)
    {
        ChildRooms.Add(room);
        room.SetParent(this);
        room.GetRoomGameObject.transform.parent = GetRoomGameObject.transform;
    }

    public void SetParent(Room room) => ParentRoom = room;

    public Room GetParent => ParentRoom;
    #region Room opening
    public List<RoomOpening> GetRoomOpenings => RoomOpenings;
    public void AddRoomOpening(DirectionEnum direction) { RoomOpenings.Add(new(direction)); }

    public bool HasRoomOpening(DirectionEnum direction) { return RoomOpenings.Exists(d => d.GetOpeningDirection == direction); }
    #endregion

    #region Door
    public bool HasDoor(DirectionEnum dir) { return Doors.Exists(d => d.GetDoorDirection == dir); }

    public void AddDoor(DirectionEnum doorDirection) { Doors.Add(new(doorDirection)); }

    public void RemoveDoor(DirectionEnum dir)
    {
        Door door = Doors.FirstOrDefault(d => d.GetDoorDirection == dir);
        if (door != null) Doors.Remove(door);
    }

    public bool CanHaveDoor(DirectionEnum dir)
    {
        if (RoomGameObject == null) return false;

        Transform doorsParent = RoomGameObject.transform.Find("Doors");
        if (doorsParent == null) return false;

        return doorsParent.Find(dir.ToString() + "Door") != null;
    }
    #endregion

    #region Enemies
    public void SetEnemies(GameObject[] enemies) => Enemies = enemies;

    internal void EnemyDied(GameObject enemy)
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] == enemy)
            {
                Enemies[i] = null;
                break;
            }
        }
    }
    #endregion
}
