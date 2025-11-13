using UnityEngine;

public static class RoomHelper
{
    public static Vector2Int DirectionToOffset(DirectionEnum dir)
    {
        return dir switch
        {
            DirectionEnum.North => new Vector2Int(0, 1),
            DirectionEnum.South => new Vector2Int(0, -1),
            DirectionEnum.East => new Vector2Int(1, 0),
            DirectionEnum.West => new Vector2Int(-1, 0),
            _ => Vector2Int.zero
        };
    }

    public static DirectionEnum Opposite(DirectionEnum dir)
    {
        return dir switch
        {
            DirectionEnum.North => DirectionEnum.South,
            DirectionEnum.South => DirectionEnum.North,
            DirectionEnum.East => DirectionEnum.West,
            DirectionEnum.West => DirectionEnum.East,
            _ => dir
        };
    }


}
