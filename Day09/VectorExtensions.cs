using System.Numerics;

namespace Day09;

public static class VectorExtensions
{
    static readonly float TOLERANCE = 0.01f;
    
    public static Vector2 Move(this Vector2 position, Vector2 step)
    {
        position.X += step.X;
        position.Y += step.Y;
        return position;
    }

    public static Vector2 DistanceDirection(this Vector2 position, Vector2 destination)
    {
        if (Vector2.Distance(position, destination) < 2) return Vector2.Zero;
        return (destination - position).FloorToOne();
    }

    private static Vector2 FloorToOne(this Vector2 vector)
    {
        return new Vector2(Floor(vector.X), Floor(vector.Y));
        
        int Floor(float value)
        {
            return value switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => 0
            };
        }
    }
}