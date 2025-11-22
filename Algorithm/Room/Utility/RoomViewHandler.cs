public static class RoomViewHandler
{
    public static void InitView(Room room, DungeonSettingsData dSD, RoomSpawnData roomSpawnData)
    {
        DungeonRoom view = room.GetRoomGameObject.GetComponent<DungeonRoom>();
        view.Init(room, dSD, roomSpawnData);
    }
}
