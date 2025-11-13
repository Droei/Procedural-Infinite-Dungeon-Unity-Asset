using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    private Dictionary<Vector2Int, Room> rooms = new Dictionary<Vector2Int, Room>();

    public void AddRoom(Room room)
    {
        rooms[room.GetGridPosition] = room;
    }

    public Room GetRoom(int x, int y)
    {
        rooms.TryGetValue(new Vector2Int(x, y), out Room room);
        return room;
    }

    public bool RoomExists(Vector2Int pos)
    {
        return rooms.ContainsKey(pos);
    }
}
