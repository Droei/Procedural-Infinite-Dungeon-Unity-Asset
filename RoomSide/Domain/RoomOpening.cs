public class RoomOpening
{
    private DirectionEnum openingDirection;


    public RoomOpening(DirectionEnum openingDirection)
    {
        this.openingDirection = openingDirection;
    }

    public DirectionEnum GetOpeningDirection => openingDirection;
}
