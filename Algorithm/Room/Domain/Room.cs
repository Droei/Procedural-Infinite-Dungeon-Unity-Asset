using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room
{
    private readonly Vector2Int GridPosition;
    private readonly GameObject RoomGameObject;
    private readonly DungeonRoom RoomView;

    private readonly List<Room> ChildRooms = new();
    private Room ParentRoom;
    private bool isLootRoom = false;

    public List<GameObject> Enemies = new();

    private readonly List<Door> Doors = new();
    private readonly List<DirectionEnum> OpenAreas = new();

    public Room(int x, int y, GameObject prefab)
    {
        GridPosition = new Vector2Int(x, y);
        RoomGameObject = prefab;
        RoomView = RoomGameObject.GetComponent<DungeonRoom>();
    }

    public Vector2Int GetGridPosition => GridPosition;
    public GameObject GetRoomGameObject => RoomGameObject;
    public DungeonRoom GetRoomView => RoomView;
    public List<Room> GetChildRooms => ChildRooms;
    public List<Room> GetNonLootRooms => ChildRooms.Where(r => !r.IsLootRoom).ToList();
    public string GetRoomName => RoomGameObject.name;
    public void SetAsLootRoom() { isLootRoom = true; }
    public bool IsLootRoom => isLootRoom;


    public Room AddChild(Room room)
    {
        ChildRooms.Add(room);
        room.SetParent(this);
        room.GetRoomGameObject.transform.parent = GetRoomGameObject.transform;
        return room;
    }

    private void SetParent(Room room) => ParentRoom = room;
    public Room GetParent => ParentRoom;

    #region Sides
    public bool HasDoor(DirectionEnum dir) { return Doors.Exists(d => d.GetDoorDirection == dir); }

    public void AddDoor(DirectionEnum doorDirection) { Doors.Add(new(doorDirection)); }

    public bool CanHaveDoor(DirectionEnum dir)
    {
        if (RoomGameObject == null) return false;

        Transform doorsParent = RoomGameObject.transform.Find("Doors");
        if (doorsParent == null) return false;

        return doorsParent.Find(dir.ToString() + "Door") != null;
    }
    public void AddOpenArea(DirectionEnum dir)
    {
        OpenAreas.Add(dir);
    }

    public List<DirectionEnum> GetOpenAreas => OpenAreas;
    #endregion

    #region Enemies

    public void AddEnemies(GameObject[] enemies)
    {
        Enemies.AddRange(enemies);
    }


    internal void EnemyDied(GameObject enemy)
    {
        Enemies.Remove(enemy);

        if (Enemies.Count == 0)
        {
            RoomGameObject
                .GetComponent<DungeonRoom>()
                .OpenDoors();
        }
    }

    #endregion
}
