using System.Numerics;

namespace Day09;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public static class DirectionExtensions
{
    public static Vector2 Vector(this Direction direction)
    {
        return direction switch
        {
            Direction.Left => new Vector2(1, 0),
            Direction.Right => new Vector2(-1, 0),
            Direction.Up => new Vector2(0, 1),
            Direction.Down => new Vector2(0, -1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "This should not happen")
        };
    }

    public static Direction StringInputToDirection(this string input)
    {
        return input switch
        {
            "L" => Direction.Left,
            "R" => Direction.Right,
            "U" => Direction.Up,
            "D" => Direction.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, "This should not happen")
        };
    }
}