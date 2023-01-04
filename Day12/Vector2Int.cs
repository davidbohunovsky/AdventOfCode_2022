﻿namespace Day12;

public struct Vector2Int
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2Int(float x, float y)
    {
        X = (int)x;
        Y = (int)y;
    }

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"X:{X} Y:{Y}";
    }

    public static bool operator ==(Vector2Int x, Vector2Int y)
    {
        if (x.X == y.X && x.Y == y.Y) return true;
        return false;
    }

    public static bool operator !=(Vector2Int x, Vector2Int y)
    {
        return !(x == y);
    }

    public static Vector2Int operator +(Vector2Int x, Vector2Int y)
    {
        return new Vector2Int(x.X + y.X, x.Y + y.Y);
    }
}