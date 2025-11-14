using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private readonly Vector2Int GridPosition;
    private readonly GameObject RoomGameObject;
    private readonly DungeonRoomMonoBehaviour RoomView;

    private readonly List<Room> ChildRooms = new();
    private Room ParentRoom;

    private GameObject[] Enemies;

    private readonly List<Door> Doors = new();
    private readonly List<DirectionEnum> OpenAreas = new();

    public Room(int x, int y, GameObject prefab)
    {
        GridPosition = new Vector2Int(x, y);
        RoomGameObject = prefab;
        RoomView = RoomGameObject.GetComponent<DungeonRoomMonoBehaviour>();
    }

    public Vector2Int GetGridPosition => GridPosition;
    public GameObject GetRoomGameObject => RoomGameObject;
    public DungeonRoomMonoBehaviour GetRoomView => RoomView;
    public List<Room> GetChildRooms => ChildRooms;


    public void AddChild(Room room)
    {
        ChildRooms.Add(room);
        room.SetParent(this);
        room.GetRoomGameObject.transform.parent = GetRoomGameObject.transform;
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
