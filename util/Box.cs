namespace AOC.util;

public class Box
{
    public Coordinate A { get; }
    public Coordinate B { get; }

    public int MinX { get; }
    public int MaxX { get; }
    public int MinY { get; }
    public int MaxY { get; }

    public Box(Coordinate a, Coordinate b)
    {
        A = a;
        B = b;
        MinX = Math.Min(A.X, B.X);
        MinY = Math.Min(A.Y, B.Y);
        MaxX = Math.Max(A.X, B.X);
        MaxY = Math.Max(A.Y, B.Y);
    }

    public bool InBox(Coordinate c) => InBox(c.X, c.Y);
    public bool InBox(int x, int y) => x >= MinX && x <= MaxX && y >= MinY && y <= MaxY;

    public bool RightOrBelowBox(Coordinate c) => RightOrBelowBox(c.X, c.Y);
    public bool RightOrBelowBox(int x, int y) => x > MaxX || y < MinY;

    public bool LeftOfBox(Coordinate c) => LeftOfBox(c.X, c.Y);
    public bool LeftOfBox(int x, int y) => x < MinX;
}