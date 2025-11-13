public class Door
{
    private DirectionEnum Direction;

    public Door(DirectionEnum dir)
    {
        Direction = dir;
    }

    public DirectionEnum GetDoorDirection => Direction;
}
