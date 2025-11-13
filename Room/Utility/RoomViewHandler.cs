public static class RoomViewHandler
{
    public static void InitView(Room room, DungeonManager dungeonManager)
    {
        DungeonRoomMonoBehaviour view = room.GetRoomGameObject.GetComponent<DungeonRoomMonoBehaviour>();
        view.Init(room, dungeonManager);
    }
}
