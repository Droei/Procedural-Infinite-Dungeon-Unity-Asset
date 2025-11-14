public class RoomSidesFactory
{
    private readonly Dungeon dungeon;
    private readonly SidesGenerator sidesGenerator;

    public RoomSidesFactory(Dungeon dungeon)
    {
        this.dungeon = dungeon;
        sidesGenerator = new SidesGenerator(dungeon);
    }

    public void AddRandomSides(ref Room room)
    {
        sidesGenerator.GenerateSides(room);
        SideVisualView.UpdateRoomVisual(room);
    }
}
